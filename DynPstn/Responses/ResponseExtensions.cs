using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML;

namespace DynPstn.Responses
{
    public static class ResponseExtensions
    {
        public static Response AsTwiML(this IResponseFormatter module, TwilioResponse twiml)
        {
            return new TwiMLResponse(twiml);
        }
    }
}
