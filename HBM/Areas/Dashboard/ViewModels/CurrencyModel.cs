using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class CurrencyListingModel
    {
        public IEnumerable<Currency> Currencies { get; set; }
    }

    public class CurrencyActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}