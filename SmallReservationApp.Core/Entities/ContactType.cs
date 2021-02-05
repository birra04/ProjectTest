using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallReservationApp.Core
{
    public class ContactType
    {
        [Key]
        [Required]
        public int ContactTypeId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
