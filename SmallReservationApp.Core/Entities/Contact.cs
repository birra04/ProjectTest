using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmallReservationApp.Core
{
    public class Contact
    {
        [Key]
        [Required]
        public int ContactId { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "NameRequired")]
        [MaxLength(150, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "NameLong")]
        public string Name { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Resources))]
        [MaxLength(16, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "PhoneNumberLong")]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(Resources.Resources),
                                     ErrorMessageResourceName = "PhoneNumberInvalid")]
        public string PhoneNumber { get; set; }

        [Display(Name = "BirthDay", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "BirthDayRequired")]
        public DateTime BirthDay { get; set; }

        [Required]
        public string ContactType { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
