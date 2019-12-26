using System.Collections.Generic;
using FileApplication.BL.Entities;

namespace FileApplication.BL.Models
{
    public class TreeItemModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public int? ParentFolderId { get; set; }
        
        public List<TreeItemModel> Children { get; set; }
    }
}