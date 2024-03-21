using DapperDemo.CQRS.Commands;
using DapperDemo.Models.DTOs;
using DapperDemo.Repository;
using MediatR;

namespace DapperDemo.CQRS.Handlers.EmployeeHandlers;

public class AddEmployeeHandler : IRequestHandler<RequestEmployeeCommand, EmployeeDTO>
{
    private readonly IEmployeeRepositoryDTO _repository;
    public AddEmployeeHandler(IEmployeeRepositoryDTO repository)
    {
        _repository = repository;
        
    }
    public Task<EmployeeDTO> Handle(RequestEmployeeCommand request, CancellationToken cancellationToken)
    {
        EmployeeDTO employeeDTO = _repository.Add(request.EmployeeRequest);
        return Task.FromResult(employeeDTO);
    }
}

