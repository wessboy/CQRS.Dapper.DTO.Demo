using DapperDemo.CQRS.Queries;
using DapperDemo.Models.DTOs;
using DapperDemo.Repository;
using MediatR;

namespace DapperDemo.CQRS.Handlers.CompanyHandlers
{
    public class GetCompanyHandler : IRequestHandler<CompanyDTOQuery, CompanyDTO>
    {
        private readonly ICompanyRepositoryDTO _repository;
        public GetCompanyHandler(ICompanyRepositoryDTO repository)
        {
            _repository = repository;
        }
        public Task<CompanyDTO> Handle(CompanyDTOQuery request, CancellationToken cancellationToken)
        {
            CompanyDTO companyDTO = _repository.Find(request.Id);
            return Task.FromResult(companyDTO); 
        }
    }
}
