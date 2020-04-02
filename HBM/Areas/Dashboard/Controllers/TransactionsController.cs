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

    public class TransactionsController : Controller
    {
        DebtCardServices debtCardServices = new DebtCardServices();
        BudgetServices budgetServices = new BudgetServices();
        TransactionsServices transactionsServices = new TransactionsServices(); //package
        // GET: Dashboard/Debts
        public ActionResult Index(string searchTerm, int? budgetsID, int? page, int? budgetTypeID, DateTime? searchTermStart = null, DateTime? searchTermEnd = null, string orderByPrice = "")
        {
            int recordSize = 5;
            page = page ?? 1;
            float sum = 0;

            TransactionListingModel model = new TransactionListingModel();

            model.SearchTerm = searchTerm;
            model.BudgetsID = budgetsID;

            model.SearchTermStart = searchTermStart;
            model.SearchTermEnd = searchTermEnd;

            model.Budgets = budgetServices.GetAllBudgets();
            model.DebtCards = debtCardServices.GetAllDebtCards();

            model.Transactions = transactionsServices.SearchTransactions(searchTerm, budgetsID, page.Value, orderByPrice, recordSize, budgetTypeID, searchTermStart, searchTermEnd);

            foreach (var tra in model.Transactions)
            {
                sum += tra.Price;
            }
            model.SumOfPrice = sum;
            var totalRecords = transactionsServices.SearchTransactionsCount(searchTerm, budgetsID, budgetTypeID, searchTermStart, searchTermEnd);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult ActionExpense(int? ID)
        {
            TransactionActionModel model = new TransactionActionModel();

            if (ID.HasValue) //we are trying to edit a record
            {
                var transaction = transactionsServices.GetTransactionsByID(ID.Value);

                model.ID = transaction.ID;
                model.BudgetsID = transaction.BudgetsID;
                model.BudgetsName = transaction.BudgetsName;
                model.TransactionName = transaction.TransactionName;
                model.Place = transaction.Place;
                model.Price = transaction.Price;
                model.Date = transaction.Date;
            }

            model.Budgets = budgetServices.GetAllBudgets();
            model.DebtCards = debtCardServices.GetAllDebtCards();

            return PartialView("_ActionExpense", model);
        }

        [HttpPost]
        public JsonResult ActionExpense(TransactionActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0) //we are trying to edit a record
            {
                var transaction = transactionsServices.GetTransactionsByID(model.ID);

                transaction.BudgetsID = (int)model.BudgetsID;
                transaction.BudgetsName = model.BudgetsName;
                transaction.TransactionName = model.TransactionName;
                transaction.Place = model.Place;
                transaction.Price = model.Price;
                transaction.Date = model.Date;

                result = transactionsServices.UpdateTransactions(transaction);

            }
            else //we are trying to create a record
            {
                Transactions transaction = new Transactions();

                transaction.BudgetsID = (int)model.BudgetsID;
                transaction.BudgetsName = model.BudgetsName;
                transaction.TransactionName = model.TransactionName;
                transaction.Place = model.Place;
                transaction.Price = model.Price;
                transaction.Date = model.Date;

                result = transactionsServices.SaveTransactions(transaction);
            }
            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Transactions." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult ActionIncome(int? ID)
        {
            TransactionActionModel model = new TransactionActionModel();

            if (ID.HasValue) //we are trying to edit a record
            {
                var transaction = transactionsServices.GetTransactionsByID(ID.Value);

                model.ID = transaction.ID;
                model.BudgetsID = transaction.BudgetsID;
                model.BudgetsName = transaction.BudgetsName;
                model.TransactionName = transaction.TransactionName;
                model.Place = transaction.Place;
                model.Price = transaction.Price;
                model.Date = transaction.Date;
            }

            model.Budgets = budgetServices.GetAllBudgets();
            model.DebtCards = debtCardServices.GetAllDebtCards();

            return PartialView("_ActionIncome", model);
        }

        [HttpPost]
        public JsonResult ActionIncome(TransactionActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0) //we are trying to edit a record
            {
                var transaction = transactionsServices.GetTransactionsByID(model.ID);

                transaction.BudgetsID = (int)model.BudgetsID;
                transaction.BudgetsName = model.BudgetsName;
                transaction.TransactionName = model.TransactionName;
                transaction.Place = model.Place;
                transaction.Price = model.Price;
                transaction.Date = model.Date;

                result = transactionsServices.UpdateTransactions(transaction);
            }
            else //we are trying to create a record
            {
                Transactions transaction = new Transactions();

                transaction.BudgetsID = (int)model.BudgetsID;
                transaction.BudgetsName = model.BudgetsName;
                transaction.TransactionName = model.TransactionName;
                transaction.Place = model.Place;
                transaction.Price = model.Price;
                transaction.Date = model.Date;

                result = transactionsServices.SaveTransactions(transaction);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Transactions." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            TransactionActionModel model = new TransactionActionModel();

            var transaction = transactionsServices.GetTransactionsByID(ID);

            model.ID = transaction.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(TransactionActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var transaction = transactionsServices.GetTransactionsByID(model.ID);

            result = transactionsServices.DeleteTransactions(transaction);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on  transaction." };
            }

            return json;
        }
    }
}