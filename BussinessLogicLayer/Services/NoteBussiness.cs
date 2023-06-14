using BussinessLogicLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
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
        public bool DeleteNote(int userId, int noteId)
        {
            try
            {
                return noteRepository.DeleteNote(userId, noteId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public NoteEntity UpdateColor(string colour, int noteId, int userId)
        {
            try
            {
                return noteRepository.UpdateColor(colour, noteId, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public string UploadImage(string filePath, long noteId, int userId)
        {
            try
            {
                return noteRepository.UploadImage(filePath, noteId, userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool UpdateRemainder(long noteId, DateTime remainder, int userId)
        {
            try
            {
                return noteRepository.UpdateRemainder(noteId, remainder, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
