using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace DynPstn.Models
{
    public class Device
    {
        public Device()
        {
            this.TwilioNumbers = new List<IncomingPhoneNumber>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string AssignedNumber { get; set; }
        public IList<IncomingPhoneNumber> TwilioNumbers { get; set; }
    }
}
