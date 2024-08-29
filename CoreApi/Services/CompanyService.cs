using EmaAPI.Helpers;
using EmaAPI.Models;
using EmaAPI.Models.Request.Company;
using EmaAPI.Repositories;
using EmaAPI.Services.Interfaces;

namespace EmaAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _repository;

        public CompanyService(IRepository<Company> repository)
        {
            _repository = repository;
        }

        public Company Insert(CompanyRequestModel request)
        {
            var newCompany = new Company();
            GenericMappingHelper.Map(request, newCompany);

            return _repository.Add(newCompany);
        }

        public Company GetCompanyById(int id)
        {
            return _repository.Find(id);
        }

        public Company Update(int companyId, CompanyRequestModel request)
        {
            var existingCompany = _repository.Find(companyId);

            if (existingCompany == null)
            {
                throw new InvalidOperationException("Belirtilen ID'ye sahip şirket bulunamadı.");
            }

            GenericMappingHelper.Map(request, existingCompany);

            return _repository.Update(existingCompany);
        }

        public IEnumerable<Company> Search()
        {
            return _repository.List().Where(i => !i.Deleted.HasValue || (i.Deleted.HasValue && i.Deleted.Value == false));
        }

        public IEnumerable<Company> SearchCompanies(string searchTerm)
        {
            var query = _repository.List().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(i =>
                    i.CompanyName.Contains(searchTerm) ||
                    i.Address.Contains(searchTerm) ||
                    i.PhoneNumber.Contains(searchTerm) ||
                    i.TaxNo.Contains(searchTerm) ||
                    i.TaxOffice.Contains(searchTerm) ||
                    i.Email.Contains(searchTerm));
            }

            return query.Where(i => !i.Deleted.HasValue || (i.Deleted.HasValue && i.Deleted.Value == false)).ToList();
        }

        public void DeleteCompanies(int companiesId)
        {
            var companyToDelete = _repository.Find(companiesId);

            if (companyToDelete != null)
            {
                companyToDelete.Deleted = true;
                _repository.Update(companyToDelete);
            }
            else
            {
                throw new InvalidOperationException("Belirtilen ID'ye sahip şirket bulunamadı.");
            }
        }
    }
}
