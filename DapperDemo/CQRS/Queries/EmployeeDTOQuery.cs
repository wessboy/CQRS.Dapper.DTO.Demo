using DapperDemo.Models.DTOs;
using MediatR;

namespace DapperDemo.CQRS.Queries;

    public class EmployeeDTOQuery : IRequest<EmployeeDTO>
    {
          public int Id {  get; set; }  
    }

