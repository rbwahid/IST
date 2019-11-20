using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Common
{
    public enum EnumUserRoleStatus
    {
        GeneralUser = 1,
        SuperAdministrator = 2,
        Admin =3,
        Manager=4,
        Developer=5,
        Customer =6,
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
    public enum EnumTicketAssignStatus
    {
        Pending = 1,
        Withhold = 2,
        Accepted = 3,
        In_Progress = 4,
        Completed = 5,
    }
    public enum EnumWorkflowFormStatus
    {
        Ticket,
        TicketAssign,
    }
}
