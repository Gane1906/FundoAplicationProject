using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Interface
{
    public interface INoteBussiness
    {
        public NoteEntity AddNote(NoteModel note, int UserId);
        public List<NoteEntity> GetAllNotesById(int UserId);
        public List<NoteEntity> GetAllNotes();
        public bool NotePinorUnpin(int NoteId, int userId);
        public bool NoteArchiveorNot(int NoteId, int userId);
        public bool NoteTrashorNot(int NoteId, int userId);
        public NoteEntity UpdateNote(NoteModel note, int noteId, int userId);
        public bool DeleteNote(int userId, int noteId);
        public NoteEntity UpdateColor(string colour, int noteId, int userId);
    }
}
