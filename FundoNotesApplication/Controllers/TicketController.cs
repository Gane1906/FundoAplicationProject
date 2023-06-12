using BussinessLogicLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FundoNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        public IUserBusiness userBusiness;
        private readonly IBus bus;
        public TicketController(IUserBusiness userBusiness, IBus bus)
        {
            this.userBusiness = userBusiness;
            this.bus = bus;
        }
        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> CreateTicketForPassword(string email)
        {
            try
            {
                if (email != null)
                {
                    var token = userBusiness.ForgetPassword(email);
                    if (token != null)
                    {
                        var output = userBusiness.CreateTicketForPassword(email, token);
                        Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                        var endPoint = await bus.GetSendEndpoint(uri);
                        await endPoint.Send(output);
                        return Ok(new { success = true, message = "Email sent succesfully",data=token });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "EmailId not recognized",data=token });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
