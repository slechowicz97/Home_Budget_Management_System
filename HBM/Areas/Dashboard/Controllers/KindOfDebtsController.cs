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
    public class KindOfDebtsController : Controller
    {

        KindsOfDebtsServices kindOfDebtsServices = new KindsOfDebtsServices();
        // GET: Dashboard/KindOfExpense
        public ActionResult Index()
        {
            KindOfDebtsListingModel model = new KindOfDebtsListingModel();

            model.KindOfDebts = kindOfDebtsServices.GetAllKindsOfDebts();

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            KindOfDebtsActionModel model = new KindOfDebtsActionModel();

            if (ID.HasValue) // we are trying to edit a record
            {
                var kindOfDebts = kindOfDebtsServices.GetKindsOfDebtsByID(ID.Value);

                model.ID = kindOfDebts.ID;
                model.Name = kindOfDebts.Name;
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(KindOfDebtsActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;
            

            if (model.ID > 0) // we are trying to edit a record
            {
                var kindOfDebts = kindOfDebtsServices.GetKindsOfDebtsByID(model.ID);

                kindOfDebts.Name = model.Name;

                result = kindOfDebtsServices.UpdateKindsOfDebts(kindOfDebts);
            }
            else  // we are trying to create a record
            {

                KindOfDebts kindOfDebts = new KindOfDebts();

                kindOfDebts.Name = model.Name;

                result = kindOfDebtsServices.SaveKindsOfDebts(kindOfDebts);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on KindOfDebts" };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            KindOfDebtsActionModel model = new KindOfDebtsActionModel();

            var kindOfDebts = kindOfDebtsServices.GetKindsOfDebtsByID(ID);

            model.ID = kindOfDebts.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(KindOfDebtsActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;

            var kindOfDebts = kindOfDebtsServices.GetKindsOfDebtsByID(model.ID);

            result = kindOfDebtsServices.DeleteKindsOfDebts(kindOfDebts);

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