using HBM.Areas.Dashboard.ViewModels;
using HBM.Entities;
using HBM.Services;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBM.Areas.Dashboard.Controllers
{
    
    public class ImportantShoppingController : Controller
    {
        ImportantShoppingService importantShoppingService = new ImportantShoppingService();
        DashboardService dashboardService = new DashboardService();
        // GET: Dashboard/ImportantShopping

        public ActionResult Index(string searchTerm, int? kindsOfDebtsID, int? page)
            {
                int recordSize = 5;
                page = page ?? 1;

            ImportantShoppingListingModel model = new ImportantShoppingListingModel();

                model.SearchTerm = searchTerm;

                model.ImportantShoppings = importantShoppingService.SearchImportantShoppings(searchTerm, page.Value, recordSize);
                var totalRecords = importantShoppingService.SearchImportantShoppingsCount(searchTerm);

                model.Pager = new Pager(totalRecords, page, recordSize);

                return View(model);
            }

            [HttpGet]
            public ActionResult Action(int? ID)
            {
            ImportantShoppingActionModel model = new ImportantShoppingActionModel();

                if (ID.HasValue) //we are trying to edit a record
                {
                    var importantShopping = importantShoppingService.GetImportantShoppingsByID(ID.Value);

                    model.ID = importantShopping.ID;
             
                    model.Name = importantShopping.Name;
                    model.Place = importantShopping.Place;
                    model.Price = importantShopping.Price;
                    model.DateStart = importantShopping.DateStop;

                model.ShoppingPictures = importantShoppingService.GetPicturesByImportantShoppingID(importantShopping.ID);

            }

                return PartialView("_Action", model);
            }

            [HttpPost]
            public JsonResult Action(ImportantShoppingActionModel model)
            {
                JsonResult json = new JsonResult();

                var result = false;

            //model.PictureIDs = "90,67,23" = ["90", "67", "23"] = {90, 67, 23}
            List<int> pictureIDs = !string.IsNullOrEmpty(model.PictureIDs) ? model.PictureIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();
            var pictures = dashboardService.GetPicturesByIDs(pictureIDs);

            if (model.ID > 0) //we are trying to edit a record
                {
                    var importantShopping = importantShoppingService.GetImportantShoppingsByID(model.ID);

                importantShopping.Name = model.Name;

                importantShopping.Price = model.Price;
                importantShopping.Place = model.Place;
                importantShopping.DateStart = model.DateStart;
                importantShopping.DateStop = model.DateStop;

                importantShopping.ShoppingPictures.Clear();
                importantShopping.ShoppingPictures.AddRange(pictures.Select(x => new ShoppingPicture() { ImportantShoppingID = importantShopping.ID, PictureID = x.ID }));


                result = importantShoppingService.UpdateImportantShoppings(importantShopping);
                }
                else //we are trying to create a record
                {
                ImportantShopping importantShopping = new ImportantShopping();

                importantShopping.Name = model.Name;

                importantShopping.Price = model.Price;
                importantShopping.Place = model.Place;
                importantShopping.DateStart = model.DateStart;
                importantShopping.DateStop = model.DateStop;

                result = importantShoppingService.SaveImportantShoppings(importantShopping);
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
            ImportantShoppingActionModel model = new ImportantShoppingActionModel();

                var importantShopping = importantShoppingService.GetImportantShoppingsByID(ID);

                model.ID = importantShopping.ID;

                return PartialView("_Delete", model);
            }

            [HttpPost]
            public JsonResult Delete(ImportantShoppingActionModel model)
            {
                JsonResult json = new JsonResult();

                var result = false;

                var importantShopping = importantShoppingService.GetImportantShoppingsByID(model.ID);

                result = importantShoppingService.DeleteImportantShoppings(importantShopping);

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

            [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();

            var dashboardService = new DashboardService();
            var picsList = new List<Picture>();

            var files = Request.Files;

            for (int i = 0; i < files.Count; i++)
            {
                var picture = files[i];

                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                var filePath = Server.MapPath("~/images/site/") + fileName;

                picture.SaveAs(filePath);

                var dbPicture = new Picture();
                dbPicture.URL = fileName;

                if (dashboardService.SavePicture(dbPicture))
                {
                    picsList.Add(dbPicture);
                }
            }

            result.Data = picsList;

            return result;
        }
  
        
    }
}