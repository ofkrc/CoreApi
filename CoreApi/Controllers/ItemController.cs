using CoreApi.Core.Base.Model;
using CoreApi.Models;
using CoreApi.Models.Request.Item;
using CoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : BaseController
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost("Insert")]
        public ActionResult<BaseResponse<Item>> Insert([FromBody] ItemRequestModel request)
        {
            try
            {
                var newItem = _itemService.Insert(request);
                return SuccessResponse(newItem, "Ürün başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<Item>(ex.Message);
            }
        }

        [HttpPut("Update")]
        public ActionResult<BaseResponse<Item>> Update(int itemId, [FromBody] ItemRequestModel request)
        {
            try
            {
                var updatedItem = _itemService.Update(itemId, request);
                if (updatedItem != null)
                {
                    return SuccessResponse(updatedItem, "Ürün başarıyla güncellendi.");
                }
                return ErrorResponse<Item>("Ürün bulunamadı.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<Item>(ex.Message);
            }
        }

        [HttpGet("Get")]
        public ActionResult<BaseResponse<IEnumerable<Item>>> Get()
        {
            try
            {
                var items = _itemService.Search();
                return SuccessResponse(items, "Ürünler başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<IEnumerable<Item>>(ex.Message);
            }
        }

        [HttpGet("SearchItems")]
        public ActionResult<BaseResponse<IEnumerable<Item>>> SearchItems(string searchTerm)
        {
            try
            {
                var items = _itemService.SearchItems(searchTerm);
                return SuccessResponse(items, "Arama sonuçları başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<IEnumerable<Item>>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseResponse<string>> DeleteItems(int id)
        {
            try
            {
                _itemService.DeleteItems(id);
                return SuccessResponse("Ürün başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<string>(ex.Message);
            }
        }

        [HttpGet("GetItemById")]
        public ActionResult<BaseResponse<Item>> GetItemById(int id)
        {
            try
            {
                var item = _itemService.GetItemById(id);
                if (item != null)
                {
                    return SuccessResponse(item, "Ürün başarıyla getirildi.");
                }
                return ErrorResponse<Item>($"RecordId {id} ile eşleşen ürün bulunamadı.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<Item>(ex.Message);
            }
        }
    }
}
