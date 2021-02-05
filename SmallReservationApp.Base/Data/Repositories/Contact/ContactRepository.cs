using SmallReservationApp.Base;
using SmallReservationApp.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Uow.Package.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        public ReservationAppDBContext Dbcontext { get; set; }

        public IQueryable<Contact> Entities => throw new NotImplementedException();

        public ContactRepository(ReservationAppDBContext context)
        {
            Dbcontext = context;
        }

        public void Create(Contact entity)
        {
            Dbcontext.Database.ExecuteSqlCommand("exec Contact_Insert @Name, @PhoneNumber, @BirthDay, @ContactType",
                                       new SqlParameter("@Name", entity.Name),
                                       new SqlParameter("@PhoneNumber", (object)entity.PhoneNumber ?? DBNull.Value),
                                       new SqlParameter("@BirthDay", entity.BirthDay),
                                       new SqlParameter("@ContactType", entity.ContactType));
        }

        public void Update(Contact entity)
        {
            Dbcontext.Database.ExecuteSqlCommand("exec Contact_Update @ContactId, @Name, @PhoneNumber, @BirthDay, @ContactType",
                                       new SqlParameter("@ContactId", entity.ContactId),
                                       new SqlParameter("@Name", entity.Name),
                                       new SqlParameter("@PhoneNumber", (object)entity.PhoneNumber ?? DBNull.Value),
                                       new SqlParameter("@BirthDay", entity.BirthDay),
                                       new SqlParameter("@ContactType", entity.ContactType));
        }

        public void Delete(Contact entity)
        {
            Dbcontext.Database.ExecuteSqlCommand("exec Contact_Delete @ContactId",
                                       new SqlParameter("@ContactId", entity.ContactId));
        }

        public IEnumerable<Contact> GetByName(string name)
        {
            return Dbcontext.Contacts.Where(c => c.Name.Contains(name));
        }

        public Contact GetById(int id)
        {
            return Dbcontext.Contacts.Where(c => c.ContactId == id).First();
        }

        public IEnumerable<Contact> GetAll()
        {
            return Dbcontext.Contacts.ToList();
        }

        public IEnumerable<Contact> GetAutoCompleteList(string searchTerm)
        {
            return Dbcontext.Contacts.Where(c => c.Name.Contains(searchTerm)).ToList();
        }

    }
}