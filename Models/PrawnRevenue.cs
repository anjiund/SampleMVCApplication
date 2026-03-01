using System;
using System.ComponentModel.DataAnnotations;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// Model for Prawn Revenue
    /// </summary>
    public class PrawnRevenue
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Prawn quantity is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Quantity must be greater than 0")]
      [Display(Name = "Prawn Quantity (kg)")]
        public decimal PrawnQuantity { get; set; }

        [Required(ErrorMessage = "Price per kg is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Price per kg")]
      [DataType(DataType.Currency)]
        public decimal PricePerKg { get; set; }

        [Display(Name = "Total Revenue")]
        [DataType(DataType.Currency)]
     public decimal TotalRevenue
    {
            get { return PrawnQuantity * PricePerKg; }
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
    [Display(Name = "Prawn Grade")]
        public string PrawnGrade { get; set; }
    }
}
