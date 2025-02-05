﻿using CoreApi.Core.Helpers;
using CoreApi.Core.Repositories;
using CoreApi.Models;
using CoreApi.Models.Request.Item;
using CoreApi.Services.Interfaces;

namespace CoreApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _itemRepository;

        public ItemService(IRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public Item Insert(ItemRequestModel request)
        {
            var newItem = new Item();
            GenericMappingHelper.Map(request, newItem);
            newItem = _itemRepository.Add(newItem);

            return newItem;
        }

        public Item Update(int itemId, ItemRequestModel request)
        {
            var existingItem = _itemRepository.Find(itemId);

            if (existingItem == null)
            {
                throw new InvalidOperationException("Belirtilen ID'ye sahip ürün bulunamadı.");
            }

            GenericMappingHelper.Map(request, existingItem);
            _itemRepository.Update(existingItem);

            return existingItem;
        }

        public IEnumerable<Item> Search()
        {
            return _itemRepository.List()
                .Where(i => !i.Deleted.HasValue || (i.Deleted.HasValue && i.Deleted.Value == false))
                .ToList();
        }

        public IEnumerable<Item> SearchItems(string searchTerm)
        {
            var query = _itemRepository.List().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(i =>
                    i.Code.Contains(searchTerm) ||
                    i.Name.Contains(searchTerm) ||
                    i.Description.Contains(searchTerm));
            }

            return query.Where(i => !i.Deleted.HasValue || (i.Deleted.HasValue && i.Deleted.Value == false)).ToList();
        }

        public void DeleteItems(int itemId)
        {
            var itemToDelete = _itemRepository.Find(itemId);

            if (itemToDelete != null)
            {
                itemToDelete.Deleted = true;
                _itemRepository.Update(itemToDelete);
            }
            else
            {
                throw new InvalidOperationException("Belirtilen ID'ye sahip ürün bulunamadı.");
            }
        }

        public Item GetItemById(int id)
        {
            return _itemRepository.Find(id);
        }
    }
}
