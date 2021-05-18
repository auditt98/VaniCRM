using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Newtonsoft.Json;

namespace Backend.Extensions
{
    public class GoogleCalendar
    {

        //public
        public CalendarService calendarService;
        public GoogleCalendar()
        {
            var keyFolder = HttpContext.Current.Server.MapPath("~/Resources");
            var keyFilePath = Path.Combine(keyFolder, "vanicrm-d871072c086a.json");

            var json = File.ReadAllText(keyFilePath);
            Newtonsoft.Json.Linq.JObject cr = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(json);
            string s = (string)cr.GetValue("private_key");
            string serviceAccountEmail = (string)cr.GetValue("client_email");

            string[] scopes = new string[] {
                CalendarService.Scope.Calendar // Manage your calendars
            };

            //GoogleCredential credential = new GoogleCredential() {  }.

            var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = scopes,
            }.FromPrivateKey(s));

            calendarService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,

            });
        }



    }
}