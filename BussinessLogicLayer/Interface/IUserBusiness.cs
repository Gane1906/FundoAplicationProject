using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Interface
{
    public interface IUserBusiness
    {
        public UserEntity Register(RegisterModel registerModel);
        public string Login(LoginModel loginModel);
        public bool IfExists(string email);
        public string ForgetPassword(string email);
        public UserTicketModel CreateTicketForPassword(string email, string token);
        public string ResetPassword(ResetPassword reset, string email);
    }
}
