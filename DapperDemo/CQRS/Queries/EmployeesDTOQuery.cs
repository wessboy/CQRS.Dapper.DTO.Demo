using DapperDemo.Models.DTOs;
using MediatR;

namespace DapperDemo.CQRS.Queries;

    public class EmployeesDTOQuery : IRequest<IEnumerable<EmployeeDTO>>
    {
    }

