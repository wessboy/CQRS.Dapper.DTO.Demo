﻿namespace DapperDemo.Models.DTOs;

    public class CompanyDTO
    {
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public List<EmployeeDTO>? Employees { get; set; }
}

