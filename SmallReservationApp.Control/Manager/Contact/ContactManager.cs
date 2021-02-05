using SmallReservationApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Uow.Package.Data;

namespace SmallReservationApp.Control
{
    public class ContactManager : IContactManager
    {
        private readonly IUnitOfWork unitOfWork;

        public ContactManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Contact GetContact(int id)
        {
            Contact contact = unitOfWork.Contacts.GetById(id);
            return contact;
        }

        public Contact AddOrEditContact(Contact contact)
        {
            Contact cont = unitOfWork.Contacts.GetById(contact.ContactId);
            if (cont == null)
            {
                unitOfWork.Contacts.Create(contact);
                unitOfWork.Reservations.Create(
                    new Reservation
                    {
                        ContactId = 0,
                        ReservationDate = DateTime.Now,
                    }
                    );
                unitOfWork.Commit();
                return unitOfWork.Contacts.GetById(contact.ContactId);
            }
            // Edit contact
            cont.ContactType = contact.ContactType;
            cont.BirthDay = contact.BirthDay;
            cont.Name = contact.Name;
            cont.PhoneNumber = contact.PhoneNumber;
            unitOfWork.Contacts.Update(cont);

            unitOfWork.Commit();

            return unitOfWork.Contacts.GetById(contact.ContactId);
        }

        public IEnumerable<Contact> GetContacts()
        {
            List<Contact> list = unitOfWork.Contacts.GetAll().ToList();
            return list;
        }

        public void DeleteContact(int id)
        {
            Contact contact = unitOfWork.Contacts.GetById(id);
            List<Reservation> reservation = unitOfWork.Reservations.GetAllReservationByContact(id).ToList();
            if (reservation.Count > 0)
            {
                foreach (Reservation item in reservation)
                {
                    unitOfWork.Reservations.Delete(item);
                }
                unitOfWork.Commit();
            }
            unitOfWork.Contacts.Delete(contact);
            unitOfWork.Commit();
        }
    }
}
