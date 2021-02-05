using SmallReservationApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using Uow.Package.Data;

namespace SmallReservationApp.Control.Manager
{
    public class ReservationManager : IReservationManager
    {
        private readonly IUnitOfWork unitOfWork;

        public ReservationManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get reservation by id
        /// </summary>
        /// <param name="id">Reservation id</param>
        /// <returns></returns>
        public Reservation GetReservation(int id)
        {
            Reservation res = unitOfWork.Reservations.GetById(id);
            return res;
        }

        /// <summary>
        /// Add or Edit a reservation
        /// </summary>
        /// <param name="reservation">Reservation entity</param>
        /// <returns></returns>
        public Reservation AddReservation(Reservation reservation)
        {
            Contact contact = unitOfWork.Contacts.GetAll().FirstOrDefault(id => id.ContactId == reservation.ContactId);
            if (contact == null)
            {
                Contact newContact = new Contact
                {
                    Name = reservation.Contact.Name,
                    ContactType = reservation.Contact.ContactType,
                    PhoneNumber = reservation.Contact.PhoneNumber,
                    BirthDay = reservation.Contact.BirthDay
                };
                unitOfWork.Contacts.Create(newContact);

                unitOfWork.Reservations.Create(
                    new Reservation
                    {
                        ContactId = unitOfWork.Contacts.GetAll().Last().ContactId,
                        ReservationDate = DateTime.Now,
                        Description = reservation.Description,
                        Ranking = 0,
                        Favorite = false
                    }
                    );
                unitOfWork.Commit();
                return unitOfWork.Reservations.GetByDescription(reservation.Description);
            }
            // Edit contact
            unitOfWork.Contacts.Update(reservation.Contact);
            //Edit Reservation
            Reservation reservationUpdate = unitOfWork.Reservations.GetAll().FirstOrDefault(id => id.ReservationId == reservation.ReservationId);
            //reservationUpdate.Contact = reservation.Contact;
            reservationUpdate.Description = reservation.Description;

            unitOfWork.Reservations.Update(reservationUpdate);
            unitOfWork.Commit();

            return GetReservation(reservation.ReservationId);
        }

        /// <summary>
        /// Add to favorite a reservation.
        /// </summary>
        /// <param name="reservation">Reservation entity</param>
        /// <returns></returns>
        public Reservation AddFavoritesToReservation(Reservation reservation)
        {
            Reservation reservationEntity = unitOfWork.Reservations.GetById(reservation.ReservationId);

            if (reservationEntity.Favorite)
            {
                reservationEntity.Favorite = false;
            }
            else
            {
                reservationEntity.Favorite = true;
            }

            unitOfWork.Reservations.Update(reservationEntity);
            unitOfWork.Commit();

            return reservationEntity;
        }

        /// <summary>
        /// Add to favorite a reservation.
        /// </summary>
        /// <param name="reservation">Reservation entity</param>
        /// <returns></returns>
        public Reservation UpdateRanking(Reservation reservation)
        {
            Reservation reservationEntity = unitOfWork.Reservations.GetById(reservation.ReservationId);
            if (reservationEntity != null)
            {
                if (reservation.Ranking.HasValue)
                {
                    reservationEntity.Ranking = (int)reservation.Ranking;
                }
            }
            unitOfWork.Commit();

            return reservationEntity;
        }

        public IEnumerable<Reservation> GetReservations()
        {
            IEnumerable<Reservation> list = unitOfWork.Reservations.GetAll();
            return list;
        }

        /// <summary>
        /// Delete reservation
        /// </summary>
        /// <param name="id">Reservation id</param>
        public void DeleteReservation(int id)
        {
            Reservation reservation = unitOfWork.Reservations.GetById(id);
            unitOfWork.Reservations.Delete(reservation);
            unitOfWork.Commit();
        }

        public IEnumerable<Reservation> GetSortList(ref QueryOptions queryOptions)
        {
            int start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
            List<Reservation> reservations = unitOfWork.Reservations.GetAll().OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.PageSize).ToList();

            queryOptions.TotalPages = (int)Math.Ceiling((double)unitOfWork.Reservations.GetAll().Count() / queryOptions.PageSize);
            return reservations;
        }

        public IEnumerable<Contact> AutoCompleteList(string searchTerm)
        {
            return unitOfWork.Contacts.GetAutoCompleteList(searchTerm);
        }
    }
}
