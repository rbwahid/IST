using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class UserRoleUnitOfWork : IDisposable
    {
        private EISTDbContext _context { get; set; }
        private UserRoleRepository _roleRepository { get; set; }
        private RoleTaskRepository _roleTaskRepository { get; set; }

        public UserRoleUnitOfWork(EISTDbContext context)
        {
            _context = context;
            _roleRepository = new UserRoleRepository(_context);
            _roleTaskRepository = new RoleTaskRepository(_context);
        }

        public UserRoleRepository RoleRepository => _roleRepository;
        public RoleTaskRepository RoleTaskRepository => _roleTaskRepository;

        public void Save(string loggedInUserId)
        {
            _context.SaveChanges(loggedInUserId);
        }

        public void Save()
        {
            _context.SaveChanges("");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
