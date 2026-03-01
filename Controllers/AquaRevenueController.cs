using System.Web.Mvc;
using SampleMVCApplication.Models;

namespace SampleMVCApplication.Controllers
{
    /// <summary>
    /// Main controller for Aqua Revenue application
    /// Manages revenue from prawn and bag sales
    /// </summary>
    public class AquaRevenueController : Controller
    {
        // Repositories for data access
        private PrawnRevenueRepository _prawnRepo = new PrawnRevenueRepository();
        private BagRevenueRepository _bagRepo = new BagRevenueRepository();

        // ============================================================
        // DASHBOARD - Shows all revenues with tabs
   // ============================================================

        /// <summary>
        /// Main dashboard with both revenue tabs
        /// Default tab: Prawn Revenue
/// </summary>
 public ActionResult Index(string tab = "prawn")
     {
       ViewBag.ActiveTab = tab.ToLower();
       ViewBag.SuccessMessage = TempData["SuccessMessage"];

       // Prepare dashboard summary
        var dashboard = new AquaRevenueDashboardViewModel
  {
   TotalPrawnRevenue = _prawnRepo.GetTotalRevenue(),
        TotalBagRevenue = _bagRepo.GetTotalRevenue(),
    PrawnRevenueCount = _prawnRepo.GetAll().Count,
     BagRevenueCount = _bagRepo.GetAll().Count,
   LastUpdated = System.DateTime.Now
         };

         ViewBag.Dashboard = dashboard;

   // Load data based on active tab
            ViewBag.PrawnRevenues = _prawnRepo.GetAll();
  ViewBag.BagRevenues = _bagRepo.GetAll();

       return View();
}

        // ============================================================
        // PRAWN REVENUE - CRUD Operations
        // ============================================================

        [HttpGet]
        public ActionResult CreatePrawn()
   {
     ViewBag.PrawnGrades = _prawnRepo.GetPrawnGrades();
     return View(new PrawnRevenue());
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
public ActionResult CreatePrawn(PrawnRevenue revenue)
        {
      if (ModelState.IsValid)
     {
         _prawnRepo.Add(revenue);
       TempData["SuccessMessage"] = "Prawn revenue added successfully!";
     return RedirectToAction("Index", new { tab = "prawn" });
         }
    ViewBag.PrawnGrades = _prawnRepo.GetPrawnGrades();
        return View(revenue);
        }

        [HttpGet]
        public ActionResult EditPrawn(int id)
        {
            var revenue = _prawnRepo.GetById(id);
    if (revenue == null)
  return HttpNotFound();
      ViewBag.PrawnGrades = _prawnRepo.GetPrawnGrades();
  return View(revenue);
        }

        [HttpPost]
    [ValidateAntiForgeryToken]
        public ActionResult EditPrawn(PrawnRevenue revenue)
        {
            if (ModelState.IsValid)
    {
        if (_prawnRepo.Update(revenue))
         {
          TempData["SuccessMessage"] = "Prawn revenue updated successfully!";
          return RedirectToAction("Index", new { tab = "prawn" });
          }
         ModelState.AddModelError("", "Revenue not found.");
         }
         ViewBag.PrawnGrades = _prawnRepo.GetPrawnGrades();
        return View(revenue);
        }

  [HttpGet]
   public ActionResult DeletePrawn(int id)
        {
            var revenue = _prawnRepo.GetById(id);
       if (revenue == null)
        return HttpNotFound();
     return View(revenue);
        }

     [HttpPost, ActionName("DeletePrawn")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePrawnConfirmed(int id)
        {
   _prawnRepo.Delete(id);
        TempData["SuccessMessage"] = "Prawn revenue deleted successfully!";
      return RedirectToAction("Index", new { tab = "prawn" });
   }

    // ============================================================
        // BAG REVENUE - CRUD Operations
   // ============================================================

        [HttpGet]
        public ActionResult CreateBag()
        {
 ViewBag.BagTypes = _bagRepo.GetBagTypes();
            return View(new BagRevenue());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
  public ActionResult CreateBag(BagRevenue revenue)
        {
            if (ModelState.IsValid)
            {
  _bagRepo.Add(revenue);
  TempData["SuccessMessage"] = "Bag revenue added successfully!";
                return RedirectToAction("Index", new { tab = "bag" });
            }
     ViewBag.BagTypes = _bagRepo.GetBagTypes();
            return View(revenue);
        }

        [HttpGet]
    public ActionResult EditBag(int id)
    {
          var revenue = _bagRepo.GetById(id);
   if (revenue == null)
     return HttpNotFound();
     ViewBag.BagTypes = _bagRepo.GetBagTypes();
       return View(revenue);
        }

        [HttpPost]
   [ValidateAntiForgeryToken]
        public ActionResult EditBag(BagRevenue revenue)
   {
    if (ModelState.IsValid)
      {
       if (_bagRepo.Update(revenue))
       {
           TempData["SuccessMessage"] = "Bag revenue updated successfully!";
       return RedirectToAction("Index", new { tab = "bag" });
       }
     ModelState.AddModelError("", "Revenue not found.");
    }
      ViewBag.BagTypes = _bagRepo.GetBagTypes();
            return View(revenue);
        }

        [HttpGet]
   public ActionResult DeleteBag(int id)
        {
          var revenue = _bagRepo.GetById(id);
            if (revenue == null)
                return HttpNotFound();
        return View(revenue);
 }

        [HttpPost, ActionName("DeleteBag")]
    [ValidateAntiForgeryToken]
        public ActionResult DeleteBagConfirmed(int id)
      {
    _bagRepo.Delete(id);
            TempData["SuccessMessage"] = "Bag revenue deleted successfully!";
        return RedirectToAction("Index", new { tab = "bag" });
        }
    }
}
