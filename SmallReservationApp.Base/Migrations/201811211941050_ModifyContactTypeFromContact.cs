namespace SmallReservationApp.Base.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyContactTypeFromContact : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "ContactType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "ContactType", c => c.Int(nullable: false));
        }
    }
}
