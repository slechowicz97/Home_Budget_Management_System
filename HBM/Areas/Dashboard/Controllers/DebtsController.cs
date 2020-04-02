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

    public class DebtsController : Controller
    {
        KindsOfDebtsServices kindsOfDebtsServices = new KindsOfDebtsServices();
        DebtsServices debtsServices = new DebtsServices(); //package
        KindOfDebtsActionModel model1 = new KindOfDebtsActionModel();
        // GET: Dashboard/Debts
        public ActionResult Index(string searchTerm, int? kindsOfDebtsID, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            DebtsListingModel model = new DebtsListingModel();

            model.SearchTerm = searchTerm;
            model.KindOfDebtsID = kindsOfDebtsID;

            model.KindOfDebts = kindsOfDebtsServices.GetAllKindsOfDebts();

            model.Debts = debtsServices.SearchDebts(searchTerm, kindsOfDebtsID, page.Value, recordSize);
            var totalRecords = debtsServices.SearchDebtsCount(searchTerm, kindsOfDebtsID);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            DebtsActionModel model = new DebtsActionModel();

            if (ID.HasValue) //we are trying to edit a record
            {
                var debts = debtsServices.GetDebtsByID(ID.Value);

                model.ID = debts.ID;
                model.KindOfDebtsID = debts.KindOfDebtsID;
                model.Name = debts.Name;
                model.DebtPerson = debts.DebtPerson;
                model.Executor = debts.Executor;
                model.Price = debts.Price;
                model.Date = debts.Date;
            }
      
            model.KindOfDebts = kindsOfDebtsServices.GetAllKindsOfDebts();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(DebtsActionModel model)
        {
            JsonResult json = new JsonResult();


            var result = false;

            if (model.ID > 0) //we are trying to edit a record
            {
                
                var debts = debtsServices.GetDebtsByID(model.ID);

                debts.KindOfDebtsID = (int)model.KindOfDebtsID;
                debts.Name = model.Name;

                debts.DebtPerson = model.DebtPerson;
                debts.Executor = model.Executor;
                debts.Price = model.Price;
                debts.Date = model.Date;

                result = debtsServices.UpdateDebts(debts);
            }
            else //we are trying to create a record
            {
                Debts debts = new Debts();

                debts.KindOfDebtsID = (int)model.KindOfDebtsID;
                debts.Name = model.Name;

                debts.DebtPerson = model.DebtPerson;
                debts.Executor = model.Executor;
                debts.Price = model.Price;
                debts.Date = model.Date;

                result = debtsServices.SaveDebts(debts);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Debt." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            DebtsActionModel model = new DebtsActionModel();

            var debts = debtsServices.GetDebtsByID(ID);

            model.ID = debts.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(DebtsActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var debts = debtsServices.GetDebtsByID(model.ID);

            result = debtsServices.DeleteDebts(debts);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Debts." };
            }

            return json;
        }
    }
}