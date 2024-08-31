using CoreApi.Core.Base.Model;
using CoreApi.Models;
using CoreApi.Models.Request.Company;
using CoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("Insert")]
        public ActionResult<BaseResponse<Company>> Insert([FromBody] CompanyRequestModel request)
        {
            try
            {
                var newCompany = _companyService.Insert(request);
                return SuccessResponse(newCompany, "Şirket başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<Company>(ex.Message);
            }
        }

        [HttpPut("Update")]
        public ActionResult<BaseResponse<Company>> Update(int companyId, [FromBody] CompanyRequestModel request)
        {
            try
            {
                var updatedCompany = _companyService.Update(companyId, request);
                if (updatedCompany != null)
                {
                    return SuccessResponse(updatedCompany, "Şirket başarıyla güncellendi.");
                }
                return ErrorResponse<Company>($"Şirket ile ID {companyId} bulunamadı.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<Company>(ex.Message);
            }
        }

        [HttpGet("Get")]
        public ActionResult<BaseResponse<IEnumerable<Company>>> Get()
        {
            try
            {
                var companies = _companyService.Search();
                return SuccessResponse(companies, "Şirketler başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<IEnumerable<Company>>(ex.Message);
            }
        }

        [HttpGet("SearchCompanies")]
        public ActionResult<BaseResponse<IEnumerable<Company>>> SearchCompanies(string searchTerm)
        {
            try
            {
                var companies = _companyService.SearchCompanies(searchTerm);
                return SuccessResponse(companies, "Arama sonuçları başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<IEnumerable<Company>>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseResponse<string>> Delete(int id)
        {
            try
            {
                _companyService.DeleteCompanies(id);
                return SuccessResponse("Şirket başarıyla silindi.");
            }
            catch (InvalidOperationException ex)
            {
                return ErrorResponse<string>(ex.Message);
            }
            catch (Exception ex)
            {
                return ErrorResponse<string>("Bir hata oluştu.");
            }
        }

        [HttpGet("GetCompanyById")]
        public ActionResult<BaseResponse<Company>> GetCompanyById(int id)
        {
            try
            {
                var company = _companyService.GetCompanyById(id);

                if (company != null)
                {
                    return SuccessResponse(company, "Şirket başarıyla getirildi.");
                }

                return ErrorResponse<Company>($"RecordId {id} ile eşleşen şirket bulunamadı.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<Company>(ex.Message);
            }
        }
    }
}
