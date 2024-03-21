using MediatR;

namespace DapperDemo.CQRS.Commands
{
    public class DeleteCompanyCommand : IRequest
    {
        public int Id { get; set; }
    }
}
