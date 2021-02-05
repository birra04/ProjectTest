using SmallReservationApp.Core;
using System.Collections.Generic;

namespace SmallReservationApp.Control.Manager
{
    public interface IReservationManager
    {
        Reservation GetReservation(int id);
        Reservation AddReservation(Reservation reservation);
        IEnumerable<Reservation> GetReservations();
        IEnumerable<Reservation> GetSortList(ref QueryOptions queryOptions);
        void DeleteReservation(int id);
        Reservation AddFavoritesToReservation(Reservation reservation);
        Reservation UpdateRanking(Reservation reservation);
        IEnumerable<Contact> AutoCompleteList(string searchTerm);


    }
}
