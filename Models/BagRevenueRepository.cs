using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// Repository for managing Bag Revenue
    /// </summary>
    public class BagRevenueRepository
    {
 // Static list simulating database
        private static List<BagRevenue> _bagRevenues = new List<BagRevenue>
        {
            new BagRevenue { Id = 1, NumberOfBags = 50, PricePerBag = 25.00m, BagType = "Small (5kg)", BuyerName = "Retail Store A", SaleDate = DateTime.Now.AddDays(-6), Notes = "Small bag sales" },
            new BagRevenue { Id = 2, NumberOfBags = 30, PricePerBag = 45.00m, BagType = "Medium (10kg)", BuyerName = "Retail Store B", SaleDate = DateTime.Now.AddDays(-4), Notes = "Medium bag sales" },
    new BagRevenue { Id = 3, NumberOfBags = 20, PricePerBag = 85.00m, BagType = "Large (20kg)", BuyerName = "Wholesale Buyer", SaleDate = DateTime.Now.AddDays(-1), Notes = "Bulk sale" }
    };

   public List<BagRevenue> GetAll()
      {
            return _bagRevenues.OrderByDescending(b => b.SaleDate).ToList();
        }

        public BagRevenue GetById(int id)
        {
        return _bagRevenues.FirstOrDefault(b => b.Id == id);
        }

        public void Add(BagRevenue revenue)
        {
          revenue.Id = _bagRevenues.Any() ? _bagRevenues.Max(b => b.Id) + 1 : 1;
            revenue.SaleDate = DateTime.Now;
     _bagRevenues.Add(revenue);
        }

  public bool Update(BagRevenue revenue)
        {
  var existing = GetById(revenue.Id);
    if (existing == null)
      return false;

 existing.NumberOfBags = revenue.NumberOfBags;
existing.PricePerBag = revenue.PricePerBag;
      existing.BagType = revenue.BagType;
            existing.BuyerName = revenue.BuyerName;
            existing.Notes = revenue.Notes;

            return true;
        }

public bool Delete(int id)
        {
      var revenue = GetById(id);
       if (revenue == null)
     return false;

            _bagRevenues.Remove(revenue);
       return true;
    }

 public decimal GetTotalRevenue()
        {
       return _bagRevenues.Sum(b => b.TotalRevenue);
        }

public List<string> GetBagTypes()
        {
      return _bagRevenues
                .Select(b => b.BagType)
     .Where(t => !string.IsNullOrEmpty(t))
                .Distinct()
     .OrderBy(t => t)
    .ToList();
        }
    }
}
