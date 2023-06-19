using BussinessLogicLayer.Interface;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundoNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBussiness collaboratorBussiness;
        public CollaboratorController(ICollaboratorBussiness collaboratorBussiness)
        {
            this.collaboratorBussiness = collaboratorBussiness;
        }
        [HttpPost("AddCollaborator")]
        public IActionResult AddCollaborator(string email,long noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = collaboratorBussiness.AddCollaborator(email, noteId, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<CollaboratorEntity> { status = true, message = "Collaborator added", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<CollaboratorEntity> { status = false, message = "No user with such email id exists", Data = result });
                }
            }
            catch (Exception e) 
            {

                throw e;
            }

        }
        [HttpGet]
        [Route("GetAllColaborators")]
        public IActionResult GetColaborators()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = collaboratorBussiness.GetAllColaborator(userId);
                if (res != null)
                {
                    return Ok(new ResponseModel<List<CollaboratorEntity>> { status = true, message = "All colaborators are displayed", Data = res });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<CollaboratorEntity>> { status = false, message = "Something went wrong", Data = res });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
