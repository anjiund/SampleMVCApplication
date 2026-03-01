using System;
using System.ComponentModel.DataAnnotations;

namespace SampleMVCApplication.Models
{
  /// <summary>
    /// Model for Bag Revenue
/// </summary>
 public class BagRevenue
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Number of bags is required")]
      [Range(1, 999999, ErrorMessage = "Number of bags must be at least 1")]
      [Display(Name = "Number of Bags")]
        public int NumberOfBags { get; set; }

        [Required(ErrorMessage = "Price per bag is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be greater than 0")]
      [Display(Name = "Price per Bag")]
        [DataType(DataType.Currency)]
 public decimal PricePerBag { get; set; }

   [Display(Name = "Total Revenue")]
 [DataType(DataType.Currency)]
     public decimal TotalRevenue
        {
        get { return NumberOfBags * PricePerBag; }
        }

        [Display(Name = "Sale Date")]
        [DataType(DataType.Date)]
        public DateTime SaleDate { get; set; }

        [StringLength(100)]
        [Display(Name = "Buyer Name")]
        public string BuyerName { get; set; }

  [StringLength(500)]
        [Display(Name = "Notes")]
     [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

    [StringLength(50)]
    [Display(Name = "Bag Type")]
        public string BagType { get; set; }
    }
}
