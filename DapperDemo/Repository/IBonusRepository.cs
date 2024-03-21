using DapperDemo.Models;

namespace DapperDemo.Repository
{
    public interface IBonusRepository
    {
        public List<Employee> GetEmployeeWithCompany(int id);
        public Company GetCompanyWithEmployees(int id);
        public List<Company> GetAllCompaniesWithEmployees();

        void AddTestCompanyWithEmployees(Company objComp);
        void AddTestCompanyWithEmployeesWithTransaction(Company objComp);
        void RemoveRange(int[] companyId);
        List<Company> FilterCompanyByName(string name);
    
    }
}
