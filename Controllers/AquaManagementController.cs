using System.Web.Mvc;
using SampleMVCApplication.Models;

namespace SampleMVCApplication.Controllers
{
    /// <summary>
  /// Main dashboard controller for Aqua Management
    /// Shows Profit and Loss combining Revenue and Expenses
    /// </summary>
    public class AquaManagementController : Controller
    {
  // Repositories for expenses
      private FeedExpenseRepository _feedExpenseRepo = new FeedExpenseRepository();
        private MineralExpenseRepository _mineralExpenseRepo = new MineralExpenseRepository();
 private OtherExpenseRepository _otherExpenseRepo = new OtherExpenseRepository();

        // Repositories for revenue
 private PrawnRevenueRepository _prawnRevenueRepo = new PrawnRevenueRepository();
     private BagRevenueRepository _bagRevenueRepo = new BagRevenueRepository();

    /// <summary>
/// Main Profit and Loss Dashboard
        /// </summary>
    public ActionResult Index()
   {
       ViewBag.SuccessMessage = TempData["SuccessMessage"];

            // Gather all expense data
 var feedExpenses = _feedExpenseRepo.GetAll();
         var mineralExpenses = _mineralExpenseRepo.GetAll();
       var otherExpenses = _otherExpenseRepo.GetAll();

   // Gather all revenue data
   var prawnRevenues = _prawnRevenueRepo.GetAll();
  var bagRevenues = _bagRevenueRepo.GetAll();

      // Create comprehensive dashboard
     var dashboard = new AquaManagementDashboardViewModel
  {
 // Revenue totals
   TotalPrawnRevenue = _prawnRevenueRepo.GetTotalRevenue(),
       TotalBagRevenue = _bagRevenueRepo.GetTotalRevenue(),

   // Expense totals
 TotalFeedExpenses = _feedExpenseRepo.GetTotalExpenses(),
  TotalMineralExpenses = _mineralExpenseRepo.GetTotalExpenses(),
      TotalOtherExpenses = _otherExpenseRepo.GetTotalExpenses(),

   // Counts
    TotalRevenueEntries = prawnRevenues.Count + bagRevenues.Count,
  TotalExpenseEntries = feedExpenses.Count + mineralExpenses.Count + otherExpenses.Count,

    LastUpdated = System.DateTime.Now
   };

     ViewBag.Dashboard = dashboard;

   // Pass detailed data for charts/lists
    ViewBag.FeedExpenses = feedExpenses;
     ViewBag.MineralExpenses = mineralExpenses;
   ViewBag.OtherExpenses = otherExpenses;
  ViewBag.PrawnRevenues = prawnRevenues;
       ViewBag.BagRevenues = bagRevenues;

            return View();
   }
    }
}
