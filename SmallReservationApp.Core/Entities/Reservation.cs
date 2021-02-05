using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallReservationApp.Core
{
    public class Reservation
    {
        [Key]
        [Required]
        public int ReservationId { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
             ErrorMessageResourceName = "DescriptionRequired")]
        public string Description { get; set; }

        [Display(Name = "Ranking", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
             ErrorMessageResourceName = "RankingRequired")]
        [Range(0, 5, ErrorMessageResourceType = typeof(Resources.Resources),
                   ErrorMessageResourceName = "RankingRange")]
        public int? Ranking { get; set; }

        [Display(Name = "ReservationDate", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
             ErrorMessageResourceName = "ReservationDateRequired")]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "Favorite", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
       ErrorMessageResourceName = "FavoriteRequired")]
        public bool Favorite { get; set; }

        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        public /*virtual*/ Contact Contact { get; set; }
    }
}
