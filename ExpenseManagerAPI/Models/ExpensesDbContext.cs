namespace ExpenseManagerAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ExpensesDbContext : DbContext
    {
        public ExpensesDbContext(): base("name=ExpensesDbContext")
        {
        }

        public DbSet<Expense> Expenses { get; set; }
    }
}