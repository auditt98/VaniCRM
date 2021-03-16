using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Extensions
{
    public class Enum
    {
        public enum EnumPermissions
        {
            USER_CREATE = 1,
            USER_MODIFY = 2,
            USER_DELETE = 3,
            USER_VIEW = 4,
            USER_MODIFY_SELF = 5,
            TASK_CREATE = 6,
            TASK_MODIFY = 7,
            TASK_DELETE = 8,
            TASK_VIEW = 9,
            TAG_CREATE = 10,
            TAG_DELETE_ITEM = 11,
            TAG_DELETE = 12,
            GROUP_CREATE = 13,
            GROUP_DELETE = 14,
            GROUP_MODIFY = 15,
            GROUP_ASSIGN_PERMISSION = 16,
            GROUP_VIEW = 17,
            NOTE_CREATE = 18,
            NOTE_DELETE = 19,
            NOTE_DELETE_ANY = 20,
            LEAD_CREATE = 21,
            LEAD_DELETE = 22,
            LEAD_MODIFY = 23,
            LEAD_CONVERT = 24,
            LEAD_VIEW = 25,
            LEAD_VIEW_LIST = 26,
            DEAL_CREATE = 27,
            DEAL_MODIFY = 28,
            DEAL_DELETE = 29,
            DEAL_CHANGE_STAGE = 30,
            DEAL_VIEW = 31,
            DEAL_VIEW_LIST = 32,
            ACCOUNT_CREATE = 33,
            ACCOUNT_MODIFY = 34,
            ACCOUNT_DELETE = 35,
            ACCOUNT_VIEW = 36,
            ACCOUNT_VIEW_LIST = 37,
            CONTACT_CREATE = 38,
            CONTACT_MODIFY = 39,
            CONTACT_DELETE = 40,
            CONTACT_VIEW = 41,
            CONTACT_VIEW_LIST = 42,
            CAMPAIGN_CREATE = 43,
            CAMPAIGN_MODIFY = 44,
            CAMPAIGN_DELETE = 45,
            CAMPAIGN_VIEW = 46,
            CAMPAIGN_VIEW_LIST = 47,
            REPORT_VIEW_ALL = 48,
            TASK_DELETE_ANY = 49,
            TASK_MODIFY_ANY = 50
        }

        public enum EnumStage
        {
            QUALIFIED = 1,
            VALUE_PROPOSITION = 2,
            FIND_KEY_CONTACTS = 3,
            SEND_PROPOSAL = 4,
            REVIEW = 5,
            NEGOTIATE = 6,
            WON = 7,
            LOST = 8
        }
    }
}