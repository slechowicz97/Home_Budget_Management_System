using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class IndexViewModel
    {
       public DebtsListingModel DebtsListingModel;
       public ScheduledTransactionsListingModel ScheduledTransactionsListingModel;
       public SavingsListingModel SavingsListingModel;
       public BalanceListingModel BalanceListingModel;
    }
}