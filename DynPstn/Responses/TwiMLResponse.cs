using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML;

namespace DynPstn.Responses
{
    public class TwiMLResponse : Response
    {
        public TwiMLResponse(TwilioResponse response)
        {
            var content = response.ToString();

            this.Contents = stream =>
            {
                var data = Encoding.UTF8.GetBytes(content);
                stream.Write(data, 0, data.Length);
            };

            this.ContentType = "application/xml";
            this.StatusCode = HttpStatusCode.OK;
        }
    }
}
