namespace EIST.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMgt : DbMigration
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
                        IssueId = c.Int(),
                        CommentId = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Issues", t => t.IssueId)
                .ForeignKey("dbo.IssueCommentLog", t => t.CommentId)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.IssueId)
                .Index(t => t.CommentId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.IssueCommentLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueId = c.Int(),
                        Comment = c.String(),
                        Status = c.Byte(nullable: false),
                        StepOrder = c.Byte(nullable: false),
                        IsInvalid = c.Boolean(nullable: false),
                        Internal = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Issues", t => t.IssueId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.IssueId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Email = c.String(),
                        Password = c.String(nullable: false),
                        Mobile = c.String(),
                        Address = c.String(),
                        Gender = c.String(),
                        SupUser = c.Boolean(nullable: false),
                        UserType = c.String(),
                        ImageFile = c.String(),
                        RoleId = c.Int(),
                        CompanyId = c.Int(),
                        PositionId = c.Int(),
                        LastPassword = c.String(),
                        LastPassChangeDate = c.DateTime(),
                        PasswordChangedCount = c.Int(),
                        SecurityStamp = c.String(),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(),
                        Status = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Positions", t => t.PositionId)
                .ForeignKey("dbo.UserRoles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.CompanyId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ShortName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        MobileNo = c.String(),
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
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PositionName = c.String(nullable: false),
                        ShortName = c.String(),
                        IsTicketProcess = c.Boolean(nullable: false),
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
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        Status = c.Int(),
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
                "dbo.UserRolePermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        Permission = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        IssueTitle = c.String(nullable: false),
                        Description = c.String(),
                        Priority = c.Boolean(nullable: false),
                        Status = c.Byte(nullable: false),
                        ApprovedDate = c.DateTime(),
                        ProjectId = c.Int(),
                        LabelId = c.Int(),
                        Milestone = c.String(),
                        IsClosed = c.Boolean(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.IssueLabels", t => t.LabelId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.ProjectId)
                .Index(t => t.LabelId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.IssueLabels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LabelTitle = c.String(nullable: false),
                        ColorCode = c.String(),
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
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CompanyId = c.Int(),
                        PmId = c.Int(),
                        SuperVisorId = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.PmId)
                .ForeignKey("dbo.Users", t => t.SuperVisorId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CompanyId)
                .Index(t => t.PmId)
                .Index(t => t.SuperVisorId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.TicketAssigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueId = c.Int(),
                        Description = c.String(),
                        Status = c.Byte(nullable: false),
                        AssigneeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AssigneeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Issues", t => t.IssueId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.IssueId)
                .Index(t => t.AssigneeId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        AuditLogId = c.Guid(nullable: false),
                        EventType = c.String(),
                        TableName = c.String(nullable: false),
                        PrimaryKeyName = c.String(),
                        PrimaryKeyValue = c.String(),
                        ColumnName = c.String(),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        CreatedUser = c.Int(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.AuditLogId)
                .ForeignKey("dbo.Users", t => t.CreatedUser)
                .Index(t => t.CreatedUser);
            
            CreateTable(
                "dbo.CustomerUserProjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.LoginRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 1000),
                        SessionId = c.String(maxLength: 1000),
                        LoggedIn = c.Boolean(nullable: false),
                        LoggedInDateTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workflows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormName = c.String(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workflows", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Workflows", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Workflows", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Workflows", "ApproverId", "dbo.Users");
            DropForeignKey("dbo.CustomerUserProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.CustomerUserProjects", "UserId", "dbo.Users");
            DropForeignKey("dbo.AuditLogs", "CreatedUser", "dbo.Users");
            DropForeignKey("dbo.AttachmentFiles", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.AttachmentFiles", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.AttachmentFiles", "CommentId", "dbo.IssueCommentLog");
            DropForeignKey("dbo.IssueCommentLog", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.IssueCommentLog", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.Issues", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.TicketAssigns", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.TicketAssigns", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.TicketAssigns", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.TicketAssigns", "AssigneeId", "dbo.Users");
            DropForeignKey("dbo.Projects", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Issues", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "SuperVisorId", "dbo.Users");
            DropForeignKey("dbo.Projects", "PmId", "dbo.Users");
            DropForeignKey("dbo.Projects", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Projects", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Issues", "LabelId", "dbo.IssueLabels");
            DropForeignKey("dbo.IssueLabels", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.IssueLabels", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Issues", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.AttachmentFiles", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.IssueCommentLog", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoles", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.UserRolePermissions", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoles", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Users", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Positions", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Positions", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Companies", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Companies", "CreatedBy", "dbo.Users");
            DropIndex("dbo.Workflows", new[] { "UpdatedBy" });
            DropIndex("dbo.Workflows", new[] { "CreatedBy" });
            DropIndex("dbo.Workflows", new[] { "ApproverId" });
            DropIndex("dbo.Workflows", new[] { "PositionId" });
            DropIndex("dbo.CustomerUserProjects", new[] { "ProjectId" });
            DropIndex("dbo.CustomerUserProjects", new[] { "UserId" });
            DropIndex("dbo.AuditLogs", new[] { "CreatedUser" });
            DropIndex("dbo.TicketAssigns", new[] { "UpdatedBy" });
            DropIndex("dbo.TicketAssigns", new[] { "CreatedBy" });
            DropIndex("dbo.TicketAssigns", new[] { "AssigneeId" });
            DropIndex("dbo.TicketAssigns", new[] { "IssueId" });
            DropIndex("dbo.Projects", new[] { "UpdatedBy" });
            DropIndex("dbo.Projects", new[] { "CreatedBy" });
            DropIndex("dbo.Projects", new[] { "SuperVisorId" });
            DropIndex("dbo.Projects", new[] { "PmId" });
            DropIndex("dbo.Projects", new[] { "CompanyId" });
            DropIndex("dbo.IssueLabels", new[] { "UpdatedBy" });
            DropIndex("dbo.IssueLabels", new[] { "CreatedBy" });
            DropIndex("dbo.Issues", new[] { "UpdatedBy" });
            DropIndex("dbo.Issues", new[] { "CreatedBy" });
            DropIndex("dbo.Issues", new[] { "LabelId" });
            DropIndex("dbo.Issues", new[] { "ProjectId" });
            DropIndex("dbo.UserRolePermissions", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UpdatedBy" });
            DropIndex("dbo.UserRoles", new[] { "CreatedBy" });
            DropIndex("dbo.Positions", new[] { "UpdatedBy" });
            DropIndex("dbo.Positions", new[] { "CreatedBy" });
            DropIndex("dbo.Companies", new[] { "UpdatedBy" });
            DropIndex("dbo.Companies", new[] { "CreatedBy" });
            DropIndex("dbo.Users", new[] { "PositionId" });
            DropIndex("dbo.Users", new[] { "CompanyId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.IssueCommentLog", new[] { "UpdatedBy" });
            DropIndex("dbo.IssueCommentLog", new[] { "CreatedBy" });
            DropIndex("dbo.IssueCommentLog", new[] { "IssueId" });
            DropIndex("dbo.AttachmentFiles", new[] { "UpdatedBy" });
            DropIndex("dbo.AttachmentFiles", new[] { "CreatedBy" });
            DropIndex("dbo.AttachmentFiles", new[] { "CommentId" });
            DropIndex("dbo.AttachmentFiles", new[] { "IssueId" });
            DropTable("dbo.Workflows");
            DropTable("dbo.LoginRecords");
            DropTable("dbo.CustomerUserProjects");
            DropTable("dbo.AuditLogs");
            DropTable("dbo.TicketAssigns");
            DropTable("dbo.Projects");
            DropTable("dbo.IssueLabels");
            DropTable("dbo.Issues");
            DropTable("dbo.UserRolePermissions");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Positions");
            DropTable("dbo.Companies");
            DropTable("dbo.Users");
            DropTable("dbo.IssueCommentLog");
            DropTable("dbo.AttachmentFiles");
        }
    }
}
