using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRepository : INoteRepository
    {
        public FundoContext fundoContext;
        public NoteRepository(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public NoteEntity AddNote(NoteModel note,int UserId)
        {
            try
            {
                NoteEntity entity = new NoteEntity();
                entity.Title = note.Title;
                entity.Description = note.Description;
                entity.Remainder = note.Remainder;
                entity.Color = note.Color;
                entity.Image = note.Image;
                entity.IsArchive = note.IsArchive;
                entity.IsTrash = note.IsTrash;
                entity.IsPinned = note.IsPinned;
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                entity.UserId = UserId;
                fundoContext.Notes.Add(entity);
                fundoContext.SaveChanges();
                return entity;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        //public NoteEntity
    }
}
