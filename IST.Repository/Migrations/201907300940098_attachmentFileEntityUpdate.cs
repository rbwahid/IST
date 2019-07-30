namespace IST.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attachmentFileEntityUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "AttachmentFileId", "dbo.AttachmentFiles");
            DropIndex("dbo.Tickets", new[] { "AttachmentFileId" });
            AddColumn("dbo.AttachmentFiles", "TicketId", c => c.Int());
            CreateIndex("dbo.AttachmentFiles", "TicketId");
            AddForeignKey("dbo.AttachmentFiles", "TicketId", "dbo.Tickets", "Id");
            DropColumn("dbo.Tickets", "AttachmentFileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "AttachmentFileId", c => c.Int());
            DropForeignKey("dbo.AttachmentFiles", "TicketId", "dbo.Tickets");
            DropIndex("dbo.AttachmentFiles", new[] { "TicketId" });
            DropColumn("dbo.AttachmentFiles", "TicketId");
            CreateIndex("dbo.Tickets", "AttachmentFileId");
            AddForeignKey("dbo.Tickets", "AttachmentFileId", "dbo.AttachmentFiles", "Id");
        }
    }
}
