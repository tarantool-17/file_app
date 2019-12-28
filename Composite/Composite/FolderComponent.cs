namespace Composite.Composite
{
    public class FolderComponent : Component
    {
        public override ComponentType Type => ComponentType.Folder;
        
        public override void CopyTo(Component newParent)
        {
            var newFolder = new FolderComponent
            {
                Id = Id, //TODO
                Name = $"Copy of {Name}"
            };
            
            newParent.AddChild(newFolder);
            
            if (Children != null)
            {
                foreach (var child in Children)
                {
                    child.CopyTo(newFolder);
                }
            }
        }

        public override void Delete()
        {
            if (Children != null)
            {
                foreach (var child in Children)
                {
                    child.Delete();
                }
            }

            Parent?.Children.Remove(this);
        }
    }
}