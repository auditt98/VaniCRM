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
using Google.Apis.Calendar.v3.Data;
using System.Threading.Tasks;

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
                CalendarService.Scope.Calendar, // Manage your calendars
            };

            //GoogleCredential credential = new GoogleCredential() {  }.

            var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = scopes,
                
                //User = "vanicrm.noreply@gmail.com",

            }.FromPrivateKey(s));

            calendarService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                
            });
        }

        public CalendarListEntry Get(string email)
        {
            var calendar = calendarService.CalendarList.List().Execute().Items.Where(c => c.Summary.ToLower() == "work@" + email).FirstOrDefault();
            return calendar;
        }

        public string GetId(string email)
        {
            var calendar = calendarService.CalendarList.List().Execute().Items.Where(c => c.Summary.ToLower() == "work@" + email).FirstOrDefault();
            if(calendar != null)
            {
                return calendar.Id;
            }
            else
            {
                return string.Empty;
            }
        }
        
        public string AddCalendar(string email)
        {
            var calendar = calendarService.CalendarList.List().Execute().Items.Where(c => c.Summary.ToLower() == "work@" + email).FirstOrDefault();
            if(calendar != null)
            {
                return calendar.Id;
            }
            else
            {
                var newCalendar = calendarService.Calendars.Insert(new Google.Apis.Calendar.v3.Data.Calendar() { Summary = "work@" + email, Description = "Description", TimeZone = "Asia/Ho_Chi_Minh" }).Execute();
                return newCalendar.Id;
            }
        }

        public GoogleCalendar DeleteCalendar(string calendarId)
        {
            calendarService.CalendarList.Delete(calendarId).ExecuteAsync();
            return this;
        }

        public GoogleCalendar AddPeopleToAcl(string email = "", string id = "", bool isAsync = false)
        {
            //check if calendar with id exist
            if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(id))
            {
                var aclRule = calendarService.Acl.List(id).Execute().Items.Where(c => c.Scope.Value == email).FirstOrDefault();
                var rule = new AclRule() { Role = "owner", Scope = new AclRule.ScopeData() { Type = "user", Value = email } };
                var publicRule = new AclRule() { Role = "reader", Scope = new AclRule.ScopeData() { Type = "default" } };
                var insObjectRule = calendarService.Acl.Insert(rule, id).Execute();
                var insObjectPublicRule = calendarService.Acl.Insert(publicRule, id).Execute();

            }
            return this;
        }

        public Event AddEvent(DateTime? startDate = null, DateTime? endDate = null, string email = "", string calendarId = "", string description = "", string summary = "", string rrule = "", bool recur = true, string location = "", bool isAllDay = false, List<string> participants = null, Dictionary<string, string> extendedProperties = null)
        {
            if(startDate == null)
            {
                startDate = DateTime.Now;
            }
            if(endDate == null)
            {
                endDate = DateTime.Now.AddHours(1);
            }
            if (!string.IsNullOrEmpty(email))
            {
                calendarId = GetId(email);
                if (!string.IsNullOrEmpty(calendarId))
                {
                    var calEvent = new Event()
                    {
                        Start = new EventDateTime() { DateTime = startDate, TimeZone = "Asia/Ho_Chi_Minh" },
                        End = new EventDateTime() { DateTime = isAllDay ? endDate.Value.Date : endDate, TimeZone = "Asia/Ho_Chi_Minh" },
                        Description = description,
                        Summary = summary,
                        Location = location,
                        Recurrence = new String[] { rrule },
                        ExtendedProperties = new Event.ExtendedPropertiesData()
                        {
                            Private__ = extendedProperties
                        },
                        Visibility = "public",
                        Transparency = "transparent",
                        ColorId = new Random().Next(1, 11).ToString(),
                        Reminders = new Event.RemindersData()
                        {
                            UseDefault = false,
                            Overrides = new EventReminder[] {
                                //new EventReminder() { Method = "email", Minutes = 24 * 60 },
                                new EventReminder() {Method = "email", Minutes = 12 * 60 },
                                new EventReminder() {Method = "email", Minutes = 2 * 60 },
                                new EventReminder() {Method = "email", Minutes = 15 },
                                new EventReminder() {Method = "email", Minutes = 0 },
                                new EventReminder() {Method = "popup", Minutes = 0 },

                            }
                        },
                    };
                    if (participants != null)
                    {
                        foreach(var participant in participants)
                        {
                            if (!string.IsNullOrEmpty(participant))
                            {
                                calEvent.Attendees.Add(new EventAttendee() { Email = participant });
                            }
                            
                        }
                    }
                    EventsResource.InsertRequest request = calendarService.Events.Insert(calEvent, calendarId);
                    request.SendUpdates = EventsResource.InsertRequest.SendUpdatesEnum.All;
                    //request.s
                    var createdEvent = request.Execute();
                    return createdEvent;
                }
                else
                {
                    return null;
                }
            }
            else if (!string.IsNullOrEmpty(calendarId)) //if pass in id then check if role exist
            {
                var calEvent = new Event()
                {
                    Start = new EventDateTime() { DateTime = startDate, TimeZone = "Asia/Ho_Chi_Minh" },
                    End = new EventDateTime() { DateTime = isAllDay ? endDate.Value.Date : endDate, TimeZone = "Asia/Ho_Chi_Minh" },
                    Description = description,
                    Summary = summary,
                    Location = location,
                    Recurrence = new String[] { rrule },
                    Reminders = new Event.RemindersData()
                    {
                        UseDefault = false,
                        Overrides = new EventReminder[] {
                                //new EventReminder() { Method = "email", Minutes = 24 * 60 },
                                new EventReminder() {Method = "email", Minutes = 12 * 60 },
                                new EventReminder() {Method = "email", Minutes = 2 * 60 },
                                new EventReminder() {Method = "email", Minutes = 15 },
                                new EventReminder() {Method = "email", Minutes = 0 },
                                new EventReminder() {Method = "popup", Minutes = 0 },

                            }
                    },
                };
                if (participants != null)
                {
                    foreach (var participant in participants)
                    {
                        if (!string.IsNullOrEmpty(participant))
                        {
                            calEvent.Attendees.Add(new EventAttendee() { Email = participant });
                        }
                    }
                }
                EventsResource.InsertRequest request = calendarService.Events.Insert(calEvent, calendarId);
                var createdEvent = request.Execute();
                return createdEvent;
            }
            else
            {
                return null;
            }
            
        }

        public Event UpdateEvent(DateTime? startDate = null, DateTime? endDate = null, string email = "", string calendarId = "", string eventId = "", string description = "", string summary = "", string rrule = "", bool recur = true, string location = "", bool isAllDay = false, List<string> participants = null, Dictionary<string, string> extendedProperties = null)
        {
            if (startDate == null)
            {
                startDate = DateTime.Now;
            }
            if (endDate == null)
            {
                endDate = DateTime.Now.AddHours(1);
            }
            if (!string.IsNullOrEmpty(email))
            {
                calendarId = GetId(email);
                if (!string.IsNullOrEmpty(calendarId) && !string.IsNullOrEmpty(eventId))
                {
                    var calEvent = calendarService.Events.Get(calendarId, eventId).Execute();
                    calEvent.Start = new EventDateTime() { DateTime = startDate, TimeZone = "Asia/Ho_Chi_Minh" };
                    calEvent.End = new EventDateTime() { DateTime = isAllDay ? endDate.Value.Date : endDate, TimeZone = "Asia/Ho_Chi_Minh" };
                    calEvent.Description = description;
                    calEvent.Summary = summary;
                    calEvent.Location = location;
                    calEvent.Recurrence = new String[] { rrule };
                    var updatedEvent = calendarService.Events.Patch(calEvent, calendarId, eventId).Execute();
                    return updatedEvent;
                }
                else
                {
                    return null;
                }
            }
            else if (!string.IsNullOrEmpty(calendarId) && !string.IsNullOrEmpty(eventId))
            {
                var calEvent = calendarService.Events.Get(calendarId, eventId).Execute();
                calEvent.Start = new EventDateTime() { DateTime = startDate, TimeZone = "Asia/Ho_Chi_Minh" };
                calEvent.End = new EventDateTime() { DateTime = isAllDay ? endDate.Value.Date : endDate, TimeZone = "Asia/Ho_Chi_Minh" };
                calEvent.Description = description;
                calEvent.Summary = summary;
                calEvent.Location = location;
                calEvent.Recurrence = new String[] { rrule };
                var updatedEvent = calendarService.Events.Patch(calEvent, calendarId, eventId).Execute();
                return updatedEvent;
            }
            else
            {
                return null;
            }
        }

        public GoogleCalendar DeleteEvent(string eventId = "", string calendarId = "")
        {
            if(!string.IsNullOrEmpty(calendarId) && !string.IsNullOrEmpty(eventId))
            {
                calendarService.Events.Delete(calendarId, eventId).ExecuteAsync();

            }
            return this;
        }

        public string GetHtmlLink(string calendarId = "", string eventId = "")
        {
            try
            {
                return calendarService.Events.Get(calendarId, eventId).Execute().HtmlLink;
            }
            catch { return ""; }
        }

        public Task<Event> GetHtmlLinkAsync(string calendarId = "", string eventId = "")
        {
            
            return calendarService.Events.Get(calendarId, eventId).ExecuteAsync();
        }

        public string Publish(string calendarId = "", string htmlLink = "")
        {
            try
            {
                var eid = GetEid(htmlLink);

                var publishLink = $"https://calendar.google.com/" + $"event?action=TEMPLATE&tmeid={eid}&tmsrc={calendarId}&scp=ALL";

                return publishLink;
            }
            catch
            {
                return "";
            }
            
        }
    
        public string GetEid(string htmlLink)
        {
            Uri link = new Uri(htmlLink);
            return HttpUtility.UrlEncode(HttpUtility.ParseQueryString(link.Query).Get("eid")); 
        }
    }
}