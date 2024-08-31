using CoreApi.Models.Request.Item;
using CoreApi.Models;

namespace CoreApi.Services.Interfaces
{
    public interface IItemService
    {
        Item Insert(ItemRequestModel request);
        Item Update(int itemId, ItemRequestModel request);
        IEnumerable<Item> Search();
        IEnumerable<Item> SearchItems(string searchTerm);
        void DeleteItems(int itemId);
        Item GetItemById(int id);
    }
}
