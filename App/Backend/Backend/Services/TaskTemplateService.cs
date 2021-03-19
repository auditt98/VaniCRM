using Backend.Domain;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class TaskTemplateService
    {
        TaskTemplateRepository _taskTemplateRepository = new TaskTemplateRepository();


        public List<TASK_TEMPLATE> GetUserTaskTemplate(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            return _taskTemplateRepository.GetUserTaskTemplate(userID, q, currentPage, pageSize).ToList();

        }

    }
}