using System;

namespace FileApplication.BL.Models
{
    public class FileComponent : Component
    {
        public override ComponentType Type => ComponentType.File;
        
        public long Size { get; set; }
        public string Src { get; set; }

        public override void CopyTo(Component newParent)
        {
            newParent.AddChild(new FileComponent
            {
                Size = Size,
                Src = Src,
                Id = Id, // TODO:
                Name = $"Copy of {Name}"
            });
        }

        public override void Delete()
        {
            if(Parent == null)
                throw new InvalidOperationException();

            Parent.Children.Remove(this);
        }
    }
}