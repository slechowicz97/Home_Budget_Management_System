using HBM.Data;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
    public class CurrencyServices
    {
        HBMContext context = new HBMContext();
          
            public IEnumerable<Currency> GetAllCurrencies()
            {
                return context.Currencies.ToList();
            }
            public Currency GetCurrenciesByID(int ID)
            {
                return context.Currencies.Find(ID);
            }

            public bool SaveCurrencies(Currency currency)
            {
                context.Currencies.Add(currency);

                return context.SaveChanges() > 0;
            }

            public bool UpdateCurrencies(Currency currency)
            {
                context.Entry(currency).State = System.Data.Entity.EntityState.Modified;

                return context.SaveChanges() > 0;
            }

            public bool DeleteCurrencies(Currency currency)
            {
                context.Entry(currency).State = System.Data.Entity.EntityState.Deleted;

                return context.SaveChanges() > 0;
            }

        }
    }