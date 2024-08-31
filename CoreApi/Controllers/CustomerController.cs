using CoreApi.Core.Base.Model;
using CoreApi.Models;
using CoreApi.Models.Request.Customer;
using CoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("Insert")]
        public ActionResult<BaseResponse<Customer>> Insert([FromBody] CustomerRequestModel request)
        {
            try
            {
                var newCustomer = _customerService.Insert(request);
                return SuccessResponse(newCustomer, "Müşteri başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<Customer>(ex.Message);
            }
        }

        [HttpPut("Update")]
        public ActionResult<BaseResponse<Customer>> Update(int customerId, [FromBody] CustomerRequestModel request)
        {
            try
            {
                var updatedCustomer = _customerService.Update(customerId, request);
                if (updatedCustomer != null)
                {
                    return SuccessResponse(updatedCustomer, "Müşteri başarıyla güncellendi.");
                }
                return ErrorResponse<Customer>($"Customer with ID {customerId} not found.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<Customer>(ex.Message);
            }
        }

        [HttpGet("Get")]
        public ActionResult<BaseResponse<IEnumerable<Customer>>> Get()
        {
            try
            {
                var customers = _customerService.Get();
                return SuccessResponse(customers, "Müşteriler başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<IEnumerable<Customer>>(ex.Message);
            }
        }

        [HttpGet("SearchCustomers")]
        public ActionResult<BaseResponse<IEnumerable<Customer>>> SearchCustomers(string searchTerm)
        {
            try
            {
                var customers = _customerService.SearchCustomers(searchTerm);
                return SuccessResponse(customers, "Arama sonuçları başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<IEnumerable<Customer>>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseResponse<string>> Delete(int id)
        {
            try
            {
                _customerService.Delete(id);
                return SuccessResponse("Müşteri başarıyla silindi.");
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

        [HttpGet("GetCustomerById")]
        public ActionResult<BaseResponse<Customer>> GetCustomerById(int id)
        {
            try
            {
                var customer = _customerService.GetCustomerById(id);

                if (customer != null)
                {
                    return SuccessResponse(customer, "Müşteri başarıyla getirildi.");
                }

                return ErrorResponse<Customer>($"RecordId {id} ile eşleşen müşteri bulunamadı.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<Customer>(ex.Message);
            }
        }
    }
}
