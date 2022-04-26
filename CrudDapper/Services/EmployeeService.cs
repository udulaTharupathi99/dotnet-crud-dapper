using CrudDapper.Data;
using CrudDapper.Model;
using Dapper;
using System.Data;

namespace CrudDapper.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DapperContext _context;

        public EmployeeService(DapperContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var query = "SELECT * FROM Employee";
            using (var connection = _context.CreateConnection())
            {
                var employeeList = await connection.QueryAsync<Employee>(query);
                return employeeList.ToList();
            }
        }


        public async Task<Employee> GetEmployeeById(int id)
        {
            var query = "SELECT * FROM Employee WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Employee>(query, new { id });
                return company;
            }
        }


        public async Task DeleteEmployee(int id)
        {
            var query = "DELETE FROM Employee WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }


        public async Task<Employee> CreateEpluyee(Employee employee)
        {
            var query = "INSERT INTO Employee (Name, Age) VALUES (@Name, @Age)" +"SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("Age", employee.Age, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdCompany = new Employee
                {
                    Id = id,
                    Name = employee.Name,
                    Age = employee.Age,
                    
                };
                return createdCompany;
            }
        }


        public async Task UpdateEpluyee(int id, Employee employee)
        {
            var query = "UPDATE Employee SET Name = @Name, Age = @Age WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("Age", employee.Age, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
