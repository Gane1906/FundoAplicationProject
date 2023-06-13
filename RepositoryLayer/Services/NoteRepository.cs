using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public List<NoteEntity> GetAllNotes(int UserId)
        {
            try
            {
                List<NoteEntity> result = new List<NoteEntity>();
                var count = fundoContext.Notes.Where(x => x.UserId == UserId).Count();
                for(int id=1; id <= count;id++)
                {
                    var note = fundoContext.Notes.Where(x => x.NoteId == id).FirstOrDefault();
                    result.Add(note);
                }
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool NotePinorUnpin(int NoteId,int userId)
        {
            try
            {
                NoteEntity note = fundoContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if (note.IsPinned == false)
                {
                    note.IsPinned = true;
                    fundoContext.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsPinned = false;
                    fundoContext.SaveChanges();
                    return false;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool NoteArchiveorNot(int NoteId,int userId)
        {
            try
            {
                NoteEntity note = fundoContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if (note.IsArchive == false)
                {
                    if (note.IsPinned == true)
                    {
                        note.IsPinned = false;
                    }
                    note.IsArchive = true;
                    fundoContext.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsArchive = false;
                    fundoContext.SaveChanges();
                    return false;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
