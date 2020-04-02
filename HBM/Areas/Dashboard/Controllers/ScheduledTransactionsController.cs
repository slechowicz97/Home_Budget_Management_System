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
    public class ScheduledTransactionsController : Controller
    {

        ScheduledTransactionsServices scheduledTransactionsServices = new ScheduledTransactionsServices();
        // GET: Dashboard/ScheduledTransactions
        public ActionResult Index(string searchTerm)
        {
            ViewBag.Message = "Widok strony głównej zaplanowanych transakcji";
            ScheduledTransactionsListingModel model = new ScheduledTransactionsListingModel();

            model.SearchTerm = searchTerm;
            model.ScheduledTransactions = scheduledTransactionsServices.SearchScheduledTransactions(searchTerm);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            ViewBag.Message = "Widok strony dodawania zaplanowanych transakcji";
            ScheduledTransactionsActionModel model = new ScheduledTransactionsActionModel();

            if (ID.HasValue) // we are trying to edit a record
            {
                var scheduledTransaction = scheduledTransactionsServices.GetScheduledTransactionsByID(ID.Value);

                model.ID = scheduledTransaction.ID;
                model.TransactionName = scheduledTransaction.TransactionName;
                model.Recipient = scheduledTransaction.Recipient;
                model.Price = scheduledTransaction.Price;
                model.Data = scheduledTransaction.Data;
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(ScheduledTransactionsActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;

            if (model.ID > 0) // we are trying to edit a record
            {
                var scheduledTransaction = scheduledTransactionsServices.GetScheduledTransactionsByID(model.ID);

                scheduledTransaction.TransactionName = model.TransactionName;
                scheduledTransaction.Recipient = model.Recipient;
                scheduledTransaction.Price = model.Price;
                scheduledTransaction.Data = model.Data;

                result = scheduledTransactionsServices.UpdateScheduledTransaction(scheduledTransaction);
            }
            else  // we are trying to create a record
            {

                ScheduledTransaction scheduledTransaction = new ScheduledTransaction();

                scheduledTransaction.TransactionName = model.TransactionName;
                scheduledTransaction.Recipient = model.Recipient;
                scheduledTransaction.Price = model.Price;
                scheduledTransaction.Data = model.Data;

                result = scheduledTransactionsServices.SaveScheduledTransaction(scheduledTransaction);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Scheduled Transaction" };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            ViewBag.Message = "Widok strony usuwania zaplanowanych transakcji";
            ScheduledTransactionsActionModel model = new ScheduledTransactionsActionModel();

            var scheduledTransaction = scheduledTransactionsServices.GetScheduledTransactionsByID(ID);

            model.ID = scheduledTransaction.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(ScheduledTransactionsActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;

            var scheduledTransaction = scheduledTransactionsServices.GetScheduledTransactionsByID(model.ID);

            result = scheduledTransactionsServices.DeleteScheduledTransaction(scheduledTransaction);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Scheduled Transaction" };
            }

            return json;
        }
    }
}