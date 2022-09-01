using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Student> products = StudentDb.GetStudents(context);
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student p)
        {
            if (ModelState.IsValid)
            {
                // Add to database
                StudentDb.Add(p, context);

                // Show success message on page
                ViewData["Message"] = $"{p.Name} was made and added successfully!";

                return View();
            }

            //Show web page with errors
            return View(p);
        }

        public IActionResult Edit(int id)
        {
            //get the product by id
            Student p = StudentDb.GetStudent(context, id);

            if (p == null)
            {
                return NotFound();
            }

            //show it on web page
            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Student p)
        {
            if (p != null)
            {
                StudentDb.Update(context, p);
                TempData["Message"] = "Product Updated!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(p);
        }

        public IActionResult Delete(int id)
        {
            Student p = StudentDb.GetStudent(context, id);

            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            //Get Product from database
            Student p = StudentDb.GetStudent(context, id);

            if (p != null)
            {
                TempData["Message"] = p.Name + " was deleted successfully";
                StudentDb.Delete(context, p);
            }

            TempData["Message"] = "This student was already deleted";
            return RedirectToAction("Index");
        }
    }
}
