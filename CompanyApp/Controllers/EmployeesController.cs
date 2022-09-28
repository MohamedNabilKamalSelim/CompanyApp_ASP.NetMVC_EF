using CompanyApp.Models;
using CompanyApp.Models.Repos;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ICompanyRepo<Employee> employeeRepo;
        private readonly ICompanyRepo<Department> departmentRepo;

        public EmployeesController(ICompanyRepo<Employee> employeeRepo
            , ICompanyRepo<Department> departmentRepo)
        {
            this.employeeRepo = employeeRepo;
            this.departmentRepo = departmentRepo;
        }

        public IActionResult Index()
        {
            var employees = employeeRepo.GetAll();

            return View(employees);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = departmentRepo.GetAll();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = employeeRepo.CheckForExistingEntity(model);

                if (existingEmployee != null)
                {
                    ViewBag.Massege = "This Employee is already exist by id:" + existingEmployee.EmployeeId.ToString()
                        + " and National Id:" + existingEmployee.NationalId.ToString();

                    ViewBag.Departments = departmentRepo.GetAll();
                    return View();
                }

                model.EmployeeImageUrl = UploadImage() ?? "DefaultImage.jpg";

                employeeRepo.Add(model);

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = departmentRepo.GetAll();
            return View();
        }

        public IActionResult Edit(int id)
        {
            var model = employeeRepo.GetById(id);

            ViewBag.Departments = departmentRepo.GetAll();

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                model.EmployeeImageUrl = NewImageUrl(model);

                employeeRepo.Update(model);

                return RedirectToAction("Index");
            }
            ViewBag.Departments = departmentRepo.GetAll();

            return View("Create", model);
        }

        public IActionResult Delete(int id)
        {
            employeeRepo.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(string term)
        {
            var result = employeeRepo.Search(term);

            return View("Index", result);
        }

        private string NewImageUrl(Employee model)
        {
            string? oldImageUrl = model.EmployeeImageUrl;
            string? newImageUrl = UploadImage();

            if (newImageUrl != null)
            {
                EmployeeRepo.DeleteOldImage(oldImageUrl);
            }
            else
            {
                newImageUrl = oldImageUrl ?? "DefaultImage.jpg";
            }
            return newImageUrl;
        }

        private string? UploadImage()
        {
            // To get the file name="ImageFile" from the recieved form 
            var ImageFile = HttpContext.Request.Form.Files.SingleOrDefault(f => f.Name == "ImageFile");
            if (ImageFile != null)
            {
                var imageName = ImageFile.FileName;

                // To generate a unique name for the Image to save it by
                string uniqueImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageName);

                //To get path and save the image in it with a new image name
                var imagePath = Path.Combine(@"wwwroot/", "Images", uniqueImageName);
                ImageFile.CopyTo(new FileStream(imagePath, FileMode.Create));

                return uniqueImageName;
            }
            else
            {
                return null;
            }
        }

    }
}
