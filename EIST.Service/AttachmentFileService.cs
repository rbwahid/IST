using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIST.Entities;
using EIST.Repository;

namespace EIST.Services
{
    public class AttachmentFileService
    {
        private EISTDbContext _context;
        private AttachmentFileUnitOfWork _attachmentFileUnitOfWork;

        public AttachmentFileService()
        {
            _context = new EISTDbContext();
            _attachmentFileUnitOfWork = new AttachmentFileUnitOfWork(_context);
        }

        public IEnumerable<AttachmentFile> GetAttachementFiles()
        {
            return _attachmentFileUnitOfWork.AttachmentFileRepository.GetAll();
        }
        public AttachmentFile GetAttachementFileById(int id)
        {

            return _attachmentFileUnitOfWork.AttachmentFileRepository.GetById(id);
        }
        public void AddAttachementFile(AttachmentFile attachementFile)
        {
            _attachmentFileUnitOfWork.AttachmentFileRepository.Add(attachementFile);
            _attachmentFileUnitOfWork.Save();
        }
        public void EditAttachementFile()
        {
            _attachmentFileUnitOfWork.Save();
        }

        public void RemoveAttachmentFileFromDbById(int id)
        {
            _attachmentFileUnitOfWork.AttachmentFileRepository.DeleteFromDb(id);
            _attachmentFileUnitOfWork.Save();
        }

        public IEnumerable<AttachmentFile> GetAttachementFileByTicketAndId(int entityId, int ticketId)
        {
            return _attachmentFileUnitOfWork.AttachmentFileRepository.GetAll();
        }
        public void Dispose()
        {
            _attachmentFileUnitOfWork.Dispose();
        }
    }
}
