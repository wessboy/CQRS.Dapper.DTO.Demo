using DapperDemo.Models;
using DapperDemo.Models.DTOs;

namespace DapperDemo.Repository
{
    public interface ICompanyRepositoryDTO
    {
        CompanyDTO Find(int id);
        IEnumerable<CompanyDTO> GetAll();
        CompanyDTO Add(CompanyRequestDTO companyDTO);
        CompanyDTO Update(CompanyRequestDTO companyDTO);
        void Remove(int id);
    }
}
