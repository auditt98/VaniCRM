using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Backend.Extensions;
namespace Backend.Repository
{
    public class UserRepository
    {
        DatabaseContext db = new DatabaseContext();
        GoogleCalendar googleCalendar = new GoogleCalendar();
        public IEnumerable<USER> GetAll()
        {
            return db.USERs.ToList();
        }

        public (IEnumerable<USER> users, Pager p) GetAllUsers(string query = "", int pageSize = 0, int currentPage = 1, List<string> sort = null)
        {
            var q = query.ToLower();

            if (pageSize == 0)
            {
                pageSize = 10;
            }

            var searchResult = db.USERs.ToList();
            Pager page;

            if (String.IsNullOrEmpty(q))
            {
                page = new Pager(searchResult.Count(), currentPage, pageSize, 9999);
            }
            else
            {
                searchResult = searchResult.Where(c => (c.Username != null && c.Username.ToLower().Contains(q)) || (c.FirstName != null && c.FirstName.ToLower().Contains(q)) || (c.LastName != null && c.LastName.ToLower().Contains(q)) || (c.Phone != null || c.Phone.ToLower().Contains(q)) || (c.Email != null && c.Email.ToLower().Contains(q)) || (c.Skype != null && c.Skype.ToLower().Contains(q))).ToList();
                if (searchResult.Count() > 0)
                {
                    page = new Pager(searchResult.Count(), currentPage, pageSize, 9999);
                }
                else
                {
                    page = new Pager(0, currentPage, pageSize, 9999);
                }

            }

            var sortResult = searchResult.OrderBy(c => 1);

            if (sort != null)
            {
                if (sort.Count() > 0)
                {
                    foreach (var sortQuery in sort)
                    {
                        if (sortQuery.Contains("desc."))
                        {
                            var s = sortQuery.Replace("desc.", "");
                            switch (s)
                            {
                                case "username":
                                    sortResult = sortResult.ThenByDescending(c => c.Username ?? string.Empty);
                                    break;
                                case "firstName":
                                    sortResult = sortResult.ThenByDescending(c => c.FirstName ?? string.Empty);
                                    break;
                                case "lastName":
                                    sortResult = sortResult.ThenByDescending(c => c.LastName ?? string.Empty);
                                    break;
                                case "phone":
                                    sortResult = sortResult.ThenByDescending(c => c.Phone ?? string.Empty);
                                    break;
                                case "email":
                                    sortResult = sortResult.ThenByDescending(c => c.Email ?? string.Empty);
                                    break;
                                case "skype":
                                    sortResult = sortResult.ThenByDescending(c => c.Skype ?? string.Empty);
                                    break;
                                default:
                                    sortResult = sortResult.ThenByDescending(c => c.ID);
                                    break;
                            }
                        }
                        else if (sortQuery.Contains("asc."))
                        {
                            var s = sortQuery.Replace("asc.", "");
                            switch (s)
                            {
                                case "username":
                                    sortResult = sortResult.ThenBy(c => c.Username ?? string.Empty);
                                    break;
                                case "firstName":
                                    sortResult = sortResult.ThenBy(c => c.FirstName ?? string.Empty);
                                    break;
                                case "lastName":
                                    sortResult = sortResult.ThenBy(c => c.LastName ?? string.Empty);
                                    break;
                                case "phone":
                                    sortResult = sortResult.ThenBy(c => c.Phone ?? string.Empty);
                                    break;
                                case "email":
                                    sortResult = sortResult.ThenBy(c => c.Email ?? string.Empty);
                                    break;
                                case "skype":
                                    sortResult = sortResult.ThenBy(c => c.Skype ?? string.Empty);
                                    break;
                                default:
                                    sortResult = sortResult.ThenByDescending(c => c.ID);
                                    break;
                            }
                        }
                        else
                        {
                            sortResult = sortResult.ThenByDescending(c => c.ID);
                        }
                    }
                }
                else
                {
                    sortResult = sortResult.ThenByDescending(c => c.ID);
                }
            }
            else
            {
                sortResult = sortResult.ThenByDescending(c => c.ID);
            }

            var takeResult = sortResult.Skip((currentPage - 1) * pageSize).Take(pageSize);
            return (takeResult, page);
        }

        /// <summary>
        /// DEPRECATED! 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public IEnumerable<USER> Search(string query = "", int pageSize = 0, int currentPage = 1)
        {
            //var pager = new Pager(db.USERs.Count(), currentPage, pageSize);
            var q = query.ToLower();
            
            if (String.IsNullOrEmpty(q))
            {
                return db.USERs.OrderByDescending(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            var result = db.USERs.Where(c => c.Username.ToLower().Contains(q) || c.Email.ToLower().Contains(q) || c.Phone.ToLower().Contains(q) || c.Skype.ToLower().Contains(q) || c.FirstName.ToLower().Contains(q) || c.LastName.ToLower().Contains(q));
            return result.OrderByDescending(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
        }

        public USER GetById(int id = 0)
        {
            return db.USERs.Find(id);
        }

        public USER GetByEmail(string email)
        {
            return db.USERs.Where(c => c.Email == email).FirstOrDefault();
        }

        public bool Create(USER u)
        {
            try
            {
                db.USERs.Add(u);
                db.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
            
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.USERs.Find(id);
                if(user != null)
                {
                    try
                    {
                        googleCalendar.DeleteCalendar(user.CalendarId);

                    }
                    catch
                    {

                    }
                    db.USERs.Remove(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateCalendarId(string email, string calendarId)
        {
            try
            {
                var user = db.USERs.Where(c => c.Email == email).FirstOrDefault();
                if(user != null)
                {
                    user.CalendarId = calendarId;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}