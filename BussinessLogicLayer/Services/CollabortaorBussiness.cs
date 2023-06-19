using BussinessLogicLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Services
{
    public class CollabortaorBussiness : ICollaboratorBussiness
    {
        private readonly ICollaboratorrepository collaboratorrepository;
        public CollabortaorBussiness(ICollaboratorrepository collaboratorrepository)
        {
            this.collaboratorrepository = collaboratorrepository;
        }
        public CollaboratorEntity AddCollaborator(string email, long noteId, int userId)
        {
            try
            {
                return collaboratorrepository.AddCollaborator(email, noteId, userId);
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
                return collaboratorrepository.GetAllColaborator(userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
