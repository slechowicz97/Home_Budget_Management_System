using HBM.Areas.Dashboard.ViewModels;
using HBM.Entities;
using HBM.Services;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBM.Areas.Dashboard.Controllers
{
    public class NotificationsController : Controller
    {
        // GET: Dashboard/Notifications
        NotificationsServices notificationsServices = new NotificationsServices();
        public ActionResult Index(string searchTerm, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            NotificationsListingModel model = new NotificationsListingModel();

            model.SearchTerm = searchTerm;

            model.Notifications = notificationsServices.SearchNotifications(searchTerm, page.Value, recordSize);

            var totalRecords = notificationsServices.SearchNotificationsCount(searchTerm);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        public ActionResult GetPopUp(NotificationsListingModel model)
        {
            return View("_PopUpModal", model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            NotificationsActionModel model = new NotificationsActionModel();

            if (ID.HasValue) //we are trying to edit a record
            {
                var notifications = notificationsServices.GetNotificationsByID(ID.Value);

                model.ID = notifications.ID;

                model.Name = notifications.Name;
                model.Content = notifications.Content;
                model.Date = notifications.Date;
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(NotificationsActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0) //we are trying to edit a record
            {
                var notifications = notificationsServices.GetNotificationsByID(model.ID);

                notifications.Name = model.Name;
                notifications.Content = model.Content;
                notifications.Date = model.Date;

                result = notificationsServices.UpdateNotifications(notifications);

            }
            else //we are trying to create a record
            {
                Notifications notifications = new Notifications();

                notifications.Name = model.Name;
                notifications.Content = model.Content;
                notifications.Date = model.Date;

                result = notificationsServices.SaveNotifications(notifications);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Notifications." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            NotificationsActionModel model = new NotificationsActionModel();

            var notifications = notificationsServices.GetNotificationsByID(ID);

            model.ID = notifications.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(NotificationsActionModel model)
        {
            JsonResult json = new JsonResult();
            
            var result = false;

            var notifications = notificationsServices.GetNotificationsByID(model.ID);

            result = notificationsServices.DeleteNotifications(notifications);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Notifications." };
            }

            return json;
        }

    }
}