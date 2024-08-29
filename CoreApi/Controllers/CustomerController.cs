using EmaAPI.Models;
using EmaAPI.Models.Request.Customer;
using EmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmaAPI.Controllers
{
    [Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerService _customerService;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpPost("Insert")]
		public ActionResult<Customer> Insert([FromBody] CustomerRequestModel request)
		{
			var newItem = _customerService.Insert(request);

			return CreatedAtAction(nameof(Insert), new { id = newItem.RecordId }, newItem);
		}

		[HttpPut("Update")]
		public IActionResult Update(int customerId, [FromBody] CustomerRequestModel request)
		{
			var updatedCustomer = _customerService.Update(customerId, request);
			if (updatedCustomer != null)
			{
				return Ok(updatedCustomer);
			}
			return NotFound();
		}

		[HttpGet("Get")]
		public IActionResult Get()
		{
			try
			{
				var customers = _customerService.Get();
				return Ok(customers);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Bir hata oluştu.");
			}
		}


		[HttpGet("SearchCustomers")]
		public IActionResult SearchCustomers(string searchTerm)
		{
			try
			{
				var customers = _customerService.SearchCustomers(searchTerm);
				return Ok(customers);
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
				_customerService.Delete(id);
				return Ok("Müşteri başarıyla silindi.");
			}
			catch (InvalidOperationException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Bir hata oluştu.");
			}
		}

        [HttpGet("GetCustomerById")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _customerService.GetCustomerById(id);

            if (customer != null)
            {
                return Ok(customer);
            }

            return NotFound($"RecordId {id} ile eşleşen müşteri bulunamadı.");
        }
    }
}
