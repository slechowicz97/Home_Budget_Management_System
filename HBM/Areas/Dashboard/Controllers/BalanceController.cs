using HBM.Areas.Dashboard.ViewModels;
using HBM.Data;
using HBM.Entities;
using HBM.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HBM.Areas.Dashboard.Controllers
{
    public class BalanceController : Controller
    {
        BalanceServices balanceServices = new BalanceServices();
        // GET: Dashboard/Balance
        public ActionResult Index(DateTime? searchTermStart = null, DateTime? searchTermEnd = null)
        {
            BalanceListingModel model = new BalanceListingModel();
            model.SearchTermStart = searchTermStart;
            model.SearchTermEnd = searchTermEnd;

            if (searchTermStart != null && searchTermEnd != null)
            {
                model = GetBalancesByDate(searchTermStart, searchTermEnd);
            }
            else 
            {
                model = GetBalances();
            }

            return View(model);
        }

        public BalanceListingModel GetBalances()
        {
            BalanceListingModel model = new BalanceListingModel();
            DebtCardsListingModel debtsCardModel = new DebtCardsListingModel();
            DebtCardServices debtCardServices = new DebtCardServices();
            TransactionsServices transactionsServices = new TransactionsServices();
            float countRemaningLimit = 0;
            var context = new HBMContext();
            float incomes = 0, expenses = 0;
            int valueCompare = 0;

            debtsCardModel.DebtCards = debtCardServices.GetAllDebtCards();
            debtsCardModel.Transactions = transactionsServices.GetAllTransactions();

            DateTime now = DateTime.Today;
            DateTime searchTermStart = new DateTime(now.Year, now.Month, 1);
          
            foreach (var transactions in context.Transactions)
            {
                valueCompare = DateTime.Compare(transactions.Date, (DateTime)now);
                if (valueCompare <= 0)
                {
                    valueCompare = DateTime.Compare((DateTime)searchTermStart, transactions.Date);
                    {
                        if (valueCompare <= 0) //daty zgodne
                        {
                            if (transactions.Budgets.KBudgetID == 1)
                            {
                                incomes += transactions.Price;
                            }
                            else
                            {
                                expenses += transactions.Price;
                            }

                        }

                    }

                }
            }


            foreach (var debts in debtsCardModel.DebtCards)
            {
                float remaning = debts.Limit;
                foreach (var transactionsDebts in debtsCardModel.Transactions)
                {
                    valueCompare = DateTime.Compare(transactionsDebts.Date, (DateTime)now);
                    if (valueCompare <= 0)
                    {
                        valueCompare = DateTime.Compare((DateTime)searchTermStart, transactionsDebts.Date);
                        {
                            if (valueCompare <= 0) //daty zgodne
                            {
                                if (transactionsDebts.BudgetsName == debts.Name)
                                {
                                    remaning -= transactionsDebts.Price;
                                    debts.Price = remaning;
                                }
                            }
                        }
                    }
                }
                model.DebtCardRemaning = debts.Limit - remaning;
                countRemaningLimit += model.DebtCardRemaning;
            }

            model.Incomes = incomes;
            model.Expenses = expenses;

        model.DebtCardRemaning = countRemaningLimit;
            
            return model;
        }

        public BalanceListingModel GetBalancesByDate(DateTime? searchTermStart, DateTime? searchTermEnd)
        {
            BalanceListingModel model = new BalanceListingModel();
            var context = new HBMContext();
            float incomes = 0, expenses = 0;
            int valueCompare = 0;
            float countRemaningLimit = 0;
            DebtCardsListingModel debtsCardModel = new DebtCardsListingModel();
            DebtCardServices debtCardServices = new DebtCardServices();
            TransactionsServices transactionsServices = new TransactionsServices();

            debtsCardModel.DebtCards = debtCardServices.GetAllDebtCards();
            debtsCardModel.Transactions = transactionsServices.GetAllTransactions();



            foreach (var transactions in context.Transactions)
            {
                valueCompare = DateTime.Compare(transactions.Date, (DateTime)searchTermEnd);
                if (valueCompare <= 0)
                {
                    valueCompare = DateTime.Compare((DateTime)searchTermStart, transactions.Date);
                    {
                        if (valueCompare <= 0) //daty zgodne
                        {
                            if (transactions.Budgets.KBudgetID == 1)
                            {
                                incomes += transactions.Price;
                            }
                            else
                            {
                                expenses += transactions.Price;
                            }
                        }

                    }
                    
                }
            }

            foreach (var debts in debtsCardModel.DebtCards)
            {
                float remaning = debts.Limit;
                foreach (var transactionsDebts in debtsCardModel.Transactions)
                {
                    valueCompare = DateTime.Compare(transactionsDebts.Date, (DateTime)searchTermEnd);
                    if (valueCompare <= 0)
                    {
                        valueCompare = DateTime.Compare((DateTime)searchTermStart, transactionsDebts.Date);
                        {
                            if (valueCompare <= 0) //daty zgodne
                            {
                                if (transactionsDebts.BudgetsName == debts.Name)
                                {
                                    remaning -= transactionsDebts.Price;
                                    debts.Price = remaning;
                                }
                            }
                        }
                    }
                }
                model.DebtCardRemaning = debts.Limit - remaning;
                countRemaningLimit += model.DebtCardRemaning;
            }
            model.Incomes = incomes;
            model.Expenses = expenses;

           model.DebtCardRemaning = countRemaningLimit;
            return model;

        }

        [HttpGet]
        public ActionResult CharterBar(BalanceListingModel model)
        {
            var context = new HBMContext();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();
         
            // balance = GetBalances();
            xValue.Add("Przychody");
            xValue.Add("Wydatki");

            yValue.Add(model.Incomes);
            yValue.Add(model.Expenses);

            new Chart(width: 600, height: 400, theme: Color.CHARTS_THEME)
                .AddSeries("Default", xValue: xValue, yValues: yValue)
                .Write("bmp");

            return null;
            ///*64, 165, 191, 228*/
            /////163, 163, 163, 163
        }





        public class Color {
            public const String CHARTS_THEME = @"<Chart BackColor=""#EFEFEF"" BackGradientStyle=""TopBottom"" BorderColor=""#A0A0A0"" BorderWidth=""1"" Palette=""None"" PaletteCustomColors=""#cc67f4"" >
<ChartAreas>
<ChartArea Name=""Default"" _Template_=""All"" BackColor=""#EFEFEF"" BackSecondaryColor=""White"" BorderWidth=""1"" BorderColor=""#A0A0A0"" BorderDashStyle=""Solid"" >
<AxisY>
<MajorGrid Interval=""Auto"" LineColor=""64, 64, 64, 64"" />    
<LabelStyle Font=""Verdana, 10pt"" />
</AxisY>
<AxisX LineColor=""#000000"">
<MajorGrid Interval=""Auto"" LineColor=""64, 64, 64, 64"" />
<LabelStyle Font=""Verdana, 10pt"" />
</AxisX>
</ChartArea>
</ChartAreas>
<Legends>
<Legend _Template_=""All"" BackColor=""Transparent"" Docking=""Bottom"" Font=""Verdana, 10pt, style=Plain"" LegendStyle=""Row"">
</Legend>
    </Legends>                          
</Chart>";
        }
    }
}