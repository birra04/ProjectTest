using AutoMapper;
using SmallReservationApp.Control.Manager;
using SmallReservationApp.Core;
using SmallReservationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace SmallReservationApp.Web.Controllers
{
    public class ReservationController : BaseController
    {
        private IReservationManager ReservationManager { get; set; }

        private IEnumerable<ReservationModel> reservationList;

        public ReservationController(IReservationManager reservationManager)
        {
            ReservationManager = reservationManager;
        }

        // GET: ReservationModel
        public ActionResult IndexListReservation([Form] QueryOptions queryOptions)
        {
            reservationList = Mapper.Map<IEnumerable<ReservationModel>>(ReservationManager.GetSortList(ref queryOptions));
            TempData["reservationList"] = reservationList;
            ViewBag.QueryOptions = queryOptions;
            return View();
        }

        // GET: ReservationModel/CreateReservation
        public ActionResult CreateReservation(ReservationModel reservation)
        {
            if (reservation == null)
            {
                reservation = new ReservationModel();
            }
            if (TempData["reservation"] != null)
            {
                reservation = (ReservationModel)TempData["reservation"];
            }

            return View(reservation);
        }

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReservations([Form] QueryOptions queryOptions)
        {
            reservationList = (IEnumerable<ReservationModel>)TempData["reservationList"];
            ViewBag.QueryOptions = queryOptions;
            return Json(reservationList, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Get reservation by Id
        /// </summary>
        /// <param name="id">Reservation id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetReservation(int? id)
        {
            ReservationModel reservation = id != null ? Mapper.Map<ReservationModel>(ReservationManager.GetReservation(id.Value)) : null;
            TempData["reservation"] = reservation;
            return RedirectToAction("CreateReservation", "Reservation");
        }

        /// <summary>
        /// Add or Edit a new reservation
        /// </summary>
        /// <param name="reservations">reservation model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditReservation(ReservationModel reservations)
        {
            Reservation reservationMapped = Mapper.Map<Reservation>(reservations);
            try
            {
                ReservationModel reservation = Mapper.Map<ReservationModel>(ReservationManager.AddReservation(reservationMapped));
                string redirectUrl = new UrlHelper(Request.RequestContext).Action("IndexListReservation");

                return Json(new { reservation, url = redirectUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Add reservation to favorites
        /// </summary>
        /// <param name="reservations"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddToFavorites(ReservationModel reservations, [Form] QueryOptions queryOptions)
        {
            Reservation reservationMapped = Mapper.Map<Reservation>(reservations);
            try
            {
                ReservationModel reservation = Mapper.Map<ReservationModel>(ReservationManager.AddFavoritesToReservation(reservationMapped));
                string redirectUrl = new UrlHelper(Request.RequestContext).Action("IndexListReservation");
                ViewBag.QueryOptions = queryOptions;
                return Json(new { reservation, url = redirectUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Add reservation ranking
        /// </summary>
        /// <param name="reservations"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateRanking(ReservationModel reservations)
        {
            Reservation reservationMapped = Mapper.Map<Reservation>(reservations);
            try
            {
                ReservationModel reservation = Mapper.Map<ReservationModel>(ReservationManager.UpdateRanking(reservationMapped));
                string redirectUrl = new UrlHelper(Request.RequestContext).Action("IndexListReservation");

                return Json(new { reservation, url = redirectUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete a reservation
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteReservation(int reservationId)
        {
            try
            {
                ReservationManager.DeleteReservation(reservationId);
                string redirectUrl = new UrlHelper(Request.RequestContext).Action("IndexListReservation");
                return Json(new { url = redirectUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public HtmlString AutoCompleteOptions(string query)
        {
            IEnumerable<ContactModel> listContact = Mapper.Map<IEnumerable<ContactModel>>(ReservationManager.AutoCompleteList(query));
            HtmlString result = HtmlHelperExtensions.HtmlConvertToJson(null, listContact);
            return result;
        }
    }
}