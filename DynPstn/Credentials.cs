using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynPstn
{
    public static class Credentials
    {
        public static string AccountSid { get { return ConfigurationManager.AppSettings["TwilioAccountSid"]; } }
        public static string AuthToken { get { return ConfigurationManager.AppSettings["TwilioAuthToken"]; } }
    }
}
