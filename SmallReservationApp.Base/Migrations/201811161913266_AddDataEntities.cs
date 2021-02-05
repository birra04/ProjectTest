namespace SmallReservationApp.Base.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddDataEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                {
                    ContactId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 150),
                    PhoneNumber = c.String(nullable: false, maxLength: 20),
                    BirthDate = c.DateTime(nullable: false),
                    ProfileDescription = c.String(),
                    ContactType_ContactTypeId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.ContactTypes", t => t.ContactType_ContactTypeId)
                .Index(t => t.ContactType_ContactTypeId);

            CreateTable(
                "dbo.ContactTypes",
                c => new
                {
                    ContactTypeId = c.Int(nullable: false, identity: true),
                    Value = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ContactTypeId);

            CreateTable(
                "dbo.Reservations",
                c => new
                {
                    ReservationId = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false),
                    Contact_ContactId = c.Int(),
                })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId)
                .Index(t => t.Contact_ContactId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "ContactType_ContactTypeId", "dbo.ContactTypes");
            DropIndex("dbo.Reservations", new[] { "Contact_ContactId" });
            DropIndex("dbo.Contacts", new[] { "ContactType_ContactTypeId" });
            DropTable("dbo.Reservations");
            DropTable("dbo.ContactTypes");
            DropTable("dbo.Contacts");
        }
    }
}

