using EmaAPI.Models.Request.Company;
using EmaAPI.Models;

namespace EmaAPI.Services.Interfaces
{
    public interface ICompanyService
    {
        Company Insert(CompanyRequestModel request);
        Company Update(int companyId, CompanyRequestModel request);
        IEnumerable<Company> Search();
        IEnumerable<Company> SearchCompanies(string searchTerm);
        void DeleteCompanies(int companiesId);
        Company GetCompanyById(int id);
    }
}
