﻿using BussinessLogicLayer.Interface;
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
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBussiness labelBussiness;
        public LabelController(ILabelBussiness labelBussiness)
        {
            this.labelBussiness = labelBussiness;
        }
        [HttpPost("AddLabel")]
        public IActionResult AddLabel(string name, long noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = labelBussiness.AddLabel(name, noteId, userId);
                if (res != null)
                {
                    return Ok(new ResponseModel<LabelEntity> { status = true, message = "label added sucessful", Data = res });
                }
                else
                {
                    return BadRequest(new ResponseModel<LabelEntity> { status = false,message="label not added",Data=res });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        [Route("GetAllLabels")]
        public IActionResult GetAllLabelsById()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = labelBussiness.GetAllLabelsById(userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<List<LabelEntity>> { status = true, message = "All Labels displayed", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<LabelEntity>> { status = false, message = "something went wrong", Data = result });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost("UpdateLabelName")]
        public IActionResult UpdateLabelName(string LabelName,string NewLabelName)
        {
            try
            {
                var result = labelBussiness.UpdateLabelName(LabelName, NewLabelName);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { status = true, message = "Label name editted", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { status = false, message = "Label name doesnot exist", Data = result });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpPost("DeletebyName")]
        public IActionResult DeleteLableByName(string labelName)
        {
            try
            {
                var res = labelBussiness.DeleteLableByName(labelName);
                if (res)
                {
                    return Ok(new ResponseModel<bool> { status = true, message = "Label deleted sucesul", Data = res });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { status = false, message = "no label such name exists", Data = res });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpPost("CreateLabel")]
        public IActionResult CreateLabel(string labelName,long noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = labelBussiness.CreateLabel(labelName, noteId, userId);
                if (res != null)
                {
                    return Ok(new ResponseModel<LabelEntity> { status = true, message = "Label added succesful", Data = res });
                }
                else
                {
                    return BadRequest(new ResponseModel<LabelEntity> { status = false, message = "something went wrong", Data = res });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
