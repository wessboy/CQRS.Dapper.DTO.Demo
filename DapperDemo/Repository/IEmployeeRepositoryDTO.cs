using DapperDemo.Models;
using DapperDemo.Models.DTOs;

namespace DapperDemo.Repository;

    public interface IEmployeeRepositoryDTO
    {
    EmployeeDTO Find(int id);
    IEnumerable<EmployeeDTO> GetAll();
    EmployeeDTO Add(EmployeeRequestDTO employeeDTO);    
    Task<EmployeeDTO> AddAsync(EmployeeRequestDTO employeeDTO);
    EmployeeDTO Update(EmployeeRequestDTO employeeDTO);
     void  Remove(int id);
       
    }

