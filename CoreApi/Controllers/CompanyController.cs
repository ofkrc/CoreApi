using EmaAPI.Models;
using EmaAPI.Models.Request.Company;
using EmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmaAPI.Controllers
{
    [Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class CompanyController : ControllerBase
	{
		private readonly ICompanyService _companyService;

		public CompanyController(ICompanyService companyService)
		{
			_companyService = companyService;
		}

		[HttpPost("Insert")]
		public ActionResult<Company> Insert([FromBody] CompanyRequestModel request)
		{
			var newItem = _companyService.Insert(request);

			return CreatedAtAction(nameof(Insert), new { id = newItem.RecordId }, newItem);
		}

		[HttpPut("Update")]
		public IActionResult Update(int companyId, [FromBody] CompanyRequestModel request)
		{
			var updatedCompany = _companyService.Update(companyId, request);
			if (updatedCompany != null)
			{
				return Ok(updatedCompany);
			}
			return NotFound();
		}

		[HttpGet("Get")]
		public IActionResult Get()
		{
			try
			{
				var companies = _companyService.Search();
				return Ok(companies);
			}
			catch (Exception ex)
			{
				// Diğer olası hataları ele alabilirsiniz.
				return StatusCode(500, "Bir hata oluştu.");
			}
		}

		[HttpGet("SearchCompanies")]
		public IActionResult SearchCompanies(string searchTerm)
		{
			try
			{
				var companies = _companyService.SearchCompanies(searchTerm);
				return Ok(companies);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Bir hata oluştu.");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				_companyService.DeleteCompanies(id);
				return Ok("Ürün başarıyla silindi.");
			}
			catch (InvalidOperationException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				// Diğer olası hataları ele alabilirsiniz.
				return StatusCode(500, "Bir hata oluştu.");
			}
		}

        [HttpGet("GetCompanyById")]
        public IActionResult GetCompanyById(int id)
        {
            var company = _companyService.GetCompanyById(id);

            if (company != null)
            {
                return Ok(company);
            }

            return NotFound($"RecordId {id} ile eşleşen şirket bulunamadı.");
        }
    }
}
