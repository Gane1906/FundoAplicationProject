using BussinessLogicLayer.Interface;
using Microsoft.AspNetCore.Http;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BussinessLogicLayer.Services
{
    public class UserBussiness : IUserBusiness
    {
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserBussiness(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public bool IfExists(string email)
        {
            return userRepository.IfExists(email);
        }
        public UserEntity Register(RegisterModel registerModel)
        {
            try
            {
                return userRepository.Register(registerModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public string Login(LoginModel loginModel)
        {
            try
            {
                return userRepository.Login(loginModel);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string ForgetPassword(string email)
        {
            try
            {
                return userRepository.ForgetPassword(email);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public UserTicketModel CreateTicketForPassword(string email, string token)
        {
            try
            {
                return userRepository.CreateTicketForPassword(email,token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ResetPassword(ResetPassword reset, string email)
        {
            try
            {
                return userRepository.ResetPassword(reset, email);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
