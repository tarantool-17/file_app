using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.Models;
using Xunit;

namespace FileApplication.Tests.Tree
{
    public class CopyTest : BaseComponentTest, IClassFixture<FileAppFixture>
    {
        public CopyTest(FileAppFixture fixture) 
            : base(fixture)
        {
        }

        [Fact]
        public async Task CopyLeaf()
        {
            var tree = await Facade.GetTreeAsync();
            var leaf = tree.Children.First(x => x.Type == ComponentType.File);
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
            
            var newLeaf = tree.Children.Last(x => x.Type == ComponentType.File);
            
            CompareCopiedFiles(leaf as FileComponent, newLeaf as FileComponent);
        }
    }
}