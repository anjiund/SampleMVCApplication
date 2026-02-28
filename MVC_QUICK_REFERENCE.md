# Quick Reference: MVC Interaction Patterns

## Pattern 1: Display Data (Read Operation)

```
???????????????????????????????????????????????
? Controller?
???????????????????????????????????????????????
? public ActionResult Index()      ?
? {  ?
?  var data = _repository.GetAll();        ?
? return View(data);  ?????????????????   ?
? }      ?   ?
????????????????????????????????????????????????
     ?
       Passes Model     ?
    ?
????????????????????????????????????????????????
? View            ?
????????????????????????????????????????????????
? @model List<Product>               ?
? ?
? @foreach(var item in Model)          ?
? {            ?
?     <p>@item.Name</p>       ?
? }     ?
????????????????????????????????????????????????
```

---

## Pattern 2: Create Data (POST Operation)

```
??????????????? STEP 1: GET (Display Form) ???????????????
?     ?
?  Controller View           ?
?  ????????????????       ??????????????????            ?
?  ? [HttpGet]    ????????? Empty Form     ?            ?
?  ? Create()     ?       ?    ?       ?
?  ? {      ?    ? Name: [ ]      ?          ?
?  ?   return     ? ? Price: [ ]     ?            ?
??   View();    ?       ? [Submit]       ?   ?
?  ? }            ?   ??????????????????            ?
?  ????????????????               ?
???????????????????????????????????????????????????????????

??????????????? STEP 2: POST (Submit Form) ???????????????
?          ?
?  View                  Controller            ?
?  ??????????????????   ????????????????????????????  ?
?  ? User fills:    ?   ? [HttpPost]   ?    ?
?  ? Name: Laptop   ????? Create(Product model)  ?    ?
?  ? Price: 999     ?   ? {            ?    ?
?  ? [Submit]       ?   ?   if (ModelState.IsValid)?    ?
?  ??????????????????   ?   {      ?    ?
?          ?     _repo.Add(model);    ? ?
?       ?     return Redirect...;  ?    ?
?   ?   }           ?    ?
?    ?   return View(model);    ?    ?
?       ? }    ?    ?
?        ????????????????????????????    ?
???????????????????????????????????????????????????????????
```

---

## Pattern 3: Validation Flow

```
   Form Submitted
  ?
    ?
   ????????????????????
   ?  Model Binding   ?
   ?  (Automatic)     ?
   ????????????????????
            ?
            ?
   ????????????????????
   ?   Validation  ?
   ? (Data Attributes)?
   ????????????????????
  ?        ?
    Valid?    Invalid
        ?        ?
  ?        ?
   ??????????? ????????????????
   ? Process ? ? Return View  ?
   ? & Save  ? ? with Errors  ?
   ??????????? ????????????????
        ?
        ?
   ???????????
   ? Redirect?
   ???????????
```

---

## Data Annotations Reference

```csharp
public class Product
{
    // Required field
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    // String length validation
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "Name must be 3-100 characters")]
    public string Description { get; set; }

    // Numeric range
[Range(0.01, 9999.99, 
    ErrorMessage = "Price must be between $0.01 and $9999.99")]
  public decimal Price { get; set; }

    // Email validation
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    // URL validation
  [Url(ErrorMessage = "Invalid URL")]
    public string Website { get; set; }

    // Regular expression
 [RegularExpression(@"^\d{5}$", 
        ErrorMessage = "ZIP must be 5 digits")]
    public string ZipCode { get; set; }

    // Custom display name
    [Display(Name = "Product Name")]
    public string ProductName { get; set; }

    // Data type hint
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.MultilineText)]
    public string LongDescription { get; set; }

    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
}
```

---

## HTML Helper Reference

### Display Helpers
```razor
@Html.DisplayFor(m => m.Name)           @* Display value *@
@Html.DisplayNameFor(m => m.Name)       @* Display label *@
@Html.Display("PropertyName")           @* Display by name *@
```

### Input Helpers
```razor
@Html.TextBoxFor(m => m.Name)         @* <input type="text"> *@
@Html.TextAreaFor(m => m.Description)   @* <textarea> *@
@Html.PasswordFor(m => m.Password)      @* <input type="password"> *@
@Html.HiddenFor(m => m.Id)     @* <input type="hidden"> *@
@Html.CheckBoxFor(m => m.IsActive)      @* <input type="checkbox"> *@
@Html.RadioButtonFor(m => m.Gender, "M")@* <input type="radio"> *@
@Html.DropDownListFor(m => m.Category,  @* <select> *@
    new SelectList(categories))
```

### Label & Validation Helpers
```razor
@Html.LabelFor(m => m.Name)       @* <label> *@
@Html.ValidationMessageFor(m => m.Name) @* Field error *@
@Html.ValidationSummary()  @* All errors *@
```

### Link Helpers
```razor
@* Generate link: <a href="/Home/Index">Click</a> *@
@Html.ActionLink("Click", "Index", "Home")

@* With route values *@
@Html.ActionLink("View", "Details", new { id = 5 })

@* With HTML attributes *@
@Html.ActionLink("Click", "Index", null, 
    new { @class = "btn btn-primary" })
```

### Form Helpers
```razor
@* <form action="/Product/Create" method="post"> *@
@using (Html.BeginForm("Create", "Product", FormMethod.Post))
{
    @Html.AntiForgeryToken()
    @* form fields *@
}
```

---

## Common Controller Actions

```csharp
// Return view with model
return View(model);

// Return view by name
return View("ViewName", model);

// Redirect to action
return RedirectToAction("Index");

// Redirect to action with parameters
return RedirectToAction("Details", new { id = 5 });

// Redirect to different controller
return RedirectToAction("Index", "Home");

// Return JSON (for AJAX)
return Json(data, JsonRequestBehavior.AllowGet);

// Return file download
return File(bytes, "application/pdf", "filename.pdf");

// Return 404
return HttpNotFound();

// Return custom status code
return new HttpStatusCodeResult(403);

// Return content
return Content("Plain text");

// Redirect to URL
return Redirect("https://example.com");
```

---

## ModelState Operations

```csharp
// Check if valid
if (ModelState.IsValid) { }

// Add error
ModelState.AddModelError("PropertyName", "Error message");
ModelState.AddModelError("", "General error");

// Clear errors
ModelState.Clear();

// Get errors
var errors = ModelState.Values
    .SelectMany(v => v.Errors)
    .Select(e => e.ErrorMessage);

// Check specific property
if (ModelState["PropertyName"].Errors.Count > 0) { }
```

---

## ViewBag vs ViewData vs TempData

| Feature | ViewBag | ViewData | TempData |
|---------|---------|----------|----------|
| **Type** | `dynamic` | `ViewDataDictionary` | `TempDataDictionary` |
| **Syntax** | `ViewBag.Message` | `ViewData["Message"]` | `TempData["Message"]` |
| **Type Safety** | ? No | ? No | ? No |
| **IntelliSense** | ? No | ? No | ? No |
| **Scope** | Current request | Current request | Current + Next request |
| **Use Case** | Simple data | Dictionary style | Data after redirect |

```csharp
// ViewBag (most common for simple data)
ViewBag.Message = "Hello";
ViewBag.Count = 10;

// ViewData (dictionary style)
ViewData["Message"] = "Hello";
ViewData["Count"] = 10;

// TempData (survives redirect)
TempData["SuccessMessage"] = "Saved!";
return RedirectToAction("Index");  // Message available in Index
```

---

## Routing Patterns

```csharp
// Default route in RouteConfig.cs
routes.MapRoute(
    name: "Default",
    url: "{controller}/{action}/{id}",
    defaults: new { 
        controller = "Home", 
    action = "Index", 
   id = UrlParameter.Optional 
    }
);

// URL Examples:
// /        ? Home/Index
// /Home     ? Home/Index
// /Home/About          ? Home/About
// /Product/Details/5   ? Product/Details(id=5)
```

---

## Common Conventions

### File Locations
```
Controllers/
    HomeController.cs       ? Actions: Index, About, Contact
    ProductController.cs    ? Actions: Index, Create, Edit, Delete

Models/
    Product.cs           ? Domain model
 ProductRepository.cs    ? Data access
  ProductViewModel.cs     ? View-specific model

Views/
  Home/
        Index.cshtml        ? HomeController.Index()
        About.cshtml        ? HomeController.About()
    Product/
   Index.cshtml        ? ProductController.Index()
      Create.cshtml    ? ProductController.Create()
    Shared/
   _Layout.cshtml      ? Master layout
        Error.cshtml        ? Error page
    _ViewStart.cshtml       ? Sets default layout
```

### Naming Conventions
```
Controller:     ProductController (must end with "Controller")
Action:     public ActionResult Index()
View:Index.cshtml (matches action name)
Model:Product.cs (singular)
Collection:     List<Product> products (plural)
Repository:     ProductRepository (entity + "Repository")
```

---

## HTTP Verbs and Actions

```csharp
// GET - Display form/data
[HttpGet]
public ActionResult Create() { }

// POST - Submit form
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create(Product model) { }

// You can have same action name with different verbs
[HttpGet]
public ActionResult Edit(int id) { }  // Display form

[HttpPost]
public ActionResult Edit(Product model) { }  // Save changes

// Other verbs (for Web API)
[HttpPut]
[HttpDelete]
[HttpPatch]
```

---

## Security Attributes

```csharp
// Prevent CSRF attacks (always use on POST actions)
[ValidateAntiForgeryToken]
public ActionResult Create(Product model) { }

// Require authentication
[Authorize]
public ActionResult SecureAction() { }

// Require specific role
[Authorize(Roles = "Admin")]
public ActionResult AdminOnly() { }

// Allow anonymous (when controller has [Authorize])
[AllowAnonymous]
public ActionResult Login() { }

// Allow only specific HTTP methods
[HttpPost, ValidateAntiForgeryToken]
public ActionResult Submit() { }
```

---

## Common Patterns Summary

### Pattern: List Items
```
Controller ? Repository.GetAll() ? return View(list)
View ? @model List<T> ? @foreach(var item in Model)
```

### Pattern: View Details
```
URL: /Product/Details/5
Controller ? Repository.GetById(5) ? return View(item)
View ? @model Product ? @Model.Name
```

### Pattern: Create Item
```
GET  ? return View(new Product())
POST ? Validate ? Save ? RedirectToAction("Index")
```

### Pattern: Edit Item
```
GET  ? GetById ? return View(item)
POST ? Validate ? Update ? RedirectToAction("Index")
```

### Pattern: Delete Item
```
GET  ? GetById ? return View(item)  [confirmation]
POST ? Delete ? RedirectToAction("Index")
```

---

## Your Project's Examples

| URL | Controller | Action | View | Description |
|-----|------------|--------|------|-------------|
| `/Home/About` | Home | About() | About.cshtml | ViewBag example |
| `/Home/AboutWithModel` | Home | AboutWithModel() | AboutWithModel.cshtml | Strongly-typed |
| `/Home/ContactForm` | Home | ContactForm() | ContactForm.cshtml | Form with validation |
| `/Product/Index` | Product | Index() | Index.cshtml | List products |
| `/Product/Details/5` | Product | Details(5) | Details.cshtml | View product |
| `/Product/Create` | Product | Create() | Create.cshtml | Create product |
| `/Product/Edit/5` | Product | Edit(5) | Edit.cshtml | Edit product |

---

This quick reference complements the detailed MVC_INTERACTION_GUIDE.md file!
