using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class AuthorizationService
    {
        DatabaseContext db = new DatabaseContext();

        public static bool Authorize(params int[] permissions)
        {
            //logic to authorize
            //grab the user's cached group in redis, else in database
            //grab the group's permissions in redis, else in database
            //authorize
            //for now we call from db first




            return true;
        }

    }
}