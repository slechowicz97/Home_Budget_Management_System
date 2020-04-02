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

    public class BudgetController : Controller
    {
        KBudgetServices kBudgetServices = new KBudgetServices();
        BudgetServices budgetServices = new BudgetServices(); //package
        // GET: Dashboard/Debts
        public ActionResult Index(string searchTerm, int? kBudgetID, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            BudgetListingModel model = new BudgetListingModel();

            model.SearchTerm = searchTerm;
            model.KBudgetID = kBudgetID;

            model.KBudgets = kBudgetServices.GetAllKBudgets();

            model.Budgets = budgetServices.SearchBudgets(searchTerm, kBudgetID, page.Value, recordSize);
            var totalRecords = budgetServices.SearchBudgetsCount(searchTerm, kBudgetID);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            BudgetActionModel model = new BudgetActionModel();

            if (ID.HasValue) //we are trying to edit a record
            {
                var budgets = budgetServices.GetBudgetsByID(ID.Value);

                model.ID = budgets.ID;
                model.KBudgetID = budgets.KBudgetID;
                model.Name = budgets.Name;
            }

            model.KBudgets = kBudgetServices.GetAllKBudgets();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(BudgetActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0) //we are trying to edit a record
            {
                var budgets = budgetServices.GetBudgetsByID(model.ID);

                budgets.KBudgetID = model.KBudgetID;
                budgets.ID = model.ID;
             
                budgets.Name = model.Name;

                result = budgetServices.UpdateBudgets(budgets);
            }
            else //we are trying to create a record
            {
                Budget budgets = new Budget();

                budgets.KBudgetID = model.KBudgetID;
                budgets.ID = model.ID;

                budgets.Name = model.Name;

                result = budgetServices.SaveBudgets(budgets);
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
            BudgetActionModel model = new BudgetActionModel();

            var budgets = budgetServices.GetBudgetsByID(ID);

            model.ID = budgets.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(BudgetActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var budgets = budgetServices.GetBudgetsByID(model.ID);

            result = budgetServices.DeleteBudgets(budgets);

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