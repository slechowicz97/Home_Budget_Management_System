using HBM.Data;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
   public class DebtsServices
    {
        HBMContext context = new HBMContext();
        public IEnumerable<Debts> GetAllDebts()
        {
            return context.Debts.ToList();
        }

        public IEnumerable<Debts> GetAllDebtsByKind(int kindsOfDebtsID)
        {
            return context.Debts.Where(x => x.KindOfDebtsID == kindsOfDebtsID).ToList();
        }

        public IEnumerable<Debts> SearchDebts(string searchTerm, int? kindsOfDebtsID, int page, int recordSize)
        {
            var debts = context.Debts.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                debts = debts.Where(a => a.Executor.ToLower().Contains(searchTerm.ToLower()));
            }
            if (kindsOfDebtsID.HasValue && kindsOfDebtsID.Value > 0)
            {
                debts = debts.Where(a => a.KindOfDebtsID == kindsOfDebtsID.Value);
            }

            var skip = (page - 1) * recordSize;

            return debts.OrderBy(x => x.Date).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchDebtsCount(string searchTerm, int? kindsOfDebtsID)
        {
            var debts = context.Debts.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                debts = debts.Where(a => a.Executor.ToLower().Contains(searchTerm.ToLower()));
            }
            if (kindsOfDebtsID.HasValue && kindsOfDebtsID.Value > 0)
            {
                debts = debts.Where(a => a.KindOfDebtsID == kindsOfDebtsID.Value);
            }

            return debts.Count();
        }

        public Debts GetDebtsByID(int ID)
        {
            return context.Debts.Find(ID);
        }


        public bool SaveDebts(Debts debts)
        {
            context.Debts.Add(debts);

            return context.SaveChanges() > 0;
        }

        public bool UpdateDebts(Debts debts)
        {     
            context.Entry(debts).State = System.Data.Entity.EntityState.Modified;

            var exitingAccomodationPackage = context.Debts.Find(debts.ID);
            
            context.Entry(exitingAccomodationPackage).CurrentValues.SetValues(debts);

            return context.SaveChanges() > 0;
        }

        public bool DeleteDebts(Debts debts)
        {
            context.Entry(debts).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
