using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly FundoContext fundoContext;
        private readonly IConfiguration configuration;
        public UserRepository(FundoContext fundoContext,IConfiguration configuration)
        {
            this.fundoContext = fundoContext;
            this.configuration = configuration;
        }
        public bool IfExists(string email)
        {
            var count = fundoContext.User.Where(v => v.Email == email).Count();
            return count > 0;
        }
        public UserEntity Register(RegisterModel registerModel)
        {
            try
            {
                UserEntity entity = new UserEntity();
                entity.FirstName = registerModel.FirstName;
                entity.Lastname = registerModel.Lastname;
                entity.Email = registerModel.Email;
                entity.Password = EncryptPassword(registerModel.Password);
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                fundoContext.User.Add(entity);
                fundoContext.SaveChanges();
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EncryptPassword(string Password)
        {
            var EncryptPass = Encoding.UTF8.GetBytes(Password);
            return Convert.ToBase64String(EncryptPass);
        } 
        public string Login(LoginModel loginModel)
        {
            try
            {
                string encodedPass = EncryptPassword(loginModel.Password);
                var valid = fundoContext.User.Where(l => l.Email==(loginModel.Email) && l.Password==(encodedPass)).FirstOrDefault();
                if (valid != null)
                {
                    var token = GetToken(valid.UserId,valid.Email);
                    return token;
                }
                return "Invalid Email or password please check once";
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string GetToken(int UserId,string email)
        {
            var secureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var credentials = new SigningCredentials(secureKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",email),
                new Claim("UserId",UserId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
                claims, expires: DateTime.Now.AddMinutes(20), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string ForgetPassword(string email)
        {
            try
            {
                var check = fundoContext.User.Where(x => x.Email == email).FirstOrDefault();
                if (check!=null)
                {
                    var token= GetToken(check.UserId, check.Email);
                    new MSMQ().SendMessage(token, check.Email, check.FirstName);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public UserTicketModel CreateTicketForPassword(string email, string token)
        {
            try
            {
                var check = fundoContext.User.Where(x => x.Email == email).FirstOrDefault();
                if (check != null)
                {
                    UserTicketModel ticket = new UserTicketModel
                    {
                        FirstName = check.FirstName,
                        Lastname = check.Lastname,
                        Email = check.Email,
                        Token = token,
                        IssueDateAndTime = DateTime.Now
                    };
                    return ticket;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ResetPassword(ResetPassword reset, string email)
        {
            if (reset.Password.Equals(reset.ConfirmPassword))
            {
                var emailCheck = this.fundoContext.User.Where(x => x.Email == email).FirstOrDefault();
                emailCheck.Password = EncryptPassword(reset.Password);
                fundoContext.SaveChanges();
                return "Reset Done";
            }
            else
            {
                return null;
            }
        }
        public ProductEntity AddData(AddModel add)
        {
            ProductEntity entity = new ProductEntity();
            entity.ProductId = add.ProductId;
            entity.ProductName = add.ProductName;
            entity.Price = add.Price;
            entity.Quantity = add.Quantity;
            fundoContext.Add(entity);
            fundoContext.SaveChanges();
            return entity;
        }
    }
}