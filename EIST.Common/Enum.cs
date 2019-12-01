﻿using System;
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
    public enum EnumIssueStatus
    {
        Rejected = 0,
        Pending = 1,
        Withheld = 2,
        Accepted = 3,
        In_Progress = 4,
        Completed = 5,
    }
    public enum EnumTicketAssignStatus
    {
        Rejected = 0,
        Pending = 1,
        Delegated = 2,
        Started = 3,
        Completed = 4,
    }
    public enum EnumWorkflowFormStatus
    {
        Issue,
        TicketAssign
    }
    public enum EnumUserType : byte
    {
        Customer,
        Developer
    }
}
