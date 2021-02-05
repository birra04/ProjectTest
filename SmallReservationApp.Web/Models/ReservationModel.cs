using SmallReservationApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallReservationApp.Web.Models
{
    public class ReservationModel
    {
        public int ReservationId { get; set; }

        public string Description { get; set; }

        public int? Ranking { get; set; }

        public DateTime ReservationDate { get; set; }

        public bool Favorite { get; set; }

        public int ContactId { get; set; }

        public ContactModel Contact { get; set; }
    }
}