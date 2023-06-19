using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRepository : ILabelRepository
    {
        private readonly FundoContext fundoContext;
        public LabelRepository(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public LabelEntity AddLabel(string name,long noteId,int UserId)
        {
            try
            {
                LabelEntity entity = new LabelEntity();
                entity.LabelName =name;
                entity.NoteId = noteId;
                entity.UserId = UserId;
                fundoContext.Label.Add(entity);
                fundoContext.SaveChanges();
                return entity;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<LabelEntity> GetAllLabelsById(int userId)
        {
            try
            {
                var list = fundoContext.Label.Where(x => x.UserId == userId).ToList();
                return list;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool UpdateLabelName(string oldName,string newName)
        {
            try
            {
                var label = fundoContext.Label.Where(x => x.LabelName == oldName).FirstOrDefault();
                if (label != null)
                {
                    label.LabelName = newName;
                    fundoContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool DeleteLableByName(string LabelName)
        {
            try
            {
                var label = fundoContext.Label.Where(x => x.LabelName == LabelName).FirstOrDefault();
                if (label != null)
                {
                    fundoContext.Label.Remove(label);
                    fundoContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public LabelEntity CreateLabel(string labelName,long noteId,int userId)
        {
            try
            {
                LabelEntity entity = new LabelEntity();
                var user = fundoContext.Label.Where(x => x.UserId == userId);
                if (user != null)
                {
                    var check = fundoContext.Label.Where(x => x.LabelName == labelName).FirstOrDefault();
                    if (check == null)
                    {
                        entity.LabelName = labelName;
                        entity.NoteId = noteId;
                        entity.UserId = userId;
                        fundoContext.Label.Add(entity);
                        fundoContext.SaveChanges();
                        return entity;
                    }
                    else
                    {
                        check.LabelName = labelName;
                        check.NoteId = noteId;
                        check.UserId = userId;
                        fundoContext.SaveChanges();
                        return check;
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
