using EIST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class AttachmentFileUnitOfWork:IDisposable
    {
       private EISTDbContext _context { get; set; }
       private AttachmentFileRepository _attachmentFileRepository { get; set; }

        public AttachmentFileUnitOfWork(EISTDbContext context)
        {
            _context = context;
            _attachmentFileRepository = new AttachmentFileRepository(_context);
        }

        public AttachmentFileRepository AttachmentFileRepository
        {
            get { return _attachmentFileRepository; }
        }
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
