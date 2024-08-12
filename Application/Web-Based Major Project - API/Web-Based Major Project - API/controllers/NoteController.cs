using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Web_Based_Major_Project___API.Entities;
using Web_Based_Major_Project___API.Services;

namespace Web_Based_Major_Project___API.Controllers
{
    [Authorize]
    [Route("api/note")]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost]
        public ActionResult<NoteDto> CreateNote([FromBody] CreateNoteDto createNoteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = _noteService.CreateNote(createNoteDto);

            var noteDto = new NoteDto
            {
                Id = note.Id,
                Text = note.Text,
                Status = note.Status
            };

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, noteDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<NoteDto>> GetAllNotes()
        {
            var notes = _noteService.GetAllNotes();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetNoteById(int id)
        {
            var noteDto = _noteService.GetNoteById(id);

            if (noteDto == null)
            {
                return NotFound();
            }

            return Ok(noteDto);
        }

        [HttpPut("{id}")]
        public ActionResult<NoteDto> UpdateNote(int id, [FromBody] UpdateNoteDto updateNoteDto)
        {
            var noteDto = _noteService.UpdateNote(id, updateNoteDto);

            if (noteDto == null)
            {
                return NotFound();
            }

            return Ok(noteDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteNote(int id)
        {
            var success = _noteService.DeleteNote(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        public class UpdateNoteDto
        {
            public string Text { get; set; }
            public bool Status { get; set; }
        }

        public class CreateNoteDto
        {
            public string Text { get; set; }
            public bool Status { get; set; }
        }

        public class NoteDto
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public bool Status { get; set; }
        }
    }
}
