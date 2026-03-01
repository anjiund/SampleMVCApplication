using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// Repository for managing Mineral Expenses
    /// </summary>
    public class MineralExpenseRepository
    {
        // Static list simulating database
   private static List<MineralExpense> _mineralExpenses = new List<MineralExpense>
        {
 new MineralExpense { Id = 1, Quantity = 50, Price = 120.00m, MineralType = "Calcium", DateAdded = DateTime.Now.AddDays(-4), Notes = "Calcium supplement" },
        new MineralExpense { Id = 2, Quantity = 30, Price = 150.00m, MineralType = "Magnesium", DateAdded = DateTime.Now.AddDays(-2), Notes = "Magnesium for water treatment" }
        };

        public List<MineralExpense> GetAll()
        {
      return _mineralExpenses.OrderByDescending(m => m.DateAdded).ToList();
        }

   public MineralExpense GetById(int id)
        {
       return _mineralExpenses.FirstOrDefault(m => m.Id == id);
   }

        public void Add(MineralExpense expense)
   {
 expense.Id = _mineralExpenses.Any() ? _mineralExpenses.Max(m => m.Id) + 1 : 1;
    expense.DateAdded = DateTime.Now;
  _mineralExpenses.Add(expense);
        }

        public bool Update(MineralExpense expense)
  {
       var existing = GetById(expense.Id);
      if (existing == null)
         return false;

existing.Quantity = expense.Quantity;
     existing.Price = expense.Price;
        existing.MineralType = expense.MineralType;
     existing.Notes = expense.Notes;

   return true;
   }

        public bool Delete(int id)
        {
            var expense = GetById(id);
 if (expense == null)
   return false;

   _mineralExpenses.Remove(expense);
  return true;
}

   public decimal GetTotalExpenses()
  {
return _mineralExpenses.Sum(m => m.TotalAmount);
   }
    }
}
