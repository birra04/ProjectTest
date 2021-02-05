using SmallReservationApp.Core;
using System.Collections.Generic;

namespace SmallReservationApp.Control
{
    public interface IContactManager
    {
        Contact GetContact(int id);
        Contact AddOrEditContact(Contact contact);
        IEnumerable<Contact> GetContacts();
        void DeleteContact(int id);
    }
}
