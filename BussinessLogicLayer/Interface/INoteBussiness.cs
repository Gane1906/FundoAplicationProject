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
    }
}
