using System;
using System.ComponentModel.DataAnnotations;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// Model for Other Expenses
    /// </summary>
    public class OtherExpense
  {
        public int Id { get; set; }

  [Required(ErrorMessage = "Expense name is required")]
      [StringLength(200)]
 [Display(Name = "Expense Name")]
        public string ExpenseName { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

     [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount
        {
        get { return Price; }
        }

   [Display(Name = "Date Added")]
  [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

   [StringLength(500)]
        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

  [StringLength(100)]
        [Display(Name = "Category")]
 public string Category { get; set; }
    }
}
