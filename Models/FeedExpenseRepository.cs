using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// Repository for managing Feed Expenses
    /// </summary>
    public class FeedExpenseRepository
    {
        // Static list simulating database
        private static List<FeedExpense> _feedExpenses = new List<FeedExpense>
        {
      new FeedExpense { Id = 1, FeedQuantity = 100, FeedPrice = 45.50m, DateAdded = DateTime.Now.AddDays(-5), Notes = "Initial feed purchase" },
            new FeedExpense { Id = 2, FeedQuantity = 150, FeedPrice = 44.00m, DateAdded = DateTime.Now.AddDays(-3), Notes = "Regular feed supply" },
          new FeedExpense { Id = 3, FeedQuantity = 200, FeedPrice = 43.50m, DateAdded = DateTime.Now.AddDays(-1), Notes = "Bulk purchase discount" }
        };

        public List<FeedExpense> GetAll()
        {
    return _feedExpenses.OrderByDescending(f => f.DateAdded).ToList();
   }

        public FeedExpense GetById(int id)
        {
            return _feedExpenses.FirstOrDefault(f => f.Id == id);
   }

      public void Add(FeedExpense expense)
        {
        expense.Id = _feedExpenses.Any() ? _feedExpenses.Max(f => f.Id) + 1 : 1;
            expense.DateAdded = DateTime.Now;
      _feedExpenses.Add(expense);
        }

   public bool Update(FeedExpense expense)
    {
     var existing = GetById(expense.Id);
         if (existing == null)
 return false;

            existing.FeedQuantity = expense.FeedQuantity;
        existing.FeedPrice = expense.FeedPrice;
     existing.Notes = expense.Notes;

     return true;
   }

        public bool Delete(int id)
        {
            var expense = GetById(id);
         if (expense == null)
       return false;

_feedExpenses.Remove(expense);
       return true;
        }

        public decimal GetTotalExpenses()
        {
        return _feedExpenses.Sum(f => f.TotalAmount);
   }
    }
}
