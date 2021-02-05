using AutoMapper;
using SmallReservationApp.Control;
using SmallReservationApp.Core;
using SmallReservationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SmallReservationApp.Web.Controllers
{
    public class ContactController : BaseController
    {
        private IContactManager ContactManager { get; set; }

        public ContactController(IContactManager contactManager)
        {
            ContactManager = contactManager;
        }

        // GET: ContactModel
        public ActionResult IndexListContact()
        {
            return View();
        }

        // GET: ContactModel/CreateContact
        public ActionResult CreateContact(ContactModel contact)
        {
            if (contact == null)
            {
                contact = new ContactModel();
            }
            if (TempData["contact"] != null)
            {
                contact = (ContactModel)TempData["contact"];
            }
            return View(contact);
        }

        /// <summary>
        /// Get all contacts.
        /// </summary>
        /// <returns>Return a list of contacts</returns>
        public ActionResult GetContacts()
        {
            IEnumerable<ContactModel> a = Mapper.Map<IEnumerable<ContactModel>>(ContactManager.GetContacts());
            foreach (ContactModel item in a)
            {
                switch (item.ContactType)
                {
                    case "1":
                        item.ContactType = string.Format("Contact Type {0}", item.ContactType);
                        break;
                    case "2":
                        item.ContactType = string.Format("Contact Type {0}", item.ContactType);
                        break;
                    case "3":
                        item.ContactType = string.Format("Contact Type {0}", item.ContactType);
                        break;
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get contact by id.
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>Return contact data</returns>
        [HttpGet]
        public ActionResult GetContact(int? id)
        {
            ContactModel a = id != null ? Mapper.Map<ContactModel>(ContactManager.GetContact(id.Value)) : null;
            TempData["contact"] = a;
            return RedirectToAction("CreateContact", "Contact");
        }

        /// <summary>
        /// Add or edit a contact.
        /// </summary>
        /// <param name="contacts">Contact Model</param>
        /// <returns>Edit or add a contact</returns>
        [HttpPost]
        public ActionResult EditContact(ContactModel contacts)
        {
            Contact contactMapped = Mapper.Map<Contact>(contacts);
            try
            {
                Contact contact = ContactManager.AddOrEditContact(contactMapped);

                string redirectUrl = new UrlHelper(Request.RequestContext).Action("IndexListContact");
                return Json(new { contact, url = redirectUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete a contact.
        /// </summary>
        /// <param name="contactId">Contact id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteContact(int contactId)
        {
            try
            {
                ContactManager.DeleteContact(contactId);
                string redirectUrl = new UrlHelper(Request.RequestContext).Action("IndexListContact");
                return Json(new { url = redirectUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }
    }
}