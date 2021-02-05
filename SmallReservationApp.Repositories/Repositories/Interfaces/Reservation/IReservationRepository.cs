using SmallReservationApp.Core;
using System.Collections.Generic;
using System.Linq;

namespace Uow.Package.Data.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IEnumerable<Reservation> GetByName(string name);
        Reservation GetByDescription(string pdescription);
        IEnumerable<Reservation> GetAllReservationByContact(int id);
    }
}