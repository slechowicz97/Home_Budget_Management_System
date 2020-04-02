using HBM.Entities;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class ImportantShoppingListingModel
    {
        public IEnumerable<ImportantShopping> ImportantShoppings { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }

    public class ImportantShoppingActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Place { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateStop { get; set; }

        public string PictureIDs { get; set; }
        public virtual List<ShoppingPicture> ShoppingPictures { get; set; }
    }
}