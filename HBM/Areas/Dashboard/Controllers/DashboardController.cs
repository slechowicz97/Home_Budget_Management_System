using HBM.Areas.Dashboard.ViewModels;
using HBM.Services;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBM.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        
        ScheduledTransactionsServices scheduledTransactionsServices = new ScheduledTransactionsServices();
        KindsOfDebtsServices kindsOfDebtsServices = new KindsOfDebtsServices();
        DebtsServices debtsServices = new DebtsServices();
        SavingsServices savingsServices = new SavingsServices();
        BalanceController balanceServices = new BalanceController();
        SavingsController savingsController = new SavingsController();
        NotificationsServices notificationsServices = new NotificationsServices();
        // GET: Dashboard/Dashboard
        public ActionResult Index(string searchTerm, int? kindsOfDebtsID, int? page)
        {       
            ScheduledTransactionsListingModel model = new ScheduledTransactionsListingModel();

            model.SearchTerm = "";
            model.ScheduledTransactions = scheduledTransactionsServices.SearchScheduledTransactions("");

            int recordSize = 5;
            page = page ?? 1;

            DebtsListingModel modelDebts = new DebtsListingModel();

            modelDebts.SearchTerm = searchTerm;
            modelDebts.KindOfDebtsID = kindsOfDebtsID;

            modelDebts.KindOfDebts = kindsOfDebtsServices.GetAllKindsOfDebts();

            modelDebts.Debts = debtsServices.SearchDebts(searchTerm, kindsOfDebtsID, page.Value, recordSize);
            var totalRecords = debtsServices.SearchDebtsCount(searchTerm, kindsOfDebtsID);

            modelDebts.Pager = new Pager(totalRecords, page, recordSize);

            SavingsListingModel savingModel = new SavingsListingModel();
            
            savingModel.SearchTerm = "";
            savingModel.Savings = savingsServices.SearchSavings("", page.Value, recordSize);
            savingModel.AllSavings = savingsController.GetAllOfSavingsMoney();

            IndexViewModel indexViewModel = new IndexViewModel();
     
            indexViewModel.DebtsListingModel = modelDebts;
            indexViewModel.ScheduledTransactionsListingModel = model;
            indexViewModel.SavingsListingModel = savingModel;
            indexViewModel.BalanceListingModel = balanceServices.GetBalances();
            return View(indexViewModel);
        }
    }
}