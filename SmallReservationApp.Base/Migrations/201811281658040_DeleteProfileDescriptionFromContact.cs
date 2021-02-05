namespace SmallReservationApp.Base.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteProfileDescriptionFromContact : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contacts", "ProfileDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "ProfileDescription", c => c.String());
        }
    }
}
