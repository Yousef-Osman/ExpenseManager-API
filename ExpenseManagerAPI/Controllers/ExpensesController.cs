using ExpenseManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ExpenseManagerAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ExpensesController : ApiController
    {
        public IHttpActionResult GetExpenses()
        {
			try
			{
                using (ExpensesDbContext db = new ExpensesDbContext())
                {
                    var expenses = db.Expenses.ToList();
                    return Ok(expenses);
                }
            }
			catch (Exception ex)
			{
                return BadRequest(ex.Message);
			}
        }

        public IHttpActionResult GetExpenses(int id)
        {
            try
            {
                using (ExpensesDbContext db = new ExpensesDbContext())
                {
                    var expense = db.Expenses.Where(e => e.Id == id).SingleOrDefault();
                    var expense1 = db.Expenses.Find(id);
                    return Ok(expense);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult PostExpenses(Expense exp)
        {
            try
            {
                using (ExpensesDbContext db = new ExpensesDbContext())
                {
                    db.Entry(exp).State = (exp.Id == 0) ? EntityState.Added : EntityState.Modified;
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteExpenses(int id)
        {
            try
            {
                using (ExpensesDbContext db = new ExpensesDbContext())
                {
                    var exp = db.Expenses.Where(e => e.Id == id).SingleOrDefault();
                    db.Entry(exp).State = EntityState.Deleted;
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
