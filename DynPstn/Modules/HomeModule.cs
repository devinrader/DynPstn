using DynPstn.ViewModels;
using DynPstn.Responses;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using FirebaseSharp;
using FirebaseSharp.Portable;
using Newtonsoft.Json;
using DynPstn.Models;
using Twilio.TwiML;
using Newtonsoft.Json.Linq;

namespace DynPstn.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            //var devices = new List<Device>() {
            //    new Device() { Name="VerizonSim", AssignedNumber="+13144586142", TwilioNumber="+15555555555" },
            //    new Device() { Name="VodaphoneSim", AssignedNumber="+44", TwilioNumber="+1666"}
            //};
            //await firebase.PostAsync("devices", await JsonConvert.SerializeObjectAsync(devices));

            Get["/", true] = async (context, cancellationToken) =>
            {
                Firebase firebase = new Firebase("https://vivid-fire-8972.firebaseio.com/");
                var devices = JsonConvert.DeserializeObject<List<Device>>(await firebase.GetAsync("devices/-J_WmsH0pZD-Xx7sObRe"));
                var currentLocation = JObject.Parse(await firebase.GetAsync("currentLocation")).GetValue("currentLocation").ToString();

                var client = new TwilioRestClient(Credentials.AccountSid, Credentials.AuthToken);
                var incomingPhoneNumbers = await client.ListIncomingPhoneNumbers();

                var data = new HomeViewModel() { IncomingPhoneNumbers = incomingPhoneNumbers.IncomingPhoneNumbers.Where(p=>p.FriendlyName.StartsWith("DynPstn")).ToList(), Devices=devices, CurrentLocation=currentLocation };
                return View["Index", data];
            };

            Post["/location", true] = async (context, cancellationToken) =>
            {
                Firebase firebase = new Firebase("https://vivid-fire-8972.firebaseio.com/");

                var location = this.Request.Form.location.Value;
                var data = string.Format("{{\"currentLocation\":\"{0}\"}}", location.ToString());
                await firebase.PutAsync("currentLocation", data);

                return Response.AsRedirect("/");
            };

            Post["/forward",true] = async (context, cancellationToken) =>
            {
                Firebase firebase = new Firebase("https://vivid-fire-8972.firebaseio.com/");
                var devices = JsonConvert.DeserializeObject<List<Device>>(await firebase.GetAsync("devices/-J_WmsH0pZD-Xx7sObRe"));
                var currentLocation = JObject.Parse(await firebase.GetAsync("currentLocation")).GetValue("currentLocation").ToString();
                var currentLocationDevice = devices.Where(d => d.Location == currentLocation).First();

                var response = new TwilioResponse();
                response.Say("Please wait while we connect you to Devin");
                response.Dial(currentLocationDevice.AssignedNumber);

                return Response.AsTwiML(response);
            };
        }
    }
}
