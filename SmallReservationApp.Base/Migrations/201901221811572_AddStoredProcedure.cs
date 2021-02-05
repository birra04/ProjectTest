namespace SmallReservationApp.Base.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStoredProcedure : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Contact_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 150),
                        PhoneNumber = p.String(maxLength: 20),
                        BirthDate = p.DateTime(),
                        ContactType = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Contacts]([Name], [PhoneNumber], [BirthDate], [ContactType])
                      VALUES (@Name, @PhoneNumber, @BirthDate, @ContactType)
                      
                      DECLARE @ContactId int
                      SELECT @ContactId = [ContactId]
                      FROM [dbo].[Contacts]
                      WHERE @@ROWCOUNT > 0 AND [ContactId] = scope_identity()
                      
                      SELECT t0.[ContactId]
                      FROM [dbo].[Contacts] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ContactId] = @ContactId"
            );
            
            CreateStoredProcedure(
                "dbo.Contact_Update",
                p => new
                    {
                        ContactId = p.Int(),
                        Name = p.String(maxLength: 150),
                        PhoneNumber = p.String(maxLength: 20),
                        BirthDate = p.DateTime(),
                        ContactType = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Contacts]
                      SET [Name] = @Name, [PhoneNumber] = @PhoneNumber, [BirthDate] = @BirthDate, [ContactType] = @ContactType
                      WHERE ([ContactId] = @ContactId)"
            );
            
            CreateStoredProcedure(
                "dbo.Contact_Delete",
                p => new
                    {
                        ContactId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Contacts]
                      WHERE ([ContactId] = @ContactId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Contact_Delete");
            DropStoredProcedure("dbo.Contact_Update");
            DropStoredProcedure("dbo.Contact_Insert");
        }
    }
}
