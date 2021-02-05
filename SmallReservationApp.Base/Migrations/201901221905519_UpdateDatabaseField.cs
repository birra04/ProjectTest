namespace SmallReservationApp.Base.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabaseField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "BirthDay", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservations", "Favorite", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Contacts", "PhoneNumber", c => c.String(maxLength: 16));
            DropColumn("dbo.Contacts", "BirthDate");
            DropColumn("dbo.Reservations", "Favorites");
            AlterStoredProcedure(
                "dbo.Contact_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 150),
                        PhoneNumber = p.String(maxLength: 16),
                        BirthDay = p.DateTime(),
                        ContactType = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Contacts]([Name], [PhoneNumber], [BirthDay], [ContactType])
                      VALUES (@Name, @PhoneNumber, @BirthDay, @ContactType)
                      
                      DECLARE @ContactId int
                      SELECT @ContactId = [ContactId]
                      FROM [dbo].[Contacts]
                      WHERE @@ROWCOUNT > 0 AND [ContactId] = scope_identity()
                      
                      SELECT t0.[ContactId]
                      FROM [dbo].[Contacts] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ContactId] = @ContactId"
            );
            
            AlterStoredProcedure(
                "dbo.Contact_Update",
                p => new
                    {
                        ContactId = p.Int(),
                        Name = p.String(maxLength: 150),
                        PhoneNumber = p.String(maxLength: 16),
                        BirthDay = p.DateTime(),
                        ContactType = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Contacts]
                      SET [Name] = @Name, [PhoneNumber] = @PhoneNumber, [BirthDay] = @BirthDay, [ContactType] = @ContactType
                      WHERE ([ContactId] = @ContactId)"
            );
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "Favorites", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contacts", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Contacts", "PhoneNumber", c => c.String(maxLength: 20));
            DropColumn("dbo.Reservations", "Favorite");
            DropColumn("dbo.Contacts", "BirthDay");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
