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
    }
}
