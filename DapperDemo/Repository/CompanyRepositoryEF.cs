﻿using DapperDemo.Data;
using DapperDemo.Models;

namespace DapperDemo.Repository
{
    public class CompanyRepositoryEF : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepositoryEF(ApplicationDbContext context)
        {
            _context = context;
        }
        public Company Add(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges(); 
            return company;
        }

        public Company Find(int id)
        {
            return _context.Companies.FirstOrDefault( c => c.CompanyId == id);
        }

        public List<Company> GetAll()
        {
            return _context.Companies.ToList();
            
        }

        public void Remove(int id)
        {
            Company company = _context.Companies.FirstOrDefault(c => c.CompanyId == id);
            _context.Companies.Remove(company);
            _context.SaveChanges();
        }

        public Company Update(Company company)
        {
            _context.Companies.Update(company);
            _context.SaveChanges();
            return company;
        }
    }
}
