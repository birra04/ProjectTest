using AutoMapper;
using SmallReservationApp.Core;
using SmallReservationApp.Web.Models;

namespace SmallReservationApp.Web.App_Start
{
    public static class Mapping
    {
        public static void InitProfile()
        {
            Mapper.Initialize(mapconfig =>
            {
                mapconfig.CreateMap<Contact, ContactModel>().ForMember(c => c.BirthDate, opt => opt.MapFrom(c1 => c1.BirthDay.ToString("MM/dd/yyyy")));
                mapconfig.CreateMap<ContactModel, Contact>().ForMember(c => c.BirthDay, opt => opt.MapFrom(c2 => c2.BirthDate));
                mapconfig.CreateMap<Reservation, ReservationModel>().ForMember(c => c.ReservationDate, opt => opt.MapFrom(c1 => c1.ReservationDate.ToString("MM/dd/yyyy")));
                mapconfig.CreateMap<ReservationModel, Reservation>();
            });
        }
    }
}