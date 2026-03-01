using System;

namespace SampleMVCApplication.Models
{
    /// <summary>
 /// ViewModel for Aqua Expenses Dashboard
    /// Aggregates data from all expense categories
    /// </summary>
    public class AquaExpensesDashboardViewModel
 {
 public decimal TotalFeedExpenses { get; set; }
public decimal TotalMineralExpenses { get; set; }
        public decimal TotalOtherExpenses { get; set; }

 public decimal GrandTotal
   {
      get { return TotalFeedExpenses + TotalMineralExpenses + TotalOtherExpenses; }
   }

 public int FeedExpenseCount { get; set; }
   public int MineralExpenseCount { get; set; }
        public int OtherExpenseCount { get; set; }

        public DateTime LastUpdated { get; set; }
  }
}
