using Backend.Domain;
using Backend.Models.ApiModel;
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

        public List<AccountListApiModel.AccountInfo> GetAccountList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbAccounts = _accountRepository.GetAllAccounts(query, pageSize, currentPage);
            return dbAccounts.Select(c => new AccountListApiModel.AccountInfo() { id = c.ID, name = c.Name, website = c.Website, phone = c.Phone, owner = c.Owner.FirstName + " " + c.Owner.LastName }).ToList();
        }

    }
}