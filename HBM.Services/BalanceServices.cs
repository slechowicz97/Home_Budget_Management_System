using HBM.Data;
using HBM.Data.Migrations;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balance = HBM.Entities.Balance;

namespace HBM.Services
{
    public class BalanceServices
    {
        HBMContext context = new HBMContext();
        public Entities.Balance SearchBalances(DateTime searchTermStart, DateTime searchTermEnd)
        {
            int valueCompare = 0;
            float incomes = 0, expenses = 0;

            if (!string.IsNullOrEmpty(searchTermStart.ToLongDateString()) && !string.IsNullOrEmpty(searchTermEnd.ToLongTimeString()))
            {
                valueCompare = DateTime.Compare(searchTermStart, searchTermEnd);
                if (valueCompare <= 0)
                {
                    foreach (var balanceRecord in context.Transactions)
                    {
                        valueCompare = DateTime.Compare(balanceRecord.Date, searchTermEnd);
                        if (valueCompare <= 0)
                        {
                            valueCompare = DateTime.Compare(searchTermStart, balanceRecord.Date);
                            if (valueCompare <= 0)
                            {
                                if (balanceRecord.Budgets.KBudgetID == 1)
                                {
                                    incomes += balanceRecord.Price;
                                }
                                else
                                {
                                    expenses += balanceRecord.Price;
                                }
                            }
                        }
                    }
                }
            }

            Entities.Balance balance = new Balance();
            balance.Incomes = incomes;
            balance.Expenses = expenses;

            return balance;
        }
    }
}

