using BussinessLogicLayer.Interface;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Linq;

namespace FundoNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteBussiness noteBussiness;
        private readonly FundoContext fundoContext;
        public NoteController(INoteBussiness noteBussiness,FundoContext fundoContext)
        {
            this.noteBussiness = noteBussiness;
            this.fundoContext = fundoContext;
        }
        [HttpPost("AddNote")]
        public IActionResult AddNote(NoteModel note)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            var noteAdded = noteBussiness.AddNote(note, UserId);
            if (noteAdded != null)
            {
                return Ok(new ResponseModel<NoteEntity> { status = true, message = "note added Succesfully", Data = noteAdded });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { status = false, message = "note adding failed", Data = null });
            }
        }
    }
}
