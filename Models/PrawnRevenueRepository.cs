using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// Repository for managing Prawn Revenue
    /// </summary>
  public class PrawnRevenueRepository
    {
        // Static list simulating database
        private static List<PrawnRevenue> _prawnRevenues = new List<PrawnRevenue>
        {
       new PrawnRevenue { Id = 1, PrawnQuantity = 250, PricePerKg = 180.00m, PrawnGrade = "Grade A", BuyerName = "Ocean Foods Ltd", SaleDate = DateTime.Now.AddDays(-7), Notes = "First harvest sale" },
  new PrawnRevenue { Id = 2, PrawnQuantity = 300, PricePerKg = 175.00m, PrawnGrade = "Grade A", BuyerName = "Fresh Seafood Corp", SaleDate = DateTime.Now.AddDays(-5), Notes = "Regular buyer" },
         new PrawnRevenue { Id = 3, PrawnQuantity = 150, PricePerKg = 160.00m, PrawnGrade = "Grade B", BuyerName = "Local Market", SaleDate = DateTime.Now.AddDays(-2), Notes = "Local market sale" }
        };

public List<PrawnRevenue> GetAll()
        {
       return _prawnRevenues.OrderByDescending(p => p.SaleDate).ToList();
        }

        public PrawnRevenue GetById(int id)
        {
         return _prawnRevenues.FirstOrDefault(p => p.Id == id);
   }

    public void Add(PrawnRevenue revenue)
        {
revenue.Id = _prawnRevenues.Any() ? _prawnRevenues.Max(p => p.Id) + 1 : 1;
    revenue.SaleDate = DateTime.Now;
          _prawnRevenues.Add(revenue);
        }

        public bool Update(PrawnRevenue revenue)
        {
            var existing = GetById(revenue.Id);
            if (existing == null)
       return false;

 existing.PrawnQuantity = revenue.PrawnQuantity;
  existing.PricePerKg = revenue.PricePerKg;
         existing.PrawnGrade = revenue.PrawnGrade;
            existing.BuyerName = revenue.BuyerName;
            existing.Notes = revenue.Notes;

            return true;
        }

        public bool Delete(int id)
        {
 var revenue = GetById(id);
          if (revenue == null)
         return false;

      _prawnRevenues.Remove(revenue);
     return true;
        }

    public decimal GetTotalRevenue()
  {
        return _prawnRevenues.Sum(p => p.TotalRevenue);
        }

     public List<string> GetPrawnGrades()
    {
 return _prawnRevenues
  .Select(p => p.PrawnGrade)
  .Where(g => !string.IsNullOrEmpty(g))
     .Distinct()
        .OrderBy(g => g)
     .ToList();
        }
 }
}
