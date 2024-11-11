using System.Threading.Tasks;
using System.Collections.Generic;
using FinancialTransactionApi.Data;
using Microsoft.EntityFrameworkCore;
using FinancialTransactionApi.Models;

namespace FinancialTransactionApi.Repositories
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public FinancialTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FinancialTransaction>> GetAllTransactionsAsync()
        {
            return await _context.FinancialTransactions.ToListAsync();
        }

        public async Task<FinancialTransaction> GetTransactionByIdAsync(int id)
        {
            return await _context.FinancialTransactions.FindAsync(id);
        }

        public async Task<FinancialTransaction> CreateTransactionAsync(FinancialTransaction transaction)
        {
            _context.FinancialTransactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<FinancialTransaction> UpdateTransactionAsync(FinancialTransaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _context.FinancialTransactions.FindAsync(id);
            if (transaction == null)
                return false;

            _context.FinancialTransactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}