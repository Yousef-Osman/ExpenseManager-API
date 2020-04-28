using ExpenseManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseManagerAPI.Controllers
{
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
    }
}
