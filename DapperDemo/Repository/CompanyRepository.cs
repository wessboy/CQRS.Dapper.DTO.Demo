using AutoMapper;
using Dapper;
using DapperDemo.Models;
using DapperDemo.Models.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperDemo.Repository
{
    public class CompanyRepository : ICompanyRepositoryDTO
    {
        private readonly IDbConnection db;
        private readonly IMapper _mapper;

        public CompanyRepository(IConfiguration configuration,IMapper mapper)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _mapper = mapper;
            
        }
        public CompanyDTO Add(CompanyRequestDTO companyDTO)
        {
            var sql = "INSERT INTO Companies (Name,Address,City,State,PostalCode) VALUES(@Name,@Address,@City,@State,@PostalCode);"
                      + "SELECT CAST(SCOPE_IDENTITY() as int);";

            Company company = _mapper.Map<Company>(companyDTO);
            var id = db.Query<int>(sql,company).Single();

            company.CompanyId = id;
            
            return _mapper.Map<CompanyDTO>(company);
        }

        public CompanyDTO Find(int id)
        {
            var sql = "SELECT * FROM Companies WHERE CompanyId = @CompanyId";
            Company company = db.Query<Company>(sql,new {@CompanyId = id}).Single();

            return _mapper.Map<CompanyDTO>(company);

        }

        public IEnumerable<CompanyDTO> GetAll()
        {
            var sql =  "SELECT * FROM Companies";
            List<Company> companies = db.Query<Company>(sql).ToList();

            return _mapper.Map<List<CompanyDTO>>(companies);
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Companies WHERE CompanyId = @Id";
            db.Execute(sql, new {id});
        }

        public CompanyDTO Update(CompanyRequestDTO companyDTO)
        {
            var sql = "UPDATE Companies SET Name=@Name,Address=@Address,City=@City,State=@State,PostalCode=@PostalCode WHERE CompanyId = @CompanyId;";
            Company company = _mapper.Map<Company>(companyDTO);
            db.Execute(sql,company);
            return _mapper.Map<CompanyDTO>(company);
        }
    }
}
