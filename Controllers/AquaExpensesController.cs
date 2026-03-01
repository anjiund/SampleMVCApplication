using System.Web.Mvc;
using SampleMVCApplication.Models;

namespace SampleMVCApplication.Controllers
{
    /// <summary>
  /// Main controller for Aqua Expenses Form application
 /// Manages all three expense categories with tabs
    /// </summary>
public class AquaExpensesController : Controller
  {
 // Repositories for data access
        private FeedExpenseRepository _feedRepo = new FeedExpenseRepository();
      private MineralExpenseRepository _mineralRepo = new MineralExpenseRepository();
  private OtherExpenseRepository _otherRepo = new OtherExpenseRepository();

        // ============================================================
        // DASHBOARD - Shows all expenses with tabs
        // ============================================================

        /// <summary>
        /// Main dashboard with all three tabs
        /// Default tab: Feed
   /// </summary>
   public ActionResult Index(string tab = "feed")
        {
    ViewBag.ActiveTab = tab.ToLower();
       ViewBag.SuccessMessage = TempData["SuccessMessage"];

       // Prepare dashboard summary
 var dashboard = new AquaExpensesDashboardViewModel
          {
      TotalFeedExpenses = _feedRepo.GetTotalExpenses(),
       TotalMineralExpenses = _mineralRepo.GetTotalExpenses(),
    TotalOtherExpenses = _otherRepo.GetTotalExpenses(),
FeedExpenseCount = _feedRepo.GetAll().Count,
        MineralExpenseCount = _mineralRepo.GetAll().Count,
         OtherExpenseCount = _otherRepo.GetAll().Count,
         LastUpdated = System.DateTime.Now
       };

         ViewBag.Dashboard = dashboard;

          // Load data based on active tab
        ViewBag.FeedExpenses = _feedRepo.GetAll();
   ViewBag.MineralExpenses = _mineralRepo.GetAll();
       ViewBag.OtherExpenses = _otherRepo.GetAll();

            return View();
        }

        // ============================================================
 // FEED EXPENSES - CRUD Operations
        // ============================================================

        [HttpGet]
   public ActionResult CreateFeed()
        {
            return View(new FeedExpense());
   }

   [HttpPost]
    [ValidateAntiForgeryToken]
        public ActionResult CreateFeed(FeedExpense expense)
      {
    if (ModelState.IsValid)
     {
    _feedRepo.Add(expense);
       TempData["SuccessMessage"] = "Feed expense added successfully!";
     return RedirectToAction("Index", new { tab = "feed" });
            }
 return View(expense);
   }

        [HttpGet]
      public ActionResult EditFeed(int id)
        {
       var expense = _feedRepo.GetById(id);
 if (expense == null)
                return HttpNotFound();
  return View(expense);
   }

  [HttpPost]
   [ValidateAntiForgeryToken]
     public ActionResult EditFeed(FeedExpense expense)
  {
if (ModelState.IsValid)
   {
   if (_feedRepo.Update(expense))
    {
      TempData["SuccessMessage"] = "Feed expense updated successfully!";
       return RedirectToAction("Index", new { tab = "feed" });
     }
             ModelState.AddModelError("", "Expense not found.");
     }
   return View(expense);
        }

 [HttpGet]
        public ActionResult DeleteFeed(int id)
   {
 var expense = _feedRepo.GetById(id);
   if (expense == null)
       return HttpNotFound();
  return View(expense);
 }

        [HttpPost, ActionName("DeleteFeed")]
   [ValidateAntiForgeryToken]
        public ActionResult DeleteFeedConfirmed(int id)
        {
   _feedRepo.Delete(id);
        TempData["SuccessMessage"] = "Feed expense deleted successfully!";
    return RedirectToAction("Index", new { tab = "feed" });
        }

      // ============================================================
        // MINERAL EXPENSES - CRUD Operations
 // ============================================================

        [HttpGet]
    public ActionResult CreateMineral()
{
            return View(new MineralExpense());
   }

        [HttpPost]
   [ValidateAntiForgeryToken]
     public ActionResult CreateMineral(MineralExpense expense)
        {
       if (ModelState.IsValid)
 {
    _mineralRepo.Add(expense);
   TempData["SuccessMessage"] = "Mineral expense added successfully!";
          return RedirectToAction("Index", new { tab = "minerals" });
       }
         return View(expense);
        }

  [HttpGet]
   public ActionResult EditMineral(int id)
        {
     var expense = _mineralRepo.GetById(id);
       if (expense == null)
     return HttpNotFound();
       return View(expense);
      }

   [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMineral(MineralExpense expense)
        {
        if (ModelState.IsValid)
        {
         if (_mineralRepo.Update(expense))
      {
     TempData["SuccessMessage"] = "Mineral expense updated successfully!";
  return RedirectToAction("Index", new { tab = "minerals" });
         }
        ModelState.AddModelError("", "Expense not found.");
    }
   return View(expense);
   }

 [HttpGet]
   public ActionResult DeleteMineral(int id)
        {
       var expense = _mineralRepo.GetById(id);
     if (expense == null)
         return HttpNotFound();
    return View(expense);
        }

        [HttpPost, ActionName("DeleteMineral")]
        [ValidateAntiForgeryToken]
  public ActionResult DeleteMineralConfirmed(int id)
        {
     _mineralRepo.Delete(id);
   TempData["SuccessMessage"] = "Mineral expense deleted successfully!";
      return RedirectToAction("Index", new { tab = "minerals" });
        }

  // ============================================================
  // OTHER EXPENSES - CRUD Operations
        // ============================================================

        [HttpGet]
   public ActionResult CreateOther()
      {
       ViewBag.Categories = _otherRepo.GetCategories();
          return View(new OtherExpense());
   }

        [HttpPost]
        [ValidateAntiForgeryToken]
   public ActionResult CreateOther(OtherExpense expense)
   {
        if (ModelState.IsValid)
       {
       _otherRepo.Add(expense);
    TempData["SuccessMessage"] = "Other expense added successfully!";
     return RedirectToAction("Index", new { tab = "other" });
      }
  ViewBag.Categories = _otherRepo.GetCategories();
       return View(expense);
        }

 [HttpGet]
  public ActionResult EditOther(int id)
        {
var expense = _otherRepo.GetById(id);
       if (expense == null)
     return HttpNotFound();
       ViewBag.Categories = _otherRepo.GetCategories();
return View(expense);
   }

        [HttpPost]
   [ValidateAntiForgeryToken]
        public ActionResult EditOther(OtherExpense expense)
        {
       if (ModelState.IsValid)
   {
       if (_otherRepo.Update(expense))
     {
     TempData["SuccessMessage"] = "Other expense updated successfully!";
   return RedirectToAction("Index", new { tab = "other" });
        }
    ModelState.AddModelError("", "Expense not found.");
       }
       ViewBag.Categories = _otherRepo.GetCategories();
     return View(expense);
  }

   [HttpGet]
   public ActionResult DeleteOther(int id)
     {
       var expense = _otherRepo.GetById(id);
     if (expense == null)
  return HttpNotFound();
       return View(expense);
        }

        [HttpPost, ActionName("DeleteOther")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOtherConfirmed(int id)
        {
       _otherRepo.Delete(id);
     TempData["SuccessMessage"] = "Other expense deleted successfully!";
   return RedirectToAction("Index", new { tab = "other" });
   }
    }
}
