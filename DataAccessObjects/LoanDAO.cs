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
        private readonly LibraryManagementDbContext _ctx;
        public LoanDAO(LibraryManagementDbContext ctx) => _ctx = ctx;

        public IEnumerable<Loan> GetAll() => _ctx.Loans.AsNoTracking().Include(p => p.Copy).ToList();
        public Loan? GetById(int id) => _ctx.Loans.Include(p => p.Copy).FirstOrDefault(p => p.LoanId == id);

        public void Create(Loan p) { _ctx.Loans.Add(p); _ctx.SaveChanges(); }
        public void Update(Loan p) { _ctx.Loans.Update(p); _ctx.SaveChanges(); }
        public void Delete(Loan p) { _ctx.Loans.Remove(p); _ctx.SaveChanges(); }
    }
}
