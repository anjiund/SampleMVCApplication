using System.Web.Mvc;
using SampleMVCApplication.Models;

namespace SampleMVCApplication.Controllers
{
    /// <summary>
    /// ProductController demonstrates complete MVC interaction:
    /// - Controller communicates with Model (ProductRepository)
    /// - Model handles data and business logic
    /// - Controller passes data to View for display
    /// - View sends user input back to Controller
    /// </summary>
    public class ProductController : Controller
    {
        // Repository instance - in real apps, use Dependency Injection
        private ProductRepository _repository = new ProductRepository();

        // ============================================================
        // READ OPERATIONS (GET requests)
// ============================================================

  /// <summary>
/// Display list of all products
        /// Flow: Controller ? Model (get data) ? View (display)
        /// </summary>
        [HttpGet]
        public ActionResult Index(string category = null)
        {
         // Step 1: Controller calls Model to get data
        var products = string.IsNullOrEmpty(category) 
      ? _repository.GetAll() 
  : _repository.GetByCategory(category);

       // Step 2: Controller passes data to View using ViewBag for categories
            ViewBag.Categories = _repository.GetCategories();
ViewBag.SelectedCategory = category;

          // Step 3: Controller returns View with strongly-typed model
 return View(products);
        }

        /// <summary>
        /// Display details of a single product
      /// Flow: User clicks link ? Controller gets ID ? Model retrieves data ? View displays
        /// </summary>
   [HttpGet]
    public ActionResult Details(int id)
        {
            // Controller asks Model for specific product
        var product = _repository.GetById(id);

 if (product == null)
   {
            // Handle not found case
      return HttpNotFound($"Product with ID {id} not found.");
  }

      // Pass product to view
         return View(product);
        }

        // ============================================================
        // CREATE OPERATIONS (GET + POST)
        // ============================================================

        /// <summary>
        /// Display empty form for creating new product
        /// Flow: Controller ? View (empty form)
        /// </summary>
     [HttpGet]
        public ActionResult Create()
        {
  // Create empty model
     var model = new Product();
            
        // Provide categories for dropdown
       ViewBag.Categories = _repository.GetCategories();

            return View(model);
        }

/// <summary>
        /// Handle form submission for creating new product
        /// Flow: View (form data) ? Controller ? Model (save) ? Redirect
        /// </summary>
    [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
 // Step 1: Validate the model
          if (ModelState.IsValid)
 {
        // Step 2: Controller calls Model to save data
        _repository.Add(product);

             // Step 3: Set success message
 TempData["SuccessMessage"] = $"Product '{product.Name}' created successfully!";

       // Step 4: Redirect to list (PRG pattern: Post-Redirect-Get)
         return RedirectToAction("Index");
}

            // If validation fails, return form with errors
      ViewBag.Categories = _repository.GetCategories();
  return View(product);
        }

 // ============================================================
        // UPDATE OPERATIONS (GET + POST)
        // ============================================================

  /// <summary>
        /// Display form pre-filled with existing product data
      /// Flow: Controller ? Model (get data) ? View (populated form)
      /// </summary>
    [HttpGet]
   public ActionResult Edit(int id)
{
            // Get existing product from model
        var product = _repository.GetById(id);

    if (product == null)
            {
           return HttpNotFound($"Product with ID {id} not found.");
            }

     ViewBag.Categories = _repository.GetCategories();
            return View(product);
     }

        /// <summary>
   /// Handle form submission for updating product
        /// Flow: View (modified data) ? Controller ? Model (update) ? Redirect
        /// </summary>
        [HttpPost]
  [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
     {
   if (ModelState.IsValid)
   {
                // Update in repository
     bool success = _repository.Update(product);

         if (success)
      {
TempData["SuccessMessage"] = $"Product '{product.Name}' updated successfully!";
             return RedirectToAction("Index");
              }
      else
  {
         ModelState.AddModelError("", "Product not found.");
         }
            }

       ViewBag.Categories = _repository.GetCategories();
            return View(product);
    }

      // ============================================================
        // DELETE OPERATIONS (GET + POST)
        // ============================================================

        /// <summary>
        /// Display confirmation page before deleting
        /// Flow: Controller ? Model (get data) ? View (confirmation)
     /// </summary>
     [HttpGet]
    public ActionResult Delete(int id)
        {
            var product = _repository.GetById(id);

 if (product == null)
            {
        return HttpNotFound($"Product with ID {id} not found.");
         }

            return View(product);
  }

        /// <summary>
        /// Handle delete confirmation
        /// Flow: View (confirmation) ? Controller ? Model (delete) ? Redirect
  /// </summary>
    [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _repository.GetById(id);
            
      if (product != null)
  {
          _repository.Delete(id);
             TempData["SuccessMessage"] = $"Product '{product.Name}' deleted successfully!";
      }

            return RedirectToAction("Index");
 }
    }
}
