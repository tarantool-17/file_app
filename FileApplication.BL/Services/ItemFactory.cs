using System;
using System.Collections.Generic;
using System.Linq;
using FileApplication.BL.Entities;

namespace FileApplication.BL.Services
{
    public interface IItemFactory
    {
        IItemActionService GetItemService(ItemType type);
    }
    
    public class ItemFactory : IItemFactory
    {
        private readonly IEnumerable<IItemActionService> _items;

        public ItemFactory(IEnumerable<IItemActionService> items)
        {
            _items = items;
        }
        
        public IItemActionService GetItemService(ItemType type)
        {
            var item = _items.FirstOrDefault(x => x.Type == type) ?? throw new ArgumentException();

            return item;
        }
    }
}