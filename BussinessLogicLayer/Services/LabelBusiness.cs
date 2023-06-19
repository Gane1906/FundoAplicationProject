using BussinessLogicLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Services
{
    public class LabelBusiness : ILabelBussiness
    {
        private readonly ILabelRepository labelRepository;
        public LabelBusiness(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }
        public LabelEntity AddLabel(string name, long noteId, int UserId)
        {
            try
            {
                return labelRepository.AddLabel(name, noteId, UserId);
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
                return labelRepository.GetAllLabelsById(userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool UpdateLabelName(string oldName, string newName)
        {
            try
            {
                return labelRepository.UpdateLabelName(oldName, newName);
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
                return labelRepository.DeleteLableByName(LabelName);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public LabelEntity CreateLabel(string labelName, long noteId, int userId)
        {
            try
            {
                return labelRepository.CreateLabel(labelName, noteId, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
