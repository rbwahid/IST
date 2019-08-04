namespace IST.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketAssignEntityUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Code", c => c.String());
            AddColumn("dbo.TicketAssigns", "ApprovedDate", c => c.DateTime());
            DropColumn("dbo.TicketAssigns", "Remarks");
            DropColumn("dbo.TicketAssigns", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketAssigns", "Code", c => c.String());
            AddColumn("dbo.TicketAssigns", "Remarks", c => c.String());
            DropColumn("dbo.TicketAssigns", "ApprovedDate");
            DropColumn("dbo.Tickets", "Code");
        }
    }
}
