using System;
using System.ComponentModel.DataAnnotations;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// Model for Feed Expenses
    /// </summary>
    public class FeedExpense
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Feed quantity is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Quantity must be greater than 0")]
        [Display(Name = "Feed Quantity (kg)")]
        public decimal FeedQuantity { get; set; }

        [Required(ErrorMessage = "Feed price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Feed Price per kg")]
        [DataType(DataType.Currency)]
        public decimal FeedPrice { get; set; }

        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount
        {
          get { return FeedQuantity * FeedPrice; }
        }

  [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

        [StringLength(500)]
        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }
}
