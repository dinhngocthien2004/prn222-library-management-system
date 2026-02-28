using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class LoanDAO
    {
        private static LoanDAO instance = null;
        private static readonly object instanceLock = new object();

        private LoanDAO() { }

        public static LoanDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LoanDAO();
                    }
                    return instance;
                }
            }
        }

        // =========================
        // Get All
        // =========================
        public List<Loan> GetLoanList()
        {
            var listLoans = new List<Loan>();
            try
            {
                using var context = new LibraryManagementDbContext();
                listLoans = context.Loans
                                   .Include(l => l.User)
                                   .Include(l => l.Copy)
                                   .ToList();
            }
            catch
            {
                throw;
            }
            return listLoans;
        }

        // =========================
        // Get By Id
        // =========================
        public Loan GetLoanById(int loanId)
        {
            try
            {
                using var context = new LibraryManagementDbContext();
                return context.Loans
                              .Include(l => l.User)
                              .Include(l => l.Copy)
                              .SingleOrDefault(l => l.LoanId == loanId);
            }
            catch
            {
                throw;
            }
        }

        // =========================
        // Add
        // =========================
        public void AddLoan(Loan loan)
        {
            try
            {
                using var context = new LibraryManagementDbContext();

                loan.LoanDate = DateTime.Now;
                loan.Status = 1; // 1 = Borrowing

                context.Loans.Add(loan);
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        // =========================
        // Update
        // =========================
        public void UpdateLoan(Loan loan)
        {
            try
            {
                using var context = new LibraryManagementDbContext();
                context.Entry<Loan>(loan).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteLoan(int loanId)
        {
            try
            {
                using var context = new LibraryManagementDbContext();
                var loan = context.Loans.SingleOrDefault(l => l.LoanId == loanId);
                if (loan != null)
                {
                    context.Loans.Remove(loan);
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
