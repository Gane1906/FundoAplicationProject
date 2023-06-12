using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Model
{
    public class UserTicketModel
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime IssueDateAndTime { get; set; }
    }
}
