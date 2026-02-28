using System;
using System.Collections.Generic;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// ViewModel for the About page
    /// Demonstrates how to pass structured data from Controller to View
    /// </summary>
    public class AboutViewModel
    {
        public string Title { get; set; }
    public string Message { get; set; }
        public string CompanyName { get; set; }
        public DateTime FoundedDate { get; set; }
        public List<string> Features { get; set; }

        public AboutViewModel()
    {
   Features = new List<string>();
    }
    }
}
