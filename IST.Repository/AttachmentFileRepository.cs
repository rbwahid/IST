using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IST.Entities;

namespace IST.Repository
{
   public class AttachmentFileRepository:Repository<AttachmentFile>
   {
       private ISTDbContext _context;

       public AttachmentFileRepository(ISTDbContext context) : base(context)
       {
           _context = context;
       }
    }
}
