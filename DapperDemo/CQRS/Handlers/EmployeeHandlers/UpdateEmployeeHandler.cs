using DapperDemo.CQRS.Commands;
using DapperDemo.Models.DTOs;
using DapperDemo.Repository;
using MediatR;

namespace DapperDemo.CQRS.Handlers.EmployeeHandlers;

public class UpdateEmployeeHandler : IRequestHandler<RequestEmployeeCommand, EmployeeDTO>
{
    private readonly IEmployeeRepositoryDTO _repository;
    public UpdateEmployeeHandler(IEmployeeRepositoryDTO repository)
    {
        _repository = repository;
    }
    public Task<EmployeeDTO> Handle(RequestEmployeeCommand request, CancellationToken cancellationToken)
    {
        EmployeeDTO employeeDTO = _repository.Update(request.EmployeeRequest);
        return Task.FromResult(employeeDTO);
    }
}

