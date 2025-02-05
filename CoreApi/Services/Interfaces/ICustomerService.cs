﻿using CoreApi.Models.Request.Customer;
using CoreApi.Models;

namespace CoreApi.Services.Interfaces
{
    public interface ICustomerService
    {
        Customer Insert(CustomerRequestModel request);
        Customer Update(int customerId, CustomerRequestModel request);
        void Delete(int customerId);
        IEnumerable<Customer> SearchCustomers(string searchTerm);
        IEnumerable<Customer> Get();
        Customer GetCustomerById(int id);
    }
}
