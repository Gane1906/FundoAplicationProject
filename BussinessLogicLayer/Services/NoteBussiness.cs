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
            try
            {
                return noteRepository.AddNote(note, UserId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<NoteEntity> GetAllNotesById(int UserId)
        {
            try
            {
                return noteRepository.GetAllNotesById(UserId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<NoteEntity> GetAllNotes()
        {
            try
            {
                return noteRepository.GetAllNotes();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool NotePinorUnpin(int NoteId, int userId)
        {
            try
            {
                return noteRepository.NotePinorUnpin(NoteId, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool NoteArchiveorNot(int NoteId, int userId)
        {
            try
            {
                return noteRepository.NoteArchiveorNot(NoteId, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool NoteTrashorNot(int NoteId, int userId)
        {
            try
            {
                return noteRepository.NoteArchiveorNot(NoteId, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public NoteEntity UpdateNote(NoteModel note, int noteId, int userId)
        {
            try
            {
                return noteRepository.UpdateNote(note, noteId, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
