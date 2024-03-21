using MediatR;

namespace DapperDemo.CQRS.Commands
{
    public class DeleteEmployeeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
