using HBM.Data;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
    public class KindsOfDebtsServices
    {
        HBMContext context = new HBMContext();
        public IEnumerable<KindOfDebts> GetAllKindsOfDebts()
        {
            return context.KindOfDebts.ToList();
        }
        public KindOfDebts GetKindsOfDebtsByID(int ID)
        {
            return context.KindOfDebts.Find(ID);
        }

        public bool SaveKindsOfDebts(KindOfDebts kindsOfDebts)
        {
            context.KindOfDebts.Add(kindsOfDebts);

            return context.SaveChanges() > 0;
        }

        public bool UpdateKindsOfDebts(KindOfDebts kindsOfDebts)
        {
            context.Entry(kindsOfDebts).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteKindsOfDebts(KindOfDebts kindsOfDebts)
        {
            context.Entry(kindsOfDebts).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }

    }
}
