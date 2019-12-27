using System.Collections.Generic;
using FileApplication.BL.Entities;

namespace FileApplication.BL.Models
{
    public class TreeItemModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentFolderId { get; set; }
        public ItemType Type { get; set; }

        public List<TreeItemModel> Children { get; set; }
    }
}