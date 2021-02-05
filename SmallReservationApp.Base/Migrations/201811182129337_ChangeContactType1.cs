namespace SmallReservationApp.Base.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeContactType1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "ContactType_ContactTypeId", c => c.Int());
            CreateIndex("dbo.Contacts", "ContactType_ContactTypeId");
            AddForeignKey("dbo.Contacts", "ContactType_ContactTypeId", "dbo.ContactTypes", "ContactTypeId");
            DropColumn("dbo.Contacts", "ContactType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "ContactType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Contacts", "ContactType_ContactTypeId", "dbo.ContactTypes");
            DropIndex("dbo.Contacts", new[] { "ContactType_ContactTypeId" });
            DropColumn("dbo.Contacts", "ContactType_ContactTypeId");
        }
    }
}
