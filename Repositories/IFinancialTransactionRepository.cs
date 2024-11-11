using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialTransactionApi.Models;

namespace FinancialTransactionApi.Repositories
{
    public interface IFinancialTransactionRepository
    {
        Task<IEnumerable<FinancialTransaction>> GetAllTransactionsAsync();
        Task<FinancialTransaction> GetTransactionByIdAsync(int id);
        Task<FinancialTransaction> CreateTransactionAsync(FinancialTransaction transaction);
        Task<FinancialTransaction> UpdateTransactionAsync(FinancialTransaction transaction);
        Task<bool> DeleteTransactionAsync(int id);
    }
}