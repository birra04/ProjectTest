using SmallReservationApp.Base;
using Uow.Package.Data.Repositories;

namespace Uow.Package.Data
{
    /// <summary>
    /// Unit of work provides access to repositories.  Operations on multiple repositories are atomic through
    /// the use of Commit().
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        private IContactRepository contact;
        private IReservationRepository reservation;

        public IContactRepository Contacts => contact ?? (contact = new ContactRepository(this.ReservationAppContext));
        public IReservationRepository Reservations => reservation ?? (reservation = new ReservationRepository(this.ReservationAppContext));

        public ReservationAppDBContext ReservationAppContext { get; }

        public UnitOfWork()
        {
            this.ReservationAppContext = new ReservationAppDBContext();
        }

        public UnitOfWork(ReservationAppDBContext reservationAppContext)
        {
            ReservationAppContext = reservationAppContext;
        }

        /// <summary>
        /// Commits changes made to all repositories
        /// </summary>
        public void Commit()
        {
            ReservationAppContext.SaveChanges();
        }

        public void Dispose()
        {
            ReservationAppContext.Dispose();
        }
    }
}
