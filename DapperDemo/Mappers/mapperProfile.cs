using AutoMapper;
using DapperDemo.Models;
using DapperDemo.Models.DTOs;

namespace DapperDemo.Mappers
{
    public class mapperProfile : Profile
    {
        public mapperProfile()
        {
            //read mappers

            CreateMap<Company, CompanyDTO>();

            CreateMap<Employee,EmployeeDTO>();

            // write mappers

            CreateMap<CompanyRequestDTO, Company>();

            CreateMap<EmployeeRequestDTO,Employee>();
        }
    }
}
