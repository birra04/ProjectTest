namespace SmallReservationApp.Base.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataEntitiesModifyContactId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "Contact_ContactId", "dbo.Contacts");
            DropIndex("dbo.Reservations", new[] { "Contact_ContactId" });
            DropPrimaryKey("dbo.Contacts");
            AlterColumn("dbo.Contacts", "ContactId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Reservations", "Contact_ContactId", c => c.Int());
            AddPrimaryKey("dbo.Contacts", "ContactId");
            CreateIndex("dbo.Reservations", "Contact_ContactId");
            AddForeignKey("dbo.Reservations", "Contact_ContactId", "dbo.Contacts", "ContactId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "Contact_ContactId", "dbo.Contacts");
            DropIndex("dbo.Reservations", new[] { "Contact_ContactId" });
            DropPrimaryKey("dbo.Contacts");
            AlterColumn("dbo.Reservations", "Contact_ContactId", c => c.Long());
            AlterColumn("dbo.Contacts", "ContactId", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Contacts", "ContactId");
            CreateIndex("dbo.Reservations", "Contact_ContactId");
            AddForeignKey("dbo.Reservations", "Contact_ContactId", "dbo.Contacts", "ContactId");
        }
    }
}
