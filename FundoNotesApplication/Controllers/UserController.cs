using BussinessLogicLayer.Interface;
using BussinessLogicLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundoNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBussiness;
        private readonly FundoContext fundoContext;
        private readonly INoteBussiness noteBussiness;
        private readonly ILogger<UserController> logger;
        public UserController(IUserBusiness userBussiness,FundoContext fundoContext, INoteBussiness noteBussiness,ILogger<UserController> logger)
        {
            this.userBussiness = userBussiness;
            this.fundoContext = fundoContext;
            this.noteBussiness = noteBussiness;
            this.logger=logger;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterModel register)
        {
            try
            {
                bool ifEmailExists = userBussiness.IfExists(register.Email);
                var result = userBussiness.Register(register);
                if (ifEmailExists)
                {
                    return Ok(new ResponseModel<UserEntity> { status = false, message = "Email already exists", Data = result });
                }
                else
                {
                    if (result != null)
                    {
                        return Ok(new ResponseModel<UserEntity> { status = true, message = "register succesful", Data = result });
                    }
                    else
                    {
                        return BadRequest(new ResponseModel<UserEntity> { status = false, message = "register not succesful", Data = result });
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                RegisterModel model = new RegisterModel();
                var output= userBussiness.Login(loginModel);
                if (output != null)
                {
                    HttpContext.Session.SetString("UserName", model.FirstName + " " + model.Lastname);
                    
                    string? name = this.HttpContext.Session.GetString("UserName");
                    string? email = this.HttpContext.Session.GetString("UserEmail");
                    
                    int? userId = this.HttpContext.Session.GetInt32("UserId");
                    logger.LogInformation("Login succesful");
                    return Ok(new ResponseModel<string> { status = true, message = "Login Succesful",Data=output });
                }
                logger.LogError("Login failed");
                return BadRequest(new ResponseModel<string> { status = false, message = "Login Unsuccesful",Data=output });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Exception thrown..!");
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPatch("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword reset)
        {
            var email = User.Claims.FirstOrDefault(a => a.Type == "Email").Value;
            var forget = userBussiness.ResetPassword(reset, email);
            if (forget != null)
            {
                return Ok(new ResponseModel<string> { status = true,message="password reset succesful",Data=forget });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = false, message = "Mail not sent" });
            }
        }
      
    }
}


