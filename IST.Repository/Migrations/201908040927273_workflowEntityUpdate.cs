namespace IST.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workflowEntityUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workflows", "FormName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workflows", "FormName");
        }
    }
}
