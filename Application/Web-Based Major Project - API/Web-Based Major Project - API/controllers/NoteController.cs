using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Web_Based_Major_Project___API.Entities;

namespace Web_Based_Major_Project___API.controllers
{
    [Authorize]
    [Route("api/note")]
    public class NoteController : ControllerBase
    {
        private readonly RestaurantContext _dbContext;

        public NoteController(RestaurantContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<NoteDto> CreateNote([FromBody] CreateNoteDto createNoteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = new Note
            {
                Text = createNoteDto.Text,
                Status = createNoteDto.Status
            };

            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();

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
            var notes = _dbContext.Notes
                .Select(n => new NoteDto
                {
                    Id = n.Id,
                    Text = n.Text,
                    Status = n.Status
                })
                .ToList();

            return Ok(notes);
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetNoteById(int id)
        {
            var note = _dbContext.Notes.Find(id);

            if (note == null)
            {
                return NotFound();
            }

            var noteDto = new NoteDto
            {
                Id = note.Id,
                Text = note.Text,
                Status = note.Status
            };

            return Ok(noteDto);
        }

        [HttpPut("{id}")]
        public ActionResult<NoteDto> UpdateNote(int id, [FromBody] UpdateNoteDto updateNoteDto)
        {
            var note = _dbContext.Notes.Find(id);

            if (note == null)
            {
                return NotFound();
            }

            note.Text = updateNoteDto.Text;
            note.Status = updateNoteDto.Status;

            _dbContext.SaveChanges();

            var noteDto = new NoteDto
            {
                Id = note.Id,
                Text = note.Text,
                Status = note.Status
            };

            return Ok(noteDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteNote(int id)
        {
            var note = _dbContext.Notes.Find(id);

            if (note == null)
            {
                return NotFound();
            }

            _dbContext.Notes.Remove(note);
            _dbContext.SaveChanges();

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