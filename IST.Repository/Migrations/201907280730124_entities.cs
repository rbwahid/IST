namespace IST.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttachmentFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        OriginalName = c.String(),
                        FileExtension = c.String(),
                        FileLocation = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        FaxNo = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.CompanyProjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CompanyId = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CompanyId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.TicketAssigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(),
                        Description = c.String(),
                        Remarks = c.String(),
                        Code = c.String(),
                        Status = c.Byte(nullable: false),
                        UserId = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.TicketId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueName = c.String(nullable: false),
                        Description = c.String(),
                        Priority = c.Boolean(nullable: false),
                        Status = c.Byte(nullable: false),
                        AttachmentFileId = c.Int(),
                        CompanyProjectId = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AttachmentFiles", t => t.AttachmentFileId)
                .ForeignKey("dbo.CompanyProjects", t => t.CompanyProjectId)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.AttachmentFileId)
                .Index(t => t.CompanyProjectId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketAssigns", "UserId", "dbo.Users");
            DropForeignKey("dbo.TicketAssigns", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.TicketAssigns", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Tickets", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Tickets", "CompanyProjectId", "dbo.CompanyProjects");
            DropForeignKey("dbo.Tickets", "AttachmentFileId", "dbo.AttachmentFiles");
            DropForeignKey("dbo.TicketAssigns", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.CompanyProjects", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.CompanyProjects", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.CompanyProjects", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Companies", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Companies", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.AttachmentFiles", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.AttachmentFiles", "CreatedBy", "dbo.Users");
            DropIndex("dbo.Tickets", new[] { "UpdatedBy" });
            DropIndex("dbo.Tickets", new[] { "CreatedBy" });
            DropIndex("dbo.Tickets", new[] { "CompanyProjectId" });
            DropIndex("dbo.Tickets", new[] { "AttachmentFileId" });
            DropIndex("dbo.TicketAssigns", new[] { "UpdatedBy" });
            DropIndex("dbo.TicketAssigns", new[] { "CreatedBy" });
            DropIndex("dbo.TicketAssigns", new[] { "UserId" });
            DropIndex("dbo.TicketAssigns", new[] { "TicketId" });
            DropIndex("dbo.CompanyProjects", new[] { "UpdatedBy" });
            DropIndex("dbo.CompanyProjects", new[] { "CreatedBy" });
            DropIndex("dbo.CompanyProjects", new[] { "CompanyId" });
            DropIndex("dbo.Companies", new[] { "UpdatedBy" });
            DropIndex("dbo.Companies", new[] { "CreatedBy" });
            DropIndex("dbo.AttachmentFiles", new[] { "UpdatedBy" });
            DropIndex("dbo.AttachmentFiles", new[] { "CreatedBy" });
            DropTable("dbo.Tickets");
            DropTable("dbo.TicketAssigns");
            DropTable("dbo.CompanyProjects");
            DropTable("dbo.Companies");
            DropTable("dbo.AttachmentFiles");
        }
    }
}
