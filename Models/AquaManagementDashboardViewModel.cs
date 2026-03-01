using System;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// ViewModel for Aqua Management Dashboard
    /// Shows Profit and Loss analysis combining Revenue and Expenses
 /// </summary>
  public class AquaManagementDashboardViewModel
  {
        // Revenue Data
        public decimal TotalPrawnRevenue { get; set; }
        public decimal TotalBagRevenue { get; set; }
 public decimal TotalRevenue
        {
            get { return TotalPrawnRevenue + TotalBagRevenue; }
        }

        // Expense Data
        public decimal TotalFeedExpenses { get; set; }
        public decimal TotalMineralExpenses { get; set; }
        public decimal TotalOtherExpenses { get; set; }
        public decimal TotalExpenses
 {
            get { return TotalFeedExpenses + TotalMineralExpenses + TotalOtherExpenses; }
   }

    // Profit and Loss
     public decimal NetProfitLoss
 {
get { return TotalRevenue - TotalExpenses; }
        }

        public bool IsProfitable
        {
        get { return NetProfitLoss > 0; }
 }

        public decimal ProfitMargin
        {
      get 
     { 
     if (TotalRevenue == 0) return 0;
     return (NetProfitLoss / TotalRevenue) * 100; 
      }
        }

        // Counts
        public int TotalRevenueEntries { get; set; }
      public int TotalExpenseEntries { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
