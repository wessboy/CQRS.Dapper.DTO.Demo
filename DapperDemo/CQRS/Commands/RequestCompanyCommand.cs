using DapperDemo.Models.DTOs;
using MediatR;

namespace DapperDemo.CQRS.Commands
{
    public class RequestCompanyCommand : IRequest<CompanyDTO>
    {
        public CompanyRequestDTO CompanyRequest { get; set; }
    }
}
