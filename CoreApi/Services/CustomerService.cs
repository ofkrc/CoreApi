using EmaAPI.Helpers;
using EmaAPI.Models;
using EmaAPI.Models.Request.Customer;
using EmaAPI.Repositories;
using EmaAPI.Services.Interfaces;

namespace EmaAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;

        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public Customer GetCustomerById(int id)
        {
            return _repository.Find(id);
        }

        public Customer Insert(CustomerRequestModel request)
        {
            var newCustomer = new Customer();
            GenericMappingHelper.Map(request, newCustomer);
            return _repository.Add(newCustomer);
        }

        public Customer Update(int customerId, CustomerRequestModel request)
        {
            var existingCustomer = _repository.Find(customerId);

            if (existingCustomer == null)
            {
                throw new InvalidOperationException("Belirtilen ID'ye sahip müşteri bulunamadı.");
            }

            GenericMappingHelper.Map(request, existingCustomer);
            return _repository.Update(existingCustomer);
        }

        public IEnumerable<Customer> Get()
        {
            return _repository.List().Where(i => !i.Deleted.HasValue || (i.Deleted.HasValue && i.Deleted.Value == false));
        }

        public IEnumerable<Customer> SearchCustomers(string searchTerm)
        {
            var query = _repository.List().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(i =>
                    i.Surname.Contains(searchTerm) ||
                    i.Name.Contains(searchTerm) ||
                    i.PhoneNumber.Contains(searchTerm) ||
                    i.Email.Contains(searchTerm));
            }

            return query.Where(i => !i.Deleted.HasValue || (i.Deleted.HasValue && i.Deleted.Value == false)).ToList();
        }

        public void Delete(int customerId)
        {
            var customerToDelete = _repository.Find(customerId);

            if (customerToDelete != null)
            {
                customerToDelete.Deleted = true;
                _repository.Update(customerToDelete);
            }
            else
            {
                throw new InvalidOperationException("Belirtilen ID'ye sahip müşteri bulunamadı.");
            }
        }
    }
}
