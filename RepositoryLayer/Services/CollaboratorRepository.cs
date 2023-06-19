using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollaboratorRepository : ICollaboratorrepository
    {
        private readonly FundoContext fundoContext;
        public CollaboratorRepository(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public CollaboratorEntity AddCollaborator(string email,long noteId,int userId)
        {
            try
            {
                CollaboratorEntity entity = new CollaboratorEntity();
                var user = fundoContext.User.Where(x => x.Email == email).FirstOrDefault();
                if (user != null)
                {
                    entity.CollaboratorEmail = email;
                    entity.NoteId = noteId;
                    entity.UserId = userId;
                    fundoContext.Collaborator.Add(entity);
                    fundoContext.SaveChanges();
                    return entity;
                }
                else { return null; }
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public List<CollaboratorEntity> GetAllColaborator(int userId)
        {
            try
            {
                var list = fundoContext.Collaborator.Where(x => x.UserId == userId).ToList();
                return list;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
