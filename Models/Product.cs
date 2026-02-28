using System;
using System.ComponentModel.DataAnnotations;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// Domain Model representing a Product entity
    /// In real applications, this would map to a database table
    /// </summary>
    public class Product
  {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
    public string Name { get; set; }

        [StringLength(500)]
 public string Description { get; set; }

 [Required]
        [Range(0.01, 999999.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "In Stock")]
      public bool IsAvailable { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
