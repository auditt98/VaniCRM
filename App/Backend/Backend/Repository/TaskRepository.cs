using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class TaskRepository
    {
        DatabaseContext db = new DatabaseContext();


        //calls
        public CALL GetOneCall(int id)
        {
            return new CALL();
        }

        public IEnumerable<CALL> GetUsersCalls(int userID)
        {
            return new List<CALL>();
        }

        //tasks
        public TASK GetOneTask(int id)
        {
            return new TASK();
        }

        public IEnumerable<TASK> GetUsersTasks(int userID)
        {
            return new List<TASK>();
        }
        //meetings

        public IEnumerable<MEETING> GetUsersMeetings(int userID)
        {
            return new List<MEETING>();
        }

    }
}