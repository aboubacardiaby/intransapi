using System;

namespace FinancialTransactionApi.Models
{
    public class FinancialTransaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } // e.g., "Deposit", "Withdrawal", "Transfer"
        public string AccountNumber { get; set; }
    }
}