using Backend.Domain;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class AccountService
    {
        AccountRepository _accountRepository = new AccountRepository();

        public List<ACCOUNT> GetUserAccounts(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            return _accountRepository.GetUserAccounts(userID, q, currentPage, pageSize).ToList();

        } 

    }
}