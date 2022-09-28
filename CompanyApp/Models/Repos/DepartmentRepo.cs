using CompanyApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Models.Repos
{
    public class DepartmentRepo : ICompanyRepo<Department>
    {
        private readonly Company_DbContext dbContext;

        public DepartmentRepo(Company_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Department entity)
        {
            dbContext.Departments.Add(entity);
            commit();
        }

        public Department? CheckForExistingEntity(Department entity)
        {
            var department = dbContext.Departments
                .SingleOrDefault(d => d.DepartmentName == entity.DepartmentName);
            
            return department;
        }

        public void Delete(int id)
        {
            var department = GetById(id);
            dbContext.Departments.Remove(department);
            commit();
        }

        public IList<Department> GetAll()
        {
            var allDepartments = dbContext.Departments.OrderBy(Dname => Dname.DepartmentName).ToList();
            return allDepartments;
        }

        public Department GetById(int id)
        {
            var department = dbContext.Departments.SingleOrDefault(d => d.DepartmentId == id);
            return department;
        }

        public IList<Department> Search(string term)
        {
            var result = dbContext.Departments
                .Where(d => d.DepartmentName.Contains(term)
                || d.DepartmentAbbreviation.Contains(term)
                ).ToList();
            return result;
        }

        public void Update(Department entity)
        {
            dbContext.Departments.Update(entity);
            commit();
        }

        private void commit()
        {
            dbContext.SaveChanges();
        }
    }
}
