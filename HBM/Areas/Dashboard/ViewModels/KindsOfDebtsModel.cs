    using HBM.Entities;
    using HBM.ViewModels;
    using System.Collections.Generic;

    namespace HBM.Areas.Dashboard.ViewModels
    {
        public class KindOfDebtsListingModel
        {
            public IEnumerable<KindOfDebts> KindOfDebts { get; set; }
        }

        public class KindOfDebtsActionModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
    }