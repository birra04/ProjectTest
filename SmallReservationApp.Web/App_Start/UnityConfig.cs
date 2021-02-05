using SmallReservationApp.Base;
using SmallReservationApp.Control;
using SmallReservationApp.Control.Manager;
using System.Data.Entity;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Uow.Package.Data;
using Uow.Package.Data.Repositories;

namespace SmallReservationApp.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IContactManager, ContactManager>();
            container.RegisterType<IContactRepository, ContactRepository>();
            container.RegisterType<IReservationRepository, ReservationRepository>();
            container.RegisterType<IReservationManager, ReservationManager>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<DbContext, ReservationAppDBContext>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}