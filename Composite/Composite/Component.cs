using System.Collections.Generic;

namespace Composite.Composite
{
    public abstract class Component
    {
        public abstract ComponentType Type { get; }
        
        public string Name { get; set; }
        public string Id { get; set; }
        public Component Parent { get; set; }
        public List<Component> Children { get; set; }
        

        public abstract void CopyTo(Component newParent);
        public abstract void Delete();
        

        public virtual string Rename(string newName)
        {
            Name = newName;
            return newName;
        }

        public virtual void AddChild(Component component)
        {
            Children ??= new List<Component>();
            
            component.Parent = this;
            
            Children.Add(component);
        }

        public virtual Component FindByIdAndType(ComponentType type, string id)
        {
            if (Type == type && Id == id)
            {
                return this;
            }

            if (Children != null)
            {
                foreach (var child in Children)
                {
                    var item = child.FindByIdAndType(type, id);

                    if (item != null)
                        return item;
                }
            }

            return null;
        }
    }
}