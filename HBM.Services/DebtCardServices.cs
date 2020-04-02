using HBM.Data;
using HBM.Data.Migrations;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
    public class DebtCardServices
    {
        HBMContext context = new HBMContext();
        public IEnumerable<DebtCard> GetAllDebtCards()
        {
            return context.DebtCards.ToList();
        }


        public IEnumerable<DebtCard> SearchDebtCards(string searchTerm, int page, int recordSize)
        {
            var debtCards = context.DebtCards.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                debtCards = debtCards.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
      
            var skip = (page - 1) * recordSize;

            return debtCards.OrderBy(x => x.Name).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchDebtCardsCount(string searchTerm)
        {
            var debtCards = context.DebtCards.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                debtCards = debtCards.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
       

            return debtCards.Count();
        }

        public DebtCard GetDebtCardsByID(int ID)
        {
            return context.DebtCards.Find(ID);
        }


        public bool SaveDebtCards(DebtCard debtCards)
        {
            context.DebtCards.Add(debtCards);

            return context.SaveChanges() > 0;
        }

        public bool UpdateDebtCards(DebtCard debtCards)
        {
            context.Entry(debtCards).State = System.Data.Entity.EntityState.Modified;

            var exitingAccomodationPackage = context.DebtCards.Find(debtCards.ID);

            context.Entry(exitingAccomodationPackage).CurrentValues.SetValues(debtCards);

            return context.SaveChanges() > 0;
        }

        public bool DeleteDebtCards(DebtCard debtCards)
        {
            context.Entry(debtCards).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
