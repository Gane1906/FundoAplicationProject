using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Interface
{
    public interface ICollaboratorBussiness
    {
        public CollaboratorEntity AddCollaborator(string email, long noteId, int userId);
        public List<CollaboratorEntity> GetAllColaborator(int userId);
    }
}
