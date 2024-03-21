using DapperDemo.CQRS.Queries;
using DapperDemo.Models.DTOs;
using DapperDemo.Repository;
using MediatR;
using NuGet.Protocol.Plugins;

namespace DapperDemo.CQRS.Handlers
{
    public class GetEmployeeHandler : IRequestHandler<EmployeeDTOQuery, EmployeeDTO>
    {
        private readonly IEmployeeRepositoryDTO _repository;
        public GetEmployeeHandler(IEmployeeRepositoryDTO repository)
        {
            _repository = repository;
        }
        public Task<EmployeeDTO> Handle(EmployeeDTOQuery request, CancellationToken cancellationToken)
        {
            EmployeeDTO employeeDTO = _repository.Find(request.Id);
            
            return Task.FromResult(employeeDTO);
        }
    }
}
