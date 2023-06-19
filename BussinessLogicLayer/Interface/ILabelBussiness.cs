using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Interface
{
    public interface ILabelBussiness
    {
        public LabelEntity AddLabel(string name, long noteId, int UserId);
        public List<LabelEntity> GetAllLabelsById(int userId);
        public bool UpdateLabelName(string oldName, string newName);
        public bool DeleteLableByName(string LabelName);
        public LabelEntity CreateLabel(string labelName, long noteId, int userId);
    }
}
