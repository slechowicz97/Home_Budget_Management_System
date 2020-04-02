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
    public class SavingsController : Controller
    {
        // GET: Dashboard/Savings
        SavingsServices savingsServices = new SavingsServices();
        CurrencyServices currencyServices = new CurrencyServices();
        // GET: Dashboard/ScheduledTransactions
        public ActionResult Index(string searchTerm, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            SavingsListingModel model = new SavingsListingModel();

            model.SearchTerm = searchTerm;

            model.Savings = savingsServices.SearchSavings(searchTerm, page.Value, recordSize);
            model.AllSavings = GetAllOfSavingsMoney();
            model.Currencies = currencyServices.GetAllCurrencies();
            var totalRecords = savingsServices.SearchSavingsCount(searchTerm);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            SavingsActionModel model = new SavingsActionModel();
            model.Currencies = currencyServices.GetAllCurrencies();

            if (ID.HasValue) // we are trying to edit a record
            {
                var savings = savingsServices.GetSavingsByID(ID.Value);

                model.ID = savings.ID;

                model.Type = savings.Type;
                model.Name = savings.Name;
                model.Price = savings.Price;
                model.DateEnd = savings.DateEnd;
                model.Currency = savings.Currency;
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(SavingsActionModel model)
        {
            model.Currencies = currencyServices.GetAllCurrencies();
            JsonResult json = new JsonResult();
            var result = false;

            if (model.ID > 0) // we are trying to edit a record
            {
                var savings = savingsServices.GetSavingsByID(model.ID);

                savings.Type = model.Type;
                savings.Name = model.Name;
                savings.Price = model.Price;
                savings.DateEnd = model.DateEnd;
                savings.Currency = model.Currency;

                result = savingsServices.UpdateSavings(savings);
            }
            else  // we are trying to create a record
            {

                Savings savings = new Savings();

                savings.Type = model.Type;
                savings.Name = model.Name;
                savings.Price = model.Price;
                savings.DateEnd = model.DateEnd;
                savings.Currency = model.Currency;

                result = savingsServices.SaveSavings(savings);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on savings" };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            SavingsActionModel model = new SavingsActionModel();

            var savings = savingsServices.GetSavingsByID(ID);

            model.ID = savings.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(SavingsActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;

            var savings = savingsServices.GetSavingsByID(model.ID);

            result = savingsServices.DeleteSavings(savings);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on savings" };
            }

            return json;
        }

       public float GetAllOfSavingsMoney()
       {
            SavingsListingModel model = new SavingsListingModel();
            float allMoney = 0; 
            model.Savings = savingsServices.GetAllSavings();
            foreach (var savings in model.Savings)
            {
                allMoney += savings.Price;
            }
            return allMoney;
        }
    }
}