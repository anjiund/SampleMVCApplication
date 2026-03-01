using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// Repository for managing Other Expenses
  /// </summary>
    public class OtherExpenseRepository
    {
        // Static list simulating database
  private static List<OtherExpense> _otherExpenses = new List<OtherExpense>
        {
      new OtherExpense { Id = 1, ExpenseName = "Electricity", Price = 350.00m, Category = "Utilities", DateAdded = DateTime.Now.AddDays(-6), Notes = "Monthly electricity bill" },
   new OtherExpense { Id = 2, ExpenseName = "Water Supply", Price = 150.00m, Category = "Utilities", DateAdded = DateTime.Now.AddDays(-4), Notes = "Water charges" },
new OtherExpense { Id = 3, ExpenseName = "Equipment Maintenance", Price = 500.00m, Category = "Maintenance", DateAdded = DateTime.Now.AddDays(-2), Notes = "Pump repair" }
        };

   public List<OtherExpense> GetAll()
        {
          return _otherExpenses.OrderByDescending(o => o.DateAdded).ToList();
   }

 public OtherExpense GetById(int id)
   {
     return _otherExpenses.FirstOrDefault(o => o.Id == id);
    }

   public void Add(OtherExpense expense)
 {
    expense.Id = _otherExpenses.Any() ? _otherExpenses.Max(o => o.Id) + 1 : 1;
       expense.DateAdded = DateTime.Now;
     _otherExpenses.Add(expense);
        }

  public bool Update(OtherExpense expense)
        {
            var existing = GetById(expense.Id);
if (existing == null)
   return false;

       existing.ExpenseName = expense.ExpenseName;
   existing.Price = expense.Price;
        existing.Category = expense.Category;
     existing.Notes = expense.Notes;

   return true;
   }

        public bool Delete(int id)
        {
       var expense = GetById(id);
  if (expense == null)
     return false;

       _otherExpenses.Remove(expense);
            return true;
   }

      public decimal GetTotalExpenses()
        {
       return _otherExpenses.Sum(o => o.TotalAmount);
   }

      public List<string> GetCategories()
   {
        return _otherExpenses
       .Select(o => o.Category)
         .Where(c => !string.IsNullOrEmpty(c))
            .Distinct()
      .OrderBy(c => c)
   .ToList();
        }
    }
}
