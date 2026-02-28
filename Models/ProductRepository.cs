using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleMVCApplication.Models
{
  /// <summary>
    /// Repository Pattern: Separates data access logic from business logic
    /// In real applications, this would interact with a database using Entity Framework or ADO.NET
    /// This example uses in-memory data for demonstration
    /// </summary>
    public class ProductRepository
    {
        // Static list simulating a database
  private static List<Product> _products = new List<Product>
        {
    new Product { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 1299.99m, IsAvailable = true, Category = "Electronics", CreatedDate = DateTime.Now.AddDays(-30) },
          new Product { Id = 2, Name = "Mouse", Description = "Wireless mouse", Price = 29.99m, IsAvailable = true, Category = "Electronics", CreatedDate = DateTime.Now.AddDays(-20) },
            new Product { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", Price = 89.99m, IsAvailable = true, Category = "Electronics", CreatedDate = DateTime.Now.AddDays(-15) },
            new Product { Id = 4, Name = "Monitor", Description = "27-inch 4K monitor", Price = 449.99m, IsAvailable = false, Category = "Electronics", CreatedDate = DateTime.Now.AddDays(-10) },
    new Product { Id = 5, Name = "Desk Chair", Description = "Ergonomic office chair", Price = 299.99m, IsAvailable = true, Category = "Furniture", CreatedDate = DateTime.Now.AddDays(-5) }
        };

  /// <summary>
        /// Get all products
     /// </summary>
        public List<Product> GetAll()
  {
         return _products.ToList();
        }

        /// <summary>
        /// Get products by category
        /// </summary>
   public List<Product> GetByCategory(string category)
     {
            if (string.IsNullOrEmpty(category))
  return GetAll();

         return _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Get available products only
        /// </summary>
        public List<Product> GetAvailableProducts()
  {
            return _products.Where(p => p.IsAvailable).ToList();
        }

        /// <summary>
        /// Get a single product by ID
        /// </summary>
        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
   }

        /// <summary>
        /// Add a new product
      /// </summary>
        public void Add(Product product)
      {
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
    product.CreatedDate = DateTime.Now;
     _products.Add(product);
        }

      /// <summary>
      /// Update an existing product
      /// </summary>
  public bool Update(Product product)
     {
            var existing = GetById(product.Id);
            if (existing == null)
        return false;

            existing.Name = product.Name;
     existing.Description = product.Description;
            existing.Price = product.Price;
            existing.IsAvailable = product.IsAvailable;
       existing.Category = product.Category;

            return true;
  }

 /// <summary>
        /// Delete a product
      /// </summary>
        public bool Delete(int id)
 {
    var product = GetById(id);
   if (product == null)
       return false;

         _products.Remove(product);
            return true;
        }

  /// <summary>
   /// Get all unique categories
        /// </summary>
     public List<string> GetCategories()
        {
            return _products.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();
        }
    }
}
