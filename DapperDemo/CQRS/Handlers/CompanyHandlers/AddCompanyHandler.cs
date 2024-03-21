using DapperDemo.CQRS.Commands;
using DapperDemo.Models.DTOs;
using DapperDemo.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DapperDemo.CQRS.Handlers.CompanyHandlers
{
    public class AddEmployeeHandler : IRequestHandler<RequestCompanyCommand, CompanyDTO>
    {
        private readonly ICompanyRepositoryDTO _repository;
        public AddEmployeeHandler(ICompanyRepositoryDTO repository)
        {
            _repository = repository;
            
        }
        public Task<CompanyDTO> Handle(RequestCompanyCommand request, CancellationToken cancellationToken)
        {
            CompanyDTO companyDTO = _repository.Add(request.CompanyRequest);

            return Task.FromResult(companyDTO);
        }
    }
}
