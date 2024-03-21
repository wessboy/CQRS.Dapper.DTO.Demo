using DapperDemo.CQRS.Commands;
using DapperDemo.Models.DTOs;
using DapperDemo.Repository;
using MediatR;

namespace DapperDemo.CQRS.Handlers.CompanyHandlers
{
    public class UpdateEmployeeHandler : IRequestHandler<RequestCompanyCommand, CompanyDTO>
    {
        private readonly ICompanyRepositoryDTO _repository;
        public UpdateEmployeeHandler(ICompanyRepositoryDTO repository)
        {
            _repository = repository;
        }
        public Task<CompanyDTO> Handle(RequestCompanyCommand request, CancellationToken cancellationToken)
        {
            CompanyDTO companyDTO = _repository.Update(request.CompanyRequest);
            
            return Task.FromResult(companyDTO);
        }
    }
}
