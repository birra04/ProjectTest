namespace SmallReservationApp.Base
{
    using SmallReservationApp.Core;
    using System.Data.Entity;
    using Uow.Package.Data;

    public class ReservationAppDBContext : DbContext
    {
        public ReservationAppDBContext()
    : base("ReservationAppDBContext")
        {
            Database.SetInitializer(new DatabaseInitialiser());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().MapToStoredProcedures();
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactType { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}