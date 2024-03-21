using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediatR;
using DapperDemo.CQRS.Queries;
using DapperDemo.Models.DTOs;
using DapperDemo.CQRS.Commands;

namespace DapperDemo.Controllers
{
    public class CompaniesController : Controller
    {
       
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
          
            _mediator = mediator;
            
        }

      
        public async Task<IActionResult> Index()
        {
          
              return View(await _mediator.Send(new CompaniesDTOQuery()));
        }

        
        public async Task<IActionResult> Details(int id)
        {


            var company = await _mediator.Send(new CompanyDTOQuery { Id = id});
                
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

     
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,Name,Address,City,State,PostalCode")] CompanyRequestDTO companyrequestDTO)
        {
            CompanyDTO companyDTO = null;

            if (ModelState.IsValid)
            {
                companyDTO = await _mediator.Send(new RequestCompanyCommand { CompanyRequest = companyrequestDTO});
                
                return RedirectToAction(nameof(Index));
            }
            return View(companyDTO);
        }

  
        public async Task<IActionResult> Edit(int id)
        {
           
            CompanyDTO companyDTO = await _mediator.Send(new CompanyDTOQuery { Id = id});
            if (companyDTO == null)
            {
                return NotFound();
            }
            return View(companyDTO);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,Name,Address,City,State,PostalCode")] CompanyRequestDTO companyRequestDTO)
        {
            if (id != companyRequestDTO.CompanyId)
            {
                return NotFound();
            }

            CompanyDTO companyDTO = null;

            if (ModelState.IsValid)
            {
                try
                {
                    companyDTO = await _mediator.Send(new RequestCompanyCommand { CompanyRequest = companyRequestDTO});
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if ( await CompanyExists(companyDTO.CompanyId) == false)
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
            return View(companyDTO);
        }

      
  
        public async Task<IActionResult> Delete(int id)
        {

            CompanyDTO companyDTO = await _mediator.Send(new CompanyDTOQuery { Id = id});
            if (companyDTO != null)
            {
                await _mediator.Send(new DeleteCompanyCommand { Id = id });
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CompanyExists(int id)
        {
            CompanyDTO companyDTO = await _mediator.Send(new CompanyDTOQuery { Id = id });
          return  companyDTO != null ? true : false;
        }
    }
}
