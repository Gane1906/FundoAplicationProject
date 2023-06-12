using BussinessLogicLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Services
{
    public class NoteBussiness : INoteBussiness
    {
        private readonly INoteRepository noteRepository;
        public NoteBussiness(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public NoteEntity AddNote(NoteModel note, int UserId)
        {
            return noteRepository.AddNote(note, UserId);
        }
    }
}
