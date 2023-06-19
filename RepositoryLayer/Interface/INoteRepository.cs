using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRepository
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
        public string UploadImage(string filePath, long noteId, int userId);
        public bool UpdateRemainder(long noteId, DateTime remainder, int userId);
        public NoteEntity SearchNote(string name, int userId);
    }
}
