
using blzrwasm_d.Shared;
using Microsoft.EntityFrameworkCore;

namespace blzrwasm_d.Server.Models
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Search(string name, Gender? gender);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int employeeId);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            if (appDbContext.Employees == null) { return new List<Employee>(); }
            else { return await appDbContext.Employees.ToListAsync(); }
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            if (employee.Department != null)
            {
                appDbContext.Entry(employee.Department).State = EntityState.Unchanged;
            }
            if (appDbContext.Employees == null) { return new Employee(); }
            else
            {
                var newEmployee = await appDbContext.Employees.AddAsync(employee);
                await appDbContext.SaveChangesAsync();
                return newEmployee.Entity;
            }
        }
        public async Task DeleteEmployee(int employeeId)
        {
            if (appDbContext.Employees == null) { }
            else
            {
                var result = await appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
                if (result != null)
                {
                    appDbContext.Employees.Remove(result);
                    await appDbContext.SaveChangesAsync();
                }
            }
        }
        public async Task<Employee> GetEmployee(int employeeId)
        {
            if (appDbContext.Employees == null) { return new Employee(); }
            else
            {
                var newEmployee = await appDbContext.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmployeeId == employeeId); 
                return newEmployee ?? new Employee();
            }
        }
        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            if (appDbContext.Employees == null) { return new Employee(); }
            else
            {
                var result = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);
                return result ?? new Employee();
            }
        }
        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee>? query = appDbContext.Employees;
            if (query == null) { return new List<Employee>(); }
            else
            if (!string.IsNullOrEmpty(name)) { query = query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name)); }
            if (gender != null) { query = query.Where(e => e.Gender == gender); }
            return await query.ToListAsync();
        }
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            if (appDbContext.Employees == null) { return new Employee(); }
            else
            {
                var result = await appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
                if (result != null)
                {
                    result.FirstName = employee.FirstName;
                    result.LastName = employee.LastName;
                    result.Email = employee.Email;
                    result.DateOfBirth = employee.DateOfBirth;
                    result.Gender = employee.Gender;
                    if (employee.DepartmentId != 0) { result.DepartmentId = employee.DepartmentId; }
                    else if (employee.Department != null) { result.DepartmentId = employee.Department.DepartmentId; }
                    result.PhotoPath = employee.PhotoPath;
                    await appDbContext.SaveChangesAsync();
                    return result;
                }
                return new Employee();
            }

        }
    }
}
