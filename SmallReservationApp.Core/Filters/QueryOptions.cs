using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallReservationApp.Core
{
    public class QueryOptions
    {
        public QueryOptions()
        {

            CurrentPage = 1;
            PageSize = 4;

            SortField = "ReservationId";
            SortOrder = SortOrder.ASC;
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public SortOrder SortOrder { get; set; }
        public string Sort
        {
            get
            {
                return string.Format("{0} {1}",
                SortField, SortOrder.ToString());
            }
        }
    }
}