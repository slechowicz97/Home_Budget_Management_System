using HBM.Areas.Dashboard.ViewModels;
using HBM.Entities;
using HBM.Services;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBM.Areas.Dashboard.Controllers
{

    public class DebtCardsController : Controller
    {
        DebtCardServices debtCardServices = new DebtCardServices();
        TransactionsServices transactionsServices = new TransactionsServices();
        // GET: Dashboard/DebtCards 
            public ActionResult Index(string searchTerm, int? page)
            {
                int recordSize = 5;
                page = page ?? 1;

            DebtCardsListingModel model = new DebtCardsListingModel();

                model.SearchTerm = searchTerm;

                model.DebtCards = debtCardServices.SearchDebtCards(searchTerm, page.Value, recordSize);
                model.Transactions = transactionsServices.SearchTransactions(searchTerm, null, page.Value, "", recordSize, null);

                var totalRecords = debtCardServices.SearchDebtCardsCount(searchTerm);

                model.Pager = new Pager(totalRecords, page, recordSize);

                return View(model);
            }

       [HttpGet]
        public ActionResult Action(int? ID)
        {
            DebtCardsActionModel model = new DebtCardsActionModel();

            if (ID.HasValue) //we are trying to edit a record
            {
                var debtCards = debtCardServices.GetDebtCardsByID(ID.Value);

                model.ID = debtCards.ID;

                model.Name = debtCards.Name;
                model.Limit = debtCards.Limit;
                model.PlannedTimeRefund = debtCards.PlannedTimeRefund;
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(DebtCardsActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0) //we are trying to edit a record
            {
                var debtCards = debtCardServices.GetDebtCardsByID(model.ID);

                debtCards.Name = model.Name;
                debtCards.Limit = model.Limit;
                debtCards.PlannedTimeRefund = model.PlannedTimeRefund;

                result = debtCardServices.UpdateDebtCards(debtCards);

                ScheduledTransaction scheduledTransaction = new ScheduledTransaction();
                ScheduledTransactionsServices scheduledTransactionsServices = new ScheduledTransactionsServices();
                BalanceListingModel balanceModel = new BalanceListingModel();
                BalanceController balanceController = new BalanceController();
     
                balanceModel = balanceController.GetBalances();

                DebtCardsListingModel debtsCardModel = new DebtCardsListingModel();
                debtsCardModel.DebtCards = debtCardServices.GetAllDebtCards();
                debtsCardModel.Transactions = transactionsServices.SearchTransactions("", null, 0, "", 0, null);

                foreach (var debts in debtsCardModel.DebtCards)
                {
                    float remaning = debts.Limit;
                    foreach (var transactions in debtsCardModel.Transactions)
                    {
                        if (transactions.BudgetsName == debts.Name)
                        {
                            remaning = remaning - transactions.Price;
                            debts.Price = remaning;
                        }
                    }      
                        scheduledTransaction.Price = debts.Price;
                }

                scheduledTransaction.TransactionName = "Spłata karty";
                scheduledTransaction.Recipient = "Bank";
                scheduledTransaction.Data = model.PlannedTimeRefund;

                scheduledTransactionsServices.UpdateScheduledTransaction(scheduledTransaction);
            }
            else //we are trying to create a record
            {
                DebtCard debtCards = new DebtCard();

                debtCards.Name = model.Name;
                debtCards.Limit = model.Limit;
                debtCards.PlannedTimeRefund = model.PlannedTimeRefund;

                result = debtCardServices.SaveDebtCards(debtCards);

                //Create Scheduled Transactions
                ScheduledTransaction scheduledTransaction = new ScheduledTransaction();
                ScheduledTransactionsServices scheduledTransactionsServices = new ScheduledTransactionsServices();
                BalanceListingModel balanceModel = new BalanceListingModel();
                BalanceController balanceController = new BalanceController();
        
                balanceModel = balanceController.GetBalances();
                DebtCardsListingModel debtsCardModel = new DebtCardsListingModel();
                debtsCardModel.DebtCards = debtCardServices.GetAllDebtCards();
                debtsCardModel.Transactions = transactionsServices.SearchTransactions("", null, 0, "", 0, null);
                foreach (var debts in debtsCardModel.DebtCards)
                {
                    float remaning = debts.Limit;
                    foreach (var transactions in debtsCardModel.Transactions)
                    {
                        if (transactions.BudgetsName == debts.Name)
                        {
                            remaning = remaning - transactions.Price;
                            debts.Price = remaning;
                        }
                    }
                    if (debts.Price == 0)
                    {
                        scheduledTransaction.Price = debts.Price;
                    }
                    else
                    {
                        scheduledTransaction.Price = debts.Price;
                    }
                }

                scheduledTransaction.TransactionName = "Spłata karty";
                scheduledTransaction.Recipient = "Bank";
                scheduledTransaction.Data = model.PlannedTimeRefund;

                scheduledTransactionsServices.SaveScheduledTransaction(scheduledTransaction);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on DebtCard." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            DebtCardsActionModel model = new DebtCardsActionModel();

            var debtCards = debtCardServices.GetDebtCardsByID(ID);

            model.ID = debtCards.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(DebtCardsActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var debtCards = debtCardServices.GetDebtCardsByID(model.ID);

            result = debtCardServices.DeleteDebtCards(debtCards);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Debts." };
            }

            return json;
        }

    }
}