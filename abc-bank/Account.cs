﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        private readonly int accountType;
        public List<Transaction> transactions;

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(double amount, DateTime date) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount, date));
            }
        }

        public void Withdraw(double amount, DateTime date) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount, date));
            }
        }

        public double InterestEarned() 
        {
            double amount = sumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    
                    if (CheckWithdrawalforLast10Days(10,out double withdrawalamount))
                    {
                        return (withdrawalamount * 0.05)  + ((amount - withdrawalamount) * 0.001);
                    }
                    else
                    {
                        return amount * 0.001;
                    }                     
                default:
                    return amount * 0.001;
            }
        }

        public double sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll) 
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public int GetAccountType() 
        {
            return accountType;
        }

        private bool CheckWithdrawalforLast10Days(int numberofdays, out double withdrawalamount)
        {
            withdrawalamount = 0;
            bool result = false;
            foreach (Transaction t in transactions)
            {
                if (t.transactiondate.AddDays(numberofdays) > DateTime.Today)
                {
                    withdrawalamount += t.amount;
                    result = true;
                }
            }
            return result;
        }
    }
}
