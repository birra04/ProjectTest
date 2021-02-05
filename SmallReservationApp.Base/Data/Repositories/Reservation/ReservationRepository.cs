using SmallReservationApp.Base;
using SmallReservationApp.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Uow.Package.Data.Repositories
{
    public class ReservationRepository : IReservationRepository   
    {
        public ReservationAppDBContext Dbcontext { get; set; }

        public IQueryable<Reservation> Entities => throw new System.NotImplementedException();

        public ReservationRepository(ReservationAppDBContext dbContext)
        {
            Dbcontext = dbContext;
        }

        public IEnumerable<Reservation> GetByName(string pdescription)
        {
            return Dbcontext.Reservations.Where(c => c.Description.Contains(pdescription));
        }

        public Reservation GetByDescription(string pdescription)
        {
            return Dbcontext.Reservations.FirstOrDefault(desc => desc.Description == pdescription);
        }

        public IEnumerable<Reservation> GetAllReservationByContact(int id)
        {
            return Dbcontext.Reservations.Where(reservation => reservation.ContactId == id);
        }

        public Reservation GetById(int id)
        {
            return Dbcontext.Reservations.Where(r => r.ReservationId == id).Include(c => c.Contact).First();
        }

        public void Create(Reservation entity)
        {
            Dbcontext.Reservations.Add(entity);
        }

        public void Delete(Reservation entity)
        {
            Dbcontext.Reservations.Remove(entity);
        }

        public IEnumerable<Reservation> GetAll()
        {
            return Dbcontext.Reservations.Include(c => c.Contact).ToList();
        }

        public void Update(Reservation entity)
        {
            Dbcontext.Entry(entity).State = EntityState.Modified;
        }
    }
}