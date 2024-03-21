﻿namespace DapperDemo.Models.DTOs;

    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public int CompanyId { get; set; }
        public virtual CompanyDTO? Company { get; set; }
    }
