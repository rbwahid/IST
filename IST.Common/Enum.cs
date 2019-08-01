using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Common
{
    public enum EnumUserRoleStatus
    {
        GeneralUser = 1,
        SuperAdministrator = 2,
    }

    public enum EnumUserStatus
    {
        GeneralUser = 1,
        SuperAdministrator = 2,
    }
    public enum EnumTicketStatus
    {
        Rejected = 0,
        Pending = 1,
        Withhold = 2,
        Accepted = 3,
        In_Progress = 4,
        Completed = 5,
    }
}
