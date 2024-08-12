using System.Collections.Generic;
using System.Linq;
using Web_Based_Major_Project___API.Entities;
using static Web_Based_Major_Project___API.Controllers.NoteController;

namespace Web_Based_Major_Project___API.Services
{
    public class NoteService
    {
        private readonly RestaurantContext _dbContext;

        public NoteService(RestaurantContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Note CreateNote(CreateNoteDto createNoteDto)
        {
            var note = new Note
            {
                Text = createNoteDto.Text,
                Status = createNoteDto.Status
            };

            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();

            return note;
        }

        public List<NoteDto> GetAllNotes()
        {
            return _dbContext.Notes
                .Select(n => new NoteDto
                {
                    Id = n.Id,
                    Text = n.Text,
                    Status = n.Status
                })
                .ToList();
        }

        public NoteDto GetNoteById(int id)
        {
            var note = _dbContext.Notes.Find(id);

            if (note == null)
            {
                return null;
            }

            return new NoteDto
            {
                Id = note.Id,
                Text = note.Text,
                Status = note.Status
            };
        }

        public NoteDto UpdateNote(int id, UpdateNoteDto updateNoteDto)
        {
            var note = _dbContext.Notes.Find(id);

            if (note == null)
            {
                return null;
            }

            note.Text = updateNoteDto.Text;
            note.Status = updateNoteDto.Status;
            _dbContext.SaveChanges();

            return new NoteDto
            {
                Id = note.Id,
                Text = note.Text,
                Status = note.Status
            };
        }

        public bool DeleteNote(int id)
        {
            var note = _dbContext.Notes.Find(id);

            if (note == null)
            {
                return false;
            }

            _dbContext.Notes.Remove(note);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
