using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIST.Entities;

namespace EIST.Repository
{
   public class AttachmentFileRepository:Repository<AttachmentFile>
   {
       private EISTDbContext _context;

       public AttachmentFileRepository(EISTDbContext context) : base(context)
       {
           _context = context;
       }
    }
}
