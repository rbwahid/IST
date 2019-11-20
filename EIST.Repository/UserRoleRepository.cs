﻿using EIST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class UserRoleRepository : Repository<UserRole>
    {
        private EISTDbContext _context;

        public UserRoleRepository(EISTDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<UserRole> GetAllRoles()
        {
            return _context.UserRoles.Where(x => !x.IsDeleted && x.Status != 2);
        }

        public bool IsRoleNameExist(string RoleName, string InitialRoleName)
        {
            bool isNotExist = true;

            if (RoleName != string.Empty && InitialRoleName == "undefined")
            {
                var isExist = _context.UserRoles.Any(x => x.Status != 0 && x.RoleName.ToLower().Equals(RoleName.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }

            if (RoleName != string.Empty && InitialRoleName != "undefined")
            {
                var isExist = _context.UserRoles.Any(x => x.Status != 0 && x.RoleName.ToLower() == RoleName.ToLower() && x.RoleName.ToLower() != InitialRoleName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }

            return isNotExist;
        }
        public string[] GetRolesById(int id)
        {
            string[] roleTasks = (from a in _context.UserRolePermissions
                                  join b in _context.Users on a.RoleId equals b.RoleId
                                  where b.Id.Equals(id)
                                  select a.Permission).ToArray<string>();
            return roleTasks;

        }

        public UserRole GetRoleByRoleName(string name)
        {
            return _context.UserRoles.FirstOrDefault(e => e.RoleName.ToLower() == name.ToLower());
        }
    }

    public class RoleTaskRepository : BaseRepository<UserRolePermission>
    {
        private EISTDbContext _context;

        public RoleTaskRepository(EISTDbContext context) : base(context)
        {
            _context = context;
        }
    }
}