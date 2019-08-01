namespace IST.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketEntityUpdate_WorkflowEntityAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PositionName = c.String(nullable: false),
                        ShortName = c.String(),
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
                "dbo.Workflows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordId = c.Int(nullable: false),
                        PositionId = c.Int(),
                        ApproverId = c.Int(),
                        Status = c.Byte(nullable: false),
                        ApprovalStatus = c.String(),
                        Remarks = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ApproverId)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Positions", t => t.PositionId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.PositionId)
                .Index(t => t.ApproverId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            AddColumn("dbo.Tickets", "ApprovedDate", c => c.DateTime());
            AddColumn("dbo.Companies", "ShortName", c => c.String());
            AddColumn("dbo.Companies", "MobileNo", c => c.String());
            DropColumn("dbo.Companies", "FaxNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "FaxNo", c => c.String());
            DropForeignKey("dbo.Workflows", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Workflows", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Workflows", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Workflows", "ApproverId", "dbo.Users");
            DropForeignKey("dbo.Positions", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Positions", "CreatedBy", "dbo.Users");
            DropIndex("dbo.Workflows", new[] { "UpdatedBy" });
            DropIndex("dbo.Workflows", new[] { "CreatedBy" });
            DropIndex("dbo.Workflows", new[] { "ApproverId" });
            DropIndex("dbo.Workflows", new[] { "PositionId" });
            DropIndex("dbo.Positions", new[] { "UpdatedBy" });
            DropIndex("dbo.Positions", new[] { "CreatedBy" });
            DropColumn("dbo.Companies", "MobileNo");
            DropColumn("dbo.Companies", "ShortName");
            DropColumn("dbo.Tickets", "ApprovedDate");
            DropTable("dbo.Workflows");
            DropTable("dbo.Positions");
        }
    }
}
