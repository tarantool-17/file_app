using System;

namespace FileApplication.BL.Models
{
    public class FileComponent : Component
    {
        public override ComponentType Type => ComponentType.File;
        
        public long Size { get; set; }
        public string Src { get; set; }

        public override Component CopyTo(Component newParent, string newId)
        {
            var componsent = new FileComponent
            {
                Size = Size,
                Src = Src,
                Id = newId,
                Name = $"Copy of {Name}"
            };
            
            newParent.AddChild(componsent);

            return componsent;
        }

        public override void Delete()
        {
            if(Parent == null)
                throw new InvalidOperationException();

            Parent.Children.Remove(this);
        }
    }
}