using CompanyApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Models.Repos
{
    public class EmployeeRepo : ICompanyRepo<Employee>
    {
        private readonly Company_DbContext dbContext;

        public EmployeeRepo(Company_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Employee entity)
        {
            dbContext.Employees.Add(entity);
            commit();
        }

        public void Delete(int id)
        {
            var employee = GetById(id);

            DeleteOldImage(employee.EmployeeImageUrl);

            dbContext.Employees.Remove(employee);
            commit();
        }

        public IList<Employee> GetAll()
        {
            var allEmployees = dbContext.Employees.Include(d => d.Department)
                .OrderBy(Ename => Ename.EmployeeName).ToList();

            return allEmployees;
        }

        public Employee GetById(int id)
        {
            var employee = dbContext.Employees.Include(d => d.Department)
                .SingleOrDefault(e => e.EmployeeId == id);

            return employee;
        }

        public void Update(Employee entity)
        {
            dbContext.Employees.Update(entity);
            commit();
        }

        public static void DeleteOldImage(string? OldImageUrl)
        {
            if (OldImageUrl != null && OldImageUrl != "DefaultImage.jpg")
            {
                var OldImage = Path.Combine(@"wwwroot/", "Images", OldImageUrl);
                System.IO.File.Delete(OldImage);
            }
        }

        private void commit()
        {
            dbContext.SaveChanges();
        }

        public Employee? CheckForExistingEntity(Employee entity)
        {
            var employee = dbContext.Employees.Include(d => d.Department)
                .SingleOrDefault(e => e.NationalId == entity.NationalId);

            return employee;
        }

        public IList<Employee> Search(string term)
        {
            var result = dbContext.Employees.Include(d => d.Department)
                .Where(e => e.EmployeeName.Contains(term)
                || e.NationalId.Contains(term)
                || e.Department.DepartmentName.Contains(term)
                || e.Department.DepartmentAbbreviation.Contains(term)
                ).ToList();
            return result;
        }
    }
}
