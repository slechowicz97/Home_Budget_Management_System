using HBM.Areas.Dashboard.ViewModels;
using HBM.Entities;
using HBM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBM.Areas.Dashboard.Controllers
{
    public class KBudgetController : Controller
    {
        KBudgetServices kbudgetServices = new KBudgetServices();
        // GET: Dashboard/KindOfExpense
        public ActionResult Index()
        {
            KBudgetListingModel model = new KBudgetListingModel();

            model.KBudgets = kbudgetServices.GetAllKBudgets();

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            KBudgetActionModel model = new KBudgetActionModel();

            if (ID.HasValue) // we are trying to edit a record
            {
                var kBudgets = kbudgetServices.GetKBudgetsByID(ID.Value);

                model.ID = kBudgets.ID;
                model.Name = kBudgets.Name;
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(KBudgetActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;

            if (model.ID > 0) // we are trying to edit a record
            {
                var kBudgets = kbudgetServices.GetKBudgetsByID(model.ID);

                kBudgets.Name = model.Name;

                result = kbudgetServices.UpdateKBudgets(kBudgets);
            }
            else  // we are trying to create a record
            {

                KBudget kBudgets = new KBudget();

                kBudgets.Name = model.Name;

                result = kbudgetServices.SaveKBudgets(kBudgets);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on KBudgets" };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            KBudgetActionModel model = new KBudgetActionModel();

            var kBudgets = kbudgetServices.GetKBudgetsByID(ID);

            model.ID = kBudgets.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(KBudgetActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;

            var kBudgets = kbudgetServices.GetKBudgetsByID(model.ID);

            result = kbudgetServices.DeleteKBudgets(kBudgets);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Kinds" };
            }

            return json;
        }
    }
}