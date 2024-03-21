using DapperDemo.Models.DTOs;
using MediatR;

namespace DapperDemo.CQRS.Commands;

    public class RequestEmployeeCommand : IRequest<EmployeeDTO>
    {
       public EmployeeRequestDTO EmployeeRequest { get; set; }
    }

