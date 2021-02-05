namespace SmallReservationApp.Base.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewReservationElement1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "Contact_ContactId", "dbo.Contacts");
            DropIndex("dbo.Reservations", new[] { "Contact_ContactId" });
            RenameColumn(table: "dbo.Reservations", name: "Contact_ContactId", newName: "ContactId");
            AlterColumn("dbo.Reservations", "ContactId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "ContactId");
            AddForeignKey("dbo.Reservations", "ContactId", "dbo.Contacts", "ContactId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ContactId", "dbo.Contacts");
            DropIndex("dbo.Reservations", new[] { "ContactId" });
            AlterColumn("dbo.Reservations", "ContactId", c => c.Int());
            RenameColumn(table: "dbo.Reservations", name: "ContactId", newName: "Contact_ContactId");
            CreateIndex("dbo.Reservations", "Contact_ContactId");
            AddForeignKey("dbo.Reservations", "Contact_ContactId", "dbo.Contacts", "ContactId");
        }
    }
}
