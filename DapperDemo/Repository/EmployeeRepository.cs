using AutoMapper;
using Dapper;
using DapperDemo.Models;
using DapperDemo.Models.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;


namespace DapperDemo.Repository;
public class EmployeeRepository : IEmployeeRepositoryDTO
{
    private IDbConnection db;
    private readonly IMapper _mapper;
    public EmployeeRepository(IConfiguration configuration,IMapper mapper)
    {
        db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        _mapper = mapper;
        
    }
    public EmployeeDTO Add(EmployeeRequestDTO employeeDTO)
    {
        var sql = "INSERT INTO Employees (Name,Title,Email,Phone,CompanyId) VALUES(@Name,@Title,@Email,@Phone,@CompanyId);"
                  + "SELECT CAST(SCOPE_IDENTITY() as int)";

        Employee employee = _mapper.Map<Employee>(employeeDTO);
         var id = db.Query<int>(sql,employee).Single();
        employee.EmployeeId = id;
        return _mapper.Map<EmployeeDTO>(employee);
    }

    public async Task<EmployeeDTO> AddAsync(EmployeeRequestDTO employeeDTO)
    {
        var sql = "INSERT INTO Employees (Name,Title,Email,Phone,CompanyId) VALUES(@Name,@Title,@Email,@Phone,@CompanyId);"
                  + "SELECT CAST(SCOPE_IDENTITY() as int)";
       
        Employee employee = _mapper.Map<Employee>(employeeDTO);   
        var id = await db.QueryAsync<int>(sql, employee);

        employee.EmployeeId = id.Single();
        return _mapper.Map<EmployeeDTO>(employee);
    }

    public EmployeeDTO Find(int id)
    {
        var sql = "SELECT * FROM Employees WHERE EmployeeId = @Id";
          Employee employee =  db.Query<Employee>(sql, new {@Id = id}).Single();

        return _mapper.Map<EmployeeDTO>(employee);
    }

    public IEnumerable<EmployeeDTO> GetAll()
    {
        var sql = "SELECT * FROM Employees";
        List<Employee> employee =  db.Query<Employee>(sql).ToList();

        return _mapper.Map<List<EmployeeDTO>>(employee);    
        
    }

    public void Remove(int id)
    {
        var sql = "DELETE FROM Employees WHERE EmployeeId = @Id";
        db.Execute(sql,new {@Id = id});
    }

    public EmployeeDTO Update(EmployeeRequestDTO employeeDTO)
    {
        var sql = "UPDATE Employees SET Name = @Name,Title = @Title,Email = @Email,Phone = @Phone,CompanyId = @CompanyId WHERE EmployeeId = @EmployeeId;";

        Employee employee = _mapper.Map<Employee>(employeeDTO);
         Employee employeeUpdated =  db.Query<Employee> (sql, employee).Single();

        return _mapper.Map<EmployeeDTO>(employeeUpdated);
    }
}

