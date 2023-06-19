using MassTransit;
using ModelLayer.Model;
using System.Threading.Tasks;

namespace TicketUser.Services
{
    public class TicketConsumer : IConsumer<UserTicketModel>
    {
        public async Task Consume(ConsumeContext<UserTicketModel> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
        }
    }
}
