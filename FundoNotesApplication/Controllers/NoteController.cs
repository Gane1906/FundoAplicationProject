using BussinessLogicLayer.Interface;
using Experimental.System.Messaging;
using GreenPipes.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer.Model;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteBussiness noteBussiness;
        private readonly IDistributedCache cache;
        public NoteController(INoteBussiness noteBussiness, IDistributedCache cache)
        {
            this.noteBussiness = noteBussiness;
            this.cache = cache;
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
        public IActionResult GetAllNotesById()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x=>x.Type == "UserId").Value);
                var list = noteBussiness.GetAllNotesById(UserId);
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
        [Route("GetAllNotes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                string cacheKey = "NoteList";
                List<NoteEntity> NoteList;
                byte[] notesData= await cache.GetAsync(cacheKey);
                if (notesData != null)
                {
                    var seriliazedNoteData = Encoding.UTF8.GetString(notesData);
                    NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(seriliazedNoteData);
                }
                else
                {
                    NoteList = noteBussiness.GetAllNotes();
                    var serializedNoteData = JsonConvert.SerializeObject(NoteList);
                    var redisNoteList = Encoding.UTF8.GetBytes(serializedNoteData);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await cache.SetAsync(cacheKey, redisNoteList, options);
                }
                return Ok(NoteList);
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
        [HttpGet]
        [Route("Trash/Restore")]
        public IActionResult NoteTrashorNot(int NoteId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            var res = noteBussiness.NoteArchiveorNot(NoteId, userId);
            if (res)
            {
                return Ok(new ResponseModel<bool> { status = true, message = "Moved to trash", Data = res });
            }
            else
            {
                return Ok(new ResponseModel<bool> { status = true, message = "Restored sucessful", Data = res }); 
            }
        }
        [HttpPost("updateNote")]
        public IActionResult UpdateNote(int noteId,NoteModel note)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
                var result = noteBussiness.UpdateNote(note, noteId, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { status = true, message = "Note Update sucessful", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { status = false, message = "Note update unsuccesful", Data = result });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
        [HttpPost("DeleteNode")]
        public IActionResult DeleteNote(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
                bool output = noteBussiness.DeleteNote(userId,noteId);
                if (output)
                {
                    return Ok(new ResponseModel<bool> { status = true,message="Deleted succesful",Data=output });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { status = false, message = "Cant delete", Data = output });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpPost("UpdateColor")]
        public IActionResult UpdateColor(int noteId,string color)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var res = noteBussiness.UpdateColor(color, noteId, userId);
            if (res != null)
            {
                return Ok(new ResponseModel<NoteEntity> { status = true, message = "Color updated succesfully", Data = res });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { status = false, message = "Color update unsucessful", Data = res });
            }
        }

    }
}
