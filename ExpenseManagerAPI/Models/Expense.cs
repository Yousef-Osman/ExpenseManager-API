using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpenseManagerAPI.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public String Description { get; set; }
        public bool IsExpense { get; set; }
        public double Value { get; set; }
    }
}