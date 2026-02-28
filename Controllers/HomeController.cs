using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleMVCApplication.Models;

namespace SampleMVCApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // EXAMPLE 1: Using ViewBag (your current implementation)
        // Simple but not type-safe
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // EXAMPLE 2: Using Strongly-Typed Model (RECOMMENDED)
        // Type-safe, IntelliSense support, compile-time checking
        public ActionResult AboutWithModel()
        {
            // Create and populate the model
            var model = new AboutViewModel
            {
                Title = "About Our Application",
                Message = "This is a sample MVC application demonstrating Model-View-Controller interaction.",
                CompanyName = "Sample Company Inc.",
                FoundedDate = new DateTime(2020, 1, 1),
                Features = new List<string>
                {
                    "Strongly-typed models for type safety",
                    "Separation of concerns with MVC pattern",
                    "Easy to test and maintain",
                    "Scalable architecture"
                }
            };

            // Pass the model to the view
            return View(model);
        }

        // EXAMPLE 3: GET request - Display empty form
        // Shows how Controller prepares View for user input
        [HttpGet]
        public ActionResult ContactForm()
        {
            // Return an empty model for the form
            var model = new ContactFormModel();
            return View(model);
        }

        // EXAMPLE 3: POST request - Handle form submission
        // Shows how data flows from View → Controller → Model
        [HttpPost]
        [ValidateAntiForgeryToken]  // Security: prevents CSRF attacks
        public ActionResult ContactForm(ContactFormModel model)
        {
            // Model Binding: ASP.NET automatically maps form data to model properties

            // Check if the model passes validation rules
            if (ModelState.IsValid)
            {
                // Set submission timestamp
                model.SubmittedDate = DateTime.Now;

                // In a real application, you would:
                // 1. Save to database
                // 2. Send email
                // 3. Call business logic layer

                // For demo: Store in TempData and redirect
                TempData["SuccessMessage"] = $"Thank you {model.Name}! Your message has been received.";
                TempData["SubmittedModel"] = model;

                return RedirectToAction("ContactSuccess");
            }

            // If validation fails, return the same view with error messages
            // ModelState contains validation errors
            return View(model);
        }

        // Display success page after form submission
        public ActionResult ContactSuccess()
        {
            var model = TempData["SubmittedModel"] as ContactFormModel;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}