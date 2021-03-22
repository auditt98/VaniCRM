using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class TaskTemplateRepository
    {
        DatabaseContext db = new DatabaseContext();

        //get user's tasks

        public IEnumerable<TASK_TEMPLATE> GetUserTaskTemplate(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            var dbUser = db.USERs.Find(userID);
            var templates = dbUser.TaskTemplateCreated.ToList();
            foreach (var meeting in dbUser.MEETING_PARTICIPANT)
            {
                templates.Add(meeting.MEETING.TASK_TEMPLATE);
            }

            if (pageSize == 0)
            {
                pageSize = templates.Count();
            }

            if (String.IsNullOrEmpty(q))
            {
                return templates.OrderBy(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var result = templates.Where(c => c.Title.ToLower().Contains(q.ToLower())).OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }

    }
}