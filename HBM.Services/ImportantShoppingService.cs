using HBM.Data;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
    public class ImportantShoppingService
    {
        HBMContext context = new HBMContext();
        public IEnumerable<ImportantShopping> GetAllImportantShoppings()
        {
            return context.ImportantShoppings.ToList();
        }

        public IEnumerable<ImportantShopping> GetAllImportantShoppingsByKind(int kindsOfDebtsID)
        {
            return context.ImportantShoppings.ToList();
        }

        public IEnumerable<ImportantShopping> SearchImportantShoppings(string searchTerm, int page, int recordSize)
        {
            var importantShopping = context.ImportantShoppings.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                importantShopping = importantShopping.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower())); //or DateStart
            }

            var skip = (page - 1) * recordSize;

            return importantShopping.OrderBy(x => x.DateStart).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchImportantShoppingsCount(string searchTerm)
        {
            var importantShopping = context.ImportantShoppings.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                importantShopping = importantShopping.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return importantShopping.Count();
        }

        public ImportantShopping GetImportantShoppingsByID(int ID)
        {

            return context.ImportantShoppings.Find(ID);
        }


        public bool SaveImportantShoppings(ImportantShopping importantShopping)
        {
            context.ImportantShoppings.Add(importantShopping);

            return context.SaveChanges() > 0;
        }

        public bool UpdateImportantShoppings(ImportantShopping importantShopping)
        {
            context.Entry(importantShopping).State = System.Data.Entity.EntityState.Modified;

            var exitingAccomodationPackage = context.ImportantShoppings.Find(importantShopping.ID);

            context.Entry(exitingAccomodationPackage).CurrentValues.SetValues(importantShopping);

            context.ShoppingPictures.AddRange(importantShopping.ShoppingPictures);

            return context.SaveChanges() > 0;
        }

        public bool DeleteImportantShoppings(ImportantShopping importantShopping)
        {
            context.Entry(importantShopping).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }

        public List<ShoppingPicture> GetPicturesByImportantShoppingID(int importantShoppingID)
        {
            return context.ImportantShoppings.Find(importantShoppingID).ShoppingPictures.ToList();
        }
    }
}

