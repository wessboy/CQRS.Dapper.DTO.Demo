using DapperDemo.CQRS.Queries;
using DapperDemo.Models.DTOs;
using DapperDemo.Repository;
using MediatR;

namespace DapperDemo.CQRS.Handlers
{
    public class GetComanpiesHandler : IRequestHandler<CompaniesDTOQuery, IEnumerable<CompanyDTO>>
    {
        private readonly ICompanyRepositoryDTO _repository;

        public GetComanpiesHandler(ICompanyRepositoryDTO repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<CompanyDTO>> Handle(CompaniesDTOQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAll();
        }
    }
}
