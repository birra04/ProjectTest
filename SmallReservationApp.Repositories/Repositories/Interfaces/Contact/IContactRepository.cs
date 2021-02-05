using SmallReservationApp.Core;
using System.Collections.Generic;

namespace Uow.Package.Data.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        IEnumerable<Contact> GetByName(string name);
        IEnumerable<Contact> GetAutoCompleteList(string searchTerm);
    }
}