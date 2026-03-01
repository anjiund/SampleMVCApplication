using System;

namespace SampleMVCApplication.Models
{
    /// <summary>
    /// ViewModel for Aqua Revenue Dashboard
    /// Aggregates data from all revenue categories
    /// </summary>
    public class AquaRevenueDashboardViewModel
  {
        public decimal TotalPrawnRevenue { get; set; }
        public decimal TotalBagRevenue { get; set; }

        public decimal GrandTotal
        {
      get { return TotalPrawnRevenue + TotalBagRevenue; }
        }

        public int PrawnRevenueCount { get; set; }
    public int BagRevenueCount { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
