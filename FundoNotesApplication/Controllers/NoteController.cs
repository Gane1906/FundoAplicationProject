using BussinessLogicLayer.Interface;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundoNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteBussiness noteBussiness;
        public NoteController(INoteBussiness noteBussiness)
        {
            this.noteBussiness = noteBussiness;
        }
        [HttpPost("AddNote")]
        public IActionResult AddNote(NoteModel note)
        {
            try
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
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        [Route("GetNoteById")]
        public IActionResult GetAllNotes()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x=>x.Type == "UserId").Value);
                var list = noteBussiness.GetAllNotes(UserId);
                if (list != null)
                {
                    return Ok(new ResponseModel<List<NoteEntity>> { status = true, message="Notes displayed sucesfully",Data = list }) ;
                }
                else
                {
                    return BadRequest(new ResponseModel<List<NoteEntity>> { status = false, message = "note display failed" });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        [Route("NotePin/unpin")]
        public IActionResult NotePinorUnpin(int NoteId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = noteBussiness.NotePinorUnpin(NoteId, UserId);
                if (res)
                {
                    return Ok(new ResponseModel<bool> { status = true, message = "Note pinned" });
                }
                else
                {
                    return Ok(new ResponseModel<bool> { status = true,message="Note unpinned " });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        [Route("Archive/UnArchive")]
        public IActionResult NoteArchiveorUnarchive(int NoteId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = noteBussiness.NoteArchiveorNot(NoteId, UserId);
                if (res)
                {
                    return Ok(new ResponseModel<bool> { status = true, message = "Note Archieved", Data = res });
                }
                else
                {
                    return Ok(new ResponseModel<bool> { status = true, message = "Note Unarchieved", Data = res });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
