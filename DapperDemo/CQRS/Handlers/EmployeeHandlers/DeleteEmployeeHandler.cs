using DapperDemo.CQRS.Commands;
using DapperDemo.Repository;
using MediatR;

namespace DapperDemo.CQRS.Handlers.EmployeeHandlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepositoryDTO _repository;
        public DeleteEmployeeHandler(IEmployeeRepositoryDTO repository)
        {
            _repository = repository;
            
        }
        public Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            _repository.Remove(request.Id);
            return Task.CompletedTask;
        }
    }
}
