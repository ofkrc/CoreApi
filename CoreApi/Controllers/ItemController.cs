using EmaAPI.Models;
using EmaAPI.Models.Request.Item;
using EmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmaAPI.Controllers
{
    [Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class ItemController : ControllerBase
	{
		private readonly IItemService _itemService;

		public ItemController(IItemService itemService)
		{
			_itemService = itemService;
		}

		[HttpPost("Insert")]
		public ActionResult<Item> Insert([FromBody] ItemRequestModel request)
		{
			var newItem = _itemService.Insert(request);

			return CreatedAtAction(nameof(Insert), new { id = newItem.RecordId }, newItem);
		}

		[HttpPut("Update")]
		public IActionResult Update(int itemId, [FromBody] ItemRequestModel request)
		{
			var updatedItem = _itemService.Update(itemId, request);
			if (updatedItem != null)
			{
				return Ok(updatedItem);
			}
			return NotFound();
		}

		[HttpGet("Get")]
		public IActionResult Get()
		{
			try
			{
				var invoices = _itemService.Search();
				return Ok(invoices);
			}
			catch (Exception ex)
			{
				// Diğer olası hataları ele alabilirsiniz.
				return StatusCode(500, "Bir hata oluştu.");
			}
		}


		[HttpGet("SearchItems")]
		public IActionResult SearchItems(string searchTerm)
		{
			try
			{
				var invoices = _itemService.SearchItems(searchTerm);
				return Ok(invoices);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Bir hata oluştu.");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteItems(int id)
		{
			try
			{
				_itemService.DeleteItems(id);
				return Ok("Ürün başarıyla silindi.");
			}
			catch (Exception ex)
			{
				// Diğer olası hataları ele alabilirsiniz.
				return StatusCode(500, "Bir hata oluştu.");
			}
		}

        [HttpGet("GetItemById")]
        public IActionResult GetItemById(int id)
        {
            var item = _itemService.GetItemById(id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound($"RecordId {id} ile eşleşen ürün bulunamadı.");
        }
    }
}
