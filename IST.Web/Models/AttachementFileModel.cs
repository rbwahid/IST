using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using IST.Entities;
using IST.Services;
namespace IST.Web.Models
{
    public class AttachmentFileModel
    {
        public HttpPostedFileBase FileBase { get; set; }
        public int? FileTypeId { get; set; }
        private AttachmentFileService _attachmentFileService { get; set; }
        public AttachmentFileModel()
        {
            _attachmentFileService = new AttachmentFileService();
        }

        public AttachmentFile SaveAttachmentFile(AttachmentFileModel FileItem,int ticketId, int? authenticatedUserId)
        {
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(FileItem.FileBase.FileName);
            var fileExtension = Path.GetExtension(FileItem.FileBase.FileName);
           // var finalFileName = fileNameWithoutExt + "_" + formId + "_" + string.Format("{0:yyMMddhhmmss}", DateTime.Now) + fileExtension;
            var finalFileName = Guid.NewGuid() + fileExtension;
            string folderPathWithCurrentMonth = "/Uploads/"+DateTime.Now.Year+"/"+DateTime.Now.ToString("MMMM")+"/";
            string serverPath = HttpContext.Current.Server.MapPath("~"+ folderPathWithCurrentMonth);
            // If directory does not exist, create it. 
            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }
            var path = Path.Combine(serverPath, finalFileName);
            FileItem.FileBase.SaveAs(path);

            var attachementFileEntity = new AttachmentFile
            {
                TicketId = ticketId,
                OriginalName = FileItem.FileBase.FileName,
                FileName = finalFileName,
                FileExtension = fileExtension,
                FileLocation = folderPathWithCurrentMonth + finalFileName,
                CreatedAt = DateTime.Now,
                CreatedBy = authenticatedUserId,
            };
            return attachementFileEntity;
        }

        public IEnumerable<AttachmentFile> GetAttachmentFileByTicketAndId(int entityId, int ticketId)
        {
            return _attachmentFileService.GetAttachementFileByTicketAndId(entityId, ticketId);
        }
        public void Dispose()
        {
            _attachmentFileService.Dispose();
        }
    }
}