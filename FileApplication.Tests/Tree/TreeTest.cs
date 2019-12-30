using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.Models;
using Xunit;

namespace FileApplication.Tests.Tree
{
    public class TreeTest : BaseComponentTest, IClassFixture<FileAppFixture>
    {
        public TreeTest(FileAppFixture fixture) 
            : base(fixture)
        { }

        [Fact]
        public async Task GetTree()
        {
            var tree = await Facade.GetTreeAsync();
            
            Assert.NotNull(tree);
        }
        
        [Fact]
        public async Task CopyFolder()
        {
            var tree = await Facade.GetTreeAsync();
            var leaf = tree.Children.First(x => x.Type == ComponentType.Folder);
            var beforeCount = tree.Children.Count;
            using var client = Fixture.Server.CreateClient();

            var model = new ComponentBase
            {
                Id = leaf.Id,
                Type = leaf.Type
            };
            
            await client.PutAsJsonAsync("component/copy", model)
                .Status(HttpStatusCode.OK);
            
            var afterCount = tree.Children.Count;
            
            Assert.Equal(beforeCount + 1, afterCount);
            
            var newLeaf = tree.Children.Last(x => x.Type == ComponentType.Folder);
            
            CompareCopiedFolders(leaf as FolderComponent, newLeaf as FolderComponent);
        }
        
        [Fact]
        public async Task Rename()
        {
            var tree = await Facade.GetTreeAsync();
            var child = tree.Children.First();
            using var client = Fixture.Server.CreateClient();

            var model = new RenameModel
            {
                Id = child.Id,
                Type = child.Type,
                NewName = "new name"
            };
            
            await client.PutAsJsonAsync("component/rename", model)
                .Status(HttpStatusCode.OK);
            
            child = tree.Children.First(x => x.Id == child.Id);

            Assert.Equal("new name", child.Name);
        }

        [Fact]
        public async Task DeleteFile()
        {
            var tree = await Facade.GetTreeAsync();
            var leaf = tree.Children.First(x => x.Type == ComponentType.File);
            using var client = Fixture.Server.CreateClient();
            
            await client.DeleteAsync($"component/{leaf.Type}/{leaf.Id}")
                .Status(HttpStatusCode.OK);
            
            var afterCount = tree.Children.Count;
            
            Assert.Null(tree.Children.FirstOrDefault(x => x.Id == leaf.Id));
        }

        [Fact]
        public async Task CreateSubFolder()
        {
            var tree = await Facade.GetTreeAsync();
            
            var leaf = tree.Children.First(x => x.Type == ComponentType.Folder);
            
            using var client = Fixture.Server.CreateClient();
            
            var subFolder = new FolderModel
            {
                Name = "Sub Folder",
                ParentId = leaf.Id
            };

            await client.PostAsJsonAsync("folder", subFolder)
                .Status(HttpStatusCode.OK);

            var newFolder = leaf.Children.FirstOrDefault(x => x.Name == "Sub Folder");

            Assert.NotNull(newFolder);
        }
    }
}