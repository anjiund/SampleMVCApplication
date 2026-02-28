using System;
using System.ComponentModel.DataAnnotations;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// Model for Contact form demonstrating how data flows from View ? Controller ? Model
    /// Includes validation attributes
    /// </summary>
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Your Name")]
  public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is required")]
  [StringLength(200, ErrorMessage = "Subject cannot exceed 200 characters")]
     public string Subject { get; set; }

  [Required(ErrorMessage = "Message is required")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 1000 characters")]
   [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public DateTime SubmittedDate { get; set; }
    }
}
