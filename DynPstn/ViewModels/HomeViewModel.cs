using DynPstn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace DynPstn.ViewModels
{
    public class HomeViewModel
    {
        public IList<IncomingPhoneNumber> IncomingPhoneNumbers { get; set; }
        public IList<Device> Devices { get; set; }
        public string CurrentLocation { get; set; }
        public Device CurrentLocationDevice { get { return this.Devices.Where(d => d.Location == this.CurrentLocation).First(); } }
    }
}
