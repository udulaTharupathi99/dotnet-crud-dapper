using CrudDapper.Model;

namespace CrudDapper.Services
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task<Employee> GetEmployeeById(int id);
        public Task DeleteEmployee(int id);
        public Task<Employee> CreateEpluyee(Employee employee);
        public Task UpdateEpluyee(int id, Employee employee);

    }
}
