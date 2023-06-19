using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorrepository
    {
        public CollaboratorEntity AddCollaborator(string email, long noteId, int userId);
        public List<CollaboratorEntity> GetAllColaborator(int userId);
    }
}
