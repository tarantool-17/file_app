using System;
using System.Collections.Generic;
using System.Linq;
using FileApplication.BL.Models;

namespace FileApplication.BL.Services.Base
{
    public interface IComponentServiceFactory
    {
        IEnumerable<IComponentService> GetAllComponentServices();
        IComponentService GetComponentService(ComponentType type);
    }
    
    public class ComponentServiceFactory : IComponentServiceFactory
    {
        private readonly IEnumerable<IComponentService> _items;

        public ComponentServiceFactory(IEnumerable<IComponentService> items)
        {
            _items = items;
        }

        public IEnumerable<IComponentService> GetAllComponentServices()
        {
            return _items;
        }
        
        public IComponentService GetComponentService(ComponentType type)
        {
            var item = _items.FirstOrDefault(x => x.Type == type) ?? throw new ArgumentException();

            return item;
        }
    }
}