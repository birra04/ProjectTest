namespace SmallReservationApp.Base.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeContactType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "ContactType_ContactTypeId", "dbo.ContactTypes");
            DropIndex("dbo.Contacts", new[] { "ContactType_ContactTypeId" });
            AddColumn("dbo.Contacts", "ContactType", c => c.Int(nullable: false));
            DropColumn("dbo.Contacts", "ContactType_ContactTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "ContactType_ContactTypeId", c => c.Int());
            DropColumn("dbo.Contacts", "ContactType");
            CreateIndex("dbo.Contacts", "ContactType_ContactTypeId");
            AddForeignKey("dbo.Contacts", "ContactType_ContactTypeId", "dbo.ContactTypes", "ContactTypeId");
        }
    }
}
