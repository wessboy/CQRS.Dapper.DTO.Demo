using DapperDemo.CQRS.Commands;
using DapperDemo.Repository;
using MediatR;

namespace DapperDemo.CQRS.Handlers.CompanyHandlers
{
    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly ICompanyRepositoryDTO _repository;
        public DeleteCompanyHandler(ICompanyRepositoryDTO repository)
        {
            _repository = repository;
            
        }
        public Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            _repository.Remove(request.Id);

            return Task.CompletedTask;
        }
    }
}
