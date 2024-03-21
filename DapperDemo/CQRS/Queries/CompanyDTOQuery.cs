using DapperDemo.Models.DTOs;
using MediatR;

namespace DapperDemo.CQRS.Queries;

    public class CompanyDTOQuery : IRequest<CompanyDTO>
    {
       public int Id { get; set; }  
    }

