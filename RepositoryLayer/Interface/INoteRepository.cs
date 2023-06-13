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
        public List<NoteEntity> GetAllNotes(int UserId);
        public bool NotePinorUnpin(int NoteId, int userId);
        public bool NoteArchiveorNot(int NoteId, int userId);
    }
}
