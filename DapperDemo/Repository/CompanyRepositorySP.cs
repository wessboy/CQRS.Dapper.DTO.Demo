using Dapper;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

namespace DapperDemo.Repository;

public class CompanyRepositorySP : ICompanyRepository
{
    private readonly IDbConnection db;
    public CompanyRepositorySP(IConfiguration configuration)
    {
        db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        
    }
    public Company Add(Company company)
    {
        var parameteres = new DynamicParameters();
        parameteres.Add("@CompanyId", 0,DbType.Int32, direction: ParameterDirection.Output);
        parameteres.Add("@Name", company.Name, DbType.String);
        parameteres.Add("@Address",company.Address, DbType.String);
        parameteres.Add("@City",company.City,DbType.String);
        parameteres.Add("@State",company.State,DbType.String);
        parameteres.Add("@PostalCode",company.PostalCode,DbType.String);

        db.Execute("usp_AddCompany",parameteres,commandType: CommandType.StoredProcedure);
        company.CompanyId = parameteres.Get<int>("CompanyId");

        return company;


    }

    public Company Find(int id)
    {
        return db.Query<Company>("usp_GetCompany", new { CompanyId = id }, commandType:CommandType.StoredProcedure).Single();
    }
    
    public List<Company> GetAll()
    {
        return db.Query<Company>("usp_GetALLCompany", commandType: CommandType.StoredProcedure).ToList();
    }

    public void Remove(int id)
    {
        db.Execute("usp_RemoveCompany", new {CompanyId = id}, commandType:CommandType.StoredProcedure);
    }

    public Company Update(Company company)
    {
        var parameteres = new DynamicParameters();
        parameteres.Add("@CompanyId",company.CompanyId,DbType.Int32);
        parameteres.Add("@Name", company.Name, DbType.String);
        parameteres.Add("@Address", company.Address, DbType.String);
        parameteres.Add("@City", company.City, DbType.String);
        parameteres.Add("@State", company.State, DbType.String);
        parameteres.Add("@PostalCode", company.PostalCode, DbType.String);

        db.Execute("usp_UpdateCompany", parameteres, commandType: CommandType.StoredProcedure);
        

        return company;
    }
}

