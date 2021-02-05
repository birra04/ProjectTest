namespace SmallReservationApp.Base.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewReservationElement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Ranking", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "ReservationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Contacts", "PhoneNumber", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "PhoneNumber", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Reservations", "ReservationDate");
            DropColumn("dbo.Reservations", "Ranking");
        }
    }
}
