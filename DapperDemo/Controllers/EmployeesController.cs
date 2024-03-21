
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediatR;
using DapperDemo.Models.DTOs;
using DapperDemo.CQRS.Queries;
using DapperDemo.CQRS.Commands;

namespace DapperDemo.Controllers
{
    public class EmployeesController : Controller
    {



        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
           
        }

       
        public async Task<IActionResult> Index(int companyId=0)
        {
            //List<Employee> employees = _empRepo.GetAll();   
            //foreach(Employee obj in employees)
            //{
            //    obj.Company = _compRepo.Find(obj.CompanyId);
            //}
            IEnumerable<EmployeeDTO> employees = await _mediator.Send(new EmployeesDTOQuery()); 
            //return View(_bonusRepo.GetEmployeeWithCompany(companyId));
            return View(employees);
        }

   

      
        public async Task<IActionResult> Create()
        {
            IEnumerable<CompanyDTO> companies = await _mediator.Send(new CompaniesDTOQuery());
            IEnumerable<SelectListItem> companyList = companies.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CompanyId.ToString()
            });
            ViewBag.CompanyList = companyList;
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost(EmployeeRequestDTO employeeRequestDTO)
        {
           
            if (ModelState.IsValid)
            {
                EmployeeDTO employeeDTO = await _mediator.Send(new RequestEmployeeCommand { EmployeeRequest = employeeRequestDTO});
                return RedirectToAction(nameof(Index));
            }
 
            return View(employeeRequestDTO);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            EmployeeDTO employeeDTO = await _mediator.Send(new EmployeeDTOQuery { Id = id.Value});
            if (employeeDTO == null)
            {
                return NotFound();
            }
            IEnumerable<CompanyDTO> companies = await _mediator.Send(new CompaniesDTOQuery());
            IEnumerable<SelectListItem> companyList = companies.Select(i => new SelectListItem
            {
                Text= i.Name,
                Value = i.CompanyId.ToString()
            });

            ViewBag.CompanyList = companyList;
            
            return View(employeeDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EmployeeRequestDTO employeeRequestDTO)
        {
            if (id != employeeRequestDTO.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _mediator.Send(new RequestEmployeeCommand { EmployeeRequest = employeeRequestDTO});
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await EmployeeExists(employeeRequestDTO.EmployeeId) == false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(await _mediator.Send(new CompaniesDTOQuery()), "CompanyId", "CompanyId", employeeRequestDTO.CompanyId);
            return View(employeeRequestDTO);
        }

      
  

      
        public async Task<IActionResult> Delete(int id)
        {
            
            EmployeeDTO employeeDTO = await _mediator.Send(new EmployeeDTOQuery { Id = id });
            if (employeeDTO != null)
            {
                await _mediator.Send(new DeleteEmployeeCommand { Id = id});
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async  Task<bool> EmployeeExists(int id)
        {
            EmployeeDTO employeeDTO = await _mediator.Send(new EmployeeDTOQuery { Id = id});
          return  employeeDTO != null ? true : false;
        }
    }
}
