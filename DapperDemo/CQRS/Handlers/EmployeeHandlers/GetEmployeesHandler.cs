using DapperDemo.CQRS.Queries;
using DapperDemo.Models.DTOs;
using DapperDemo.Repository;
using MediatR;

namespace DapperDemo.CQRS.Handlers
{
    public class GetEmployeesHandler : IRequestHandler<EmployeesDTOQuery, IEnumerable<EmployeeDTO>>
    {
        private readonly IEmployeeRepositoryDTO _repository;

        public GetEmployeesHandler(IEmployeeRepositoryDTO repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<EmployeeDTO>> Handle(EmployeesDTOQuery request, CancellationToken cancellationToken)
        {
            return  _repository.GetAll();
        }
    }
}
