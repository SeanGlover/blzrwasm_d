using blzrwasm_d.Shared;
using Microsoft.EntityFrameworkCore;

namespace blzrwasm_d.Server.Models
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(int departmentId);
    }
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;
        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Department> GetDepartment(int departmentId)
        {
            var department = appDbContext.Departments == null ? new Department() : await appDbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
            return department ?? new Department();
        }
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            if (appDbContext.Departments == null) { return new List<Department>(); }
            else { return await appDbContext.Departments.ToListAsync(); }
        }
    }
}
