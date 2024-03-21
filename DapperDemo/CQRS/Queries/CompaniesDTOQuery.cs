using DapperDemo.Models.DTOs;
using MediatR;

namespace DapperDemo.CQRS.Queries
{
    public class CompaniesDTOQuery : IRequest<IEnumerable<CompanyDTO>>
    {
    }
}
