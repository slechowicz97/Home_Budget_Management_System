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
    public class CurrencyController : Controller
    {
        // GET: Dashboard/Currency
        CurrencyServices currencyServices = new CurrencyServices();
        public ActionResult Index()
        {
            CurrencyListingModel model = new CurrencyListingModel();

            model.Currencies = currencyServices.GetAllCurrencies();

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            CurrencyActionModel model = new CurrencyActionModel();

            if (ID.HasValue) // we are trying to edit a record
            {
                var kBudgets = currencyServices.GetCurrenciesByID(ID.Value);

                model.ID = kBudgets.ID;
                model.Name = kBudgets.Name;
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(CurrencyActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;

            if (model.ID > 0) // we are trying to edit a record
            {
                var kBudgets = currencyServices.GetCurrenciesByID(model.ID);

                kBudgets.Name = model.Name;

                result = currencyServices.UpdateCurrencies(kBudgets);
            }
            else  // we are trying to create a record
            {

                Currency kBudgets = new Currency();

                kBudgets.Name = model.Name;

                result = currencyServices.SaveCurrencies(kBudgets);
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
            CurrencyActionModel model = new CurrencyActionModel();

            var kBudgets = currencyServices.GetCurrenciesByID(ID);

            model.ID = kBudgets.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(CurrencyActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;

            var kBudgets = currencyServices.GetCurrenciesByID(model.ID);

            result = currencyServices.DeleteCurrencies(kBudgets);

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