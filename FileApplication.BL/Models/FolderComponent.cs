namespace FileApplication.BL.Models
{
    public class FolderComponent : Component
    {
        public override ComponentType Type => ComponentType.Folder;
        
        public override Component CopyTo(Component newParent, string newId)
        {
            var newFolder = new FolderComponent
            {
                Id = newId,
                Name = $"Copy of {Name}"
            };
            
            newParent.AddChild(newFolder);

            return newFolder;
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