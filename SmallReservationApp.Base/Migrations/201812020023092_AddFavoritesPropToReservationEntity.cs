namespace SmallReservationApp.Base.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavoritesPropToReservationEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Favorites", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "Favorites");
        }
    }
}
