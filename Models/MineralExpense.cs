using System;
using System.ComponentModel.DataAnnotations;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// Model for Mineral Expenses
    /// </summary>
    public class MineralExpense
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mineral quantity is required")]
   [Range(0.01, 999999.99, ErrorMessage = "Quantity must be greater than 0")]
        [Display(Name = "Mineral Quantity (kg)")]
   public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Mineral price is required")]
    [Range(0.01, 999999.99, ErrorMessage = "Price must be greater than 0")]
   [Display(Name = "Price per kg")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Total Amount")]
   [DataType(DataType.Currency)]
        public decimal TotalAmount
   {
    get { return Quantity * Price; }
        }

   [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

        [StringLength(500)]
        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

 [StringLength(100)]
        [Display(Name = "Mineral Type")]
        public string MineralType { get; set; }
    }
}
