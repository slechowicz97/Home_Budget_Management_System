using HBM.Data;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
    public class BudgetServices
    {
        HBMContext context = new HBMContext();
        public IEnumerable<Budget> GetAllBudgets()
            {
                return context.Budgets.ToList();
            }

            public IEnumerable<Budget> GetAllBudgetsByKind(int kBudgetID)
            {
                return context.Budgets.Where(x => x.KBudgetID == kBudgetID).ToList();
            }

            public IEnumerable<Budget> SearchBudgets(string searchTerm, int? kBudgetID, int page, int recordSize)
            {
                var budgets = context.Budgets.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                budgets = budgets.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
                }
                if (kBudgetID.HasValue && kBudgetID.Value > 0)
                {
                budgets = budgets.Where(a => a.KBudgetID == kBudgetID.Value);
                }

                var skip = (page - 1) * recordSize;

                return budgets.OrderBy(x => x.KBudgetID).Skip(skip).Take(recordSize).ToList();
            }

            public int SearchBudgetsCount(string searchTerm, int? kBudgetID)
            {
                var budgets = context.Budgets.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                budgets = budgets.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
                }
                if (kBudgetID.HasValue && kBudgetID.Value > 0)
                {
                budgets = budgets.Where(a => a.KBudgetID == kBudgetID.Value);
                }

                return budgets.Count();
            }

            public Budget GetBudgetsByID(int ID)
            {
                return context.Budgets.Find(ID);
            }


            public bool SaveBudgets(Budget budgets)
            {
                context.Budgets.Add(budgets);

                return context.SaveChanges() > 0;
            }

            public bool UpdateBudgets(Budget budgets)
            {
                context.Entry(budgets).State = System.Data.Entity.EntityState.Modified;

                var exitingBudgets = context.Budgets.Find(budgets.ID);

                context.Entry(exitingBudgets).CurrentValues.SetValues(budgets);

                return context.SaveChanges() > 0;
            }

            public bool DeleteBudgets(Budget budgets)
            {    
                context.Entry(budgets).State = System.Data.Entity.EntityState.Deleted;

                return context.SaveChanges() > 0;
            }
        
    }
}