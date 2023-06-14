using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRepository : INoteRepository
    {
        public FundoContext fundoContext;
        private readonly IConfiguration configuration;
        public NoteRepository(FundoContext fundoContext, IConfiguration configuration)
        {
            this.fundoContext = fundoContext;
            this.configuration = configuration;
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
        public List<NoteEntity> GetAllNotesById(int UserId)
        {
            try
            {
                var list = fundoContext.Notes.Where(x => x.UserId == UserId).ToList();
                return list;
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
                var noteList = fundoContext.Notes.ToList();
                return noteList;
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
                    if (note.IsArchive == true)
                    {
                        note.IsArchive = false;
                    }
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
        public bool NoteTrashorNot(int NoteId, int userId)
        {
            try
            {
                NoteEntity note = fundoContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if(note.IsTrash == false)
                {
                    if (note.IsPinned == true)
                    {
                        note.IsPinned = false;
                    }
                    note.IsTrash = true;
                    fundoContext.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsTrash = false;
                    fundoContext.SaveChanges();
                    return false;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public NoteEntity UpdateNote(NoteModel note,int noteId,int userId)
        {
            var user = fundoContext.Notes.Where(x => x.UserId == userId);
            if (user != null)
            {
                var entity = user.FirstOrDefault(x => x.NoteId == noteId);
                if (entity != null)
                {
                    entity.Title = note.Title;
                    entity.Title = note.Title;
                    entity.Description = note.Description;
                    entity.Remainder = note.Remainder;
                    entity.Color = note.Color;
                    entity.Image = note.Image;
                    entity.IsArchive = note.IsArchive;
                    entity.IsTrash = note.IsTrash;
                    entity.IsPinned = note.IsPinned;
                    entity.ModifiedAt = DateTime.Now;
                    fundoContext.SaveChanges();
                    return entity;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public bool DeleteNote(int userId,int noteId)
        {
            try
            {
                var entity = fundoContext.Notes.Where(x => x.NoteId == noteId).FirstOrDefault();
                if (entity != null)
                {
                    if (entity.IsArchive == true)
                    {
                        fundoContext.Notes.Remove(entity);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public NoteEntity UpdateColor(string colour,int noteId,int userId)
        {
            try
            {
                var user = fundoContext.Notes.Where(x => x.UserId == userId);
                if (user != null)
                {
                    var note = user.FirstOrDefault(x => x.NoteId == noteId);
                    if (note != null)
                    {
                        note.Color = colour;
                        note.ModifiedAt = DateTime.Now;
                        fundoContext.SaveChanges();
                        return note;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool UpdateRemainder(long noteId,DateTime remainder,int userId)
        {
            try
            {
                var user = fundoContext.Notes.Where(x => x.UserId == userId);
                if (user != null)
                {
                    var note = user.FirstOrDefault(x => x.NoteId == noteId);
                    if (note != null)
                    {
                        note.Remainder = remainder;
                        note.ModifiedAt = DateTime.Now;
                        fundoContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else { return false; }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public string UploadImage(string filePath,long noteId,int userId)
        {
            try
            {
                var user = fundoContext.Notes.Where(x => x.UserId == userId);
                if (user != null)
                {
                    var note = user.FirstOrDefault(x => x.NoteId == noteId);
                    if (note != null)
                    {
                        Account account = new Account(configuration["Cloudinary:CloudName"], configuration["Cloudinary:ApiKey"], configuration["Cloudinary:ApiSecretkey"]);
                        Cloudinary cloudinary = new Cloudinary(account);
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(filePath),
                            PublicId = note.Title
                        };
                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                        note.ModifiedAt = DateTime.Now;
                        note.Image = uploadResult.Uri.ToString();
                        fundoContext.SaveChanges();
                        return "Image uploaded succesful";
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
