using Dapper;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace DapperDemo.Repository
{
    public class BonusRepository : IBonusRepository
    {
        private readonly IDbConnection db;
        public BonusRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public void AddTestCompanyWithEmployees(Company objComp)
        {
            var sql = "INSERT INTO Companies (Name,Address,City,State,PostalCode) VALUES(@Name,@Address,@City,@State,@PostalCode);"
                   + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var idComp = db.Query<int>(sql,objComp).Single();
            objComp.CompanyId = idComp;

            //foreach(var employee in objComp.Employees) 
            //{
            //    employee.CompanyId = objComp.CompanyId;

            //    var sql1 = "INSERT INTO Employees (Name,Title,Email,Phone,CompanyId) VALUES(@Name,@Title,@Email,@Phone,@CompanyId);"
            //                + "SELECT CAST(SCOPE_IDENTITY() as int);";

            //    var idEmp = db.Query<int>(sql1, employee).Single();
            //    employee.EmployeeId = idEmp;


            //}

            objComp.Employees.Select(c => { c.CompanyId = idComp;return c; }).ToList();

               var sqlEmp = "INSERT INTO Employees (Name,Title,Email,Phone,CompanyId) VALUES(@Name,@Title,@Email,@Phone,@CompanyId);"
                            + "SELECT CAST(SCOPE_IDENTITY() as int);";

            db.Execute(sqlEmp,objComp.Employees);
        }



        public List<Company> GetAllCompaniesWithEmployees()
        {
            var sql = "SELECT C.*,E.* FROM Employees AS E INNER JOIN Companies AS C ON E.CompanyId = C.CompanyId";
        
            var companyDic = new Dictionary<int, Company>();
            
            var company = db.Query<Company,Employee,Company>(sql,(c,e) =>
            {
                if(!companyDic.TryGetValue(c.CompanyId,out var currentCompany)) 
                {
                    currentCompany = c;
                    companyDic.Add(currentCompany.CompanyId, currentCompany);
                }

                currentCompany.Employees.Add(e);
                return currentCompany;
            },splitOn:"EmployeeId");

            return company.Distinct().ToList();
        }

        public Company GetCompanyWithEmployees(int id)
        {
            var parameter = new
            {
                CompanyId = id
            };

            var sql = "SELECT * FROM Companies WHERE CompanyId = @CompanyId;"
                      + "SELECT * FROM Employees WHERE CompanyId = @CompanyId";

            Company company;

            using(var lists = db.QueryMultiple(sql, parameter)) 
            { 
               company = lists.Read<Company>().ToList().FirstOrDefault();
                company.Employees = lists.Read<Employee>().ToList();
            }

            return company;
        }

        public List<Employee> GetEmployeeWithCompany(int id)
        {
            var sql = "SELECT E.*,C.* From Employees As E INNER JOIN Companies C ON E.CompanyId = C.CompanyId";

            if(id > 0)
            {
                sql += " WHERE E.CompanyId = @Id";
            }

            var employee = db.Query<Employee, Company, Employee>(sql, (e, c) =>
            {
                e.Company = c;
                return e;
            },new {id},splitOn:"CompanyId");

            return employee.ToList();
        }

        public void RemoveRange(int[] companyId)
        {
            db.Query("DELETE FROM Companies WHERE CompanyId IN @companyId", new {companyId});
        }

        public List<Company> FilterCompanyByName(string name)
        {
            return db.Query<Company>("SELECT * FROM Companies WHERE Name like '%' + @name + '%' ", new {name}).ToList();
        }

        public void AddTestCompanyWithEmployeesWithTransaction(Company objComp)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {

                    var sql = "INSERT INTO Companies (Name,Address,City,State,PostalCode) VALUES(@Name,@Address,@City,@State,@PostalCode);"
                                + "SELECT CAST(SCOPE_IDENTITY() as int);";
                    var idComp = db.Query<int>(sql, objComp).Single();
                    objComp.CompanyId = idComp;



                    objComp.Employees.Select(c => { c.CompanyId = idComp; return c; }).ToList();

                    var sqlEmp = "INSERT INTO Employees (Name,Title,Email,Phone,CompanyId) VALUES(@Name,@Title,@Email,@Phone,@CompanyId);"
                                 + "SELECT CAST(SCOPE_IDENTITY() as int);";

                    db.Execute(sqlEmp, objComp.Employees);

                    transaction.Complete();

                }
                catch (Exception ex)
                {

                    
                }


            }

        }
    }
}
