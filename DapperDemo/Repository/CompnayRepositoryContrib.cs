using AutoMapper;
using Dapper.Contrib.Extensions;
using DapperDemo.Models;
using DapperDemo.Models.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperDemo.Repository;

public class CompnayRepositoryContrib : ICompanyRepository
{
    public Company Add(Company company)
    {
        throw new NotImplementedException();
    }

    public Company Find(int id)
    {
        throw new NotImplementedException();
    }

    public List<Company> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Company Update(Company company)
    {
        throw new NotImplementedException();
    }
}

