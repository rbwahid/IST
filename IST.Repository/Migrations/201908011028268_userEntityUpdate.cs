namespace IST.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userEntityUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CompanyId", c => c.Int());
            AddColumn("dbo.Users", "PositionId", c => c.Int());
            AddColumn("dbo.Positions", "IsTicketProcess", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Users", "CompanyId");
            CreateIndex("dbo.Users", "PositionId");
            AddForeignKey("dbo.Users", "CompanyId", "dbo.Companies", "Id");
            AddForeignKey("dbo.Users", "PositionId", "dbo.Positions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Users", new[] { "PositionId" });
            DropIndex("dbo.Users", new[] { "CompanyId" });
            DropColumn("dbo.Positions", "IsTicketProcess");
            DropColumn("dbo.Users", "PositionId");
            DropColumn("dbo.Users", "CompanyId");
        }
    }
}
