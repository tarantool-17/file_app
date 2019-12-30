using System.ComponentModel;
using FileApplication.BL.Models;
using FileApplication.BL.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FileApplication.Tests
{
    public class BaseComponentTest
    {
        public readonly FileAppFixture Fixture;
        public readonly IFacade Facade;
        
        public BaseComponentTest(FileAppFixture fixture)
        {
            Fixture = fixture;
            Facade = fixture.Server.Services.GetRequiredService<IFacade>();
        }
        
        public void CompareCopiedFolders(FolderComponent left, FolderComponent right)
        {
            Assert.NotEqual(left.Id, right.Id);
            Assert.Equal(left.Type, right.Type);
            Assert.Equal($"Copy of {left.Name}", right.Name);
            // Assert.Equal(left.ParentId, right.ParentId); ??
            Assert.Equal(left.Children?.Count ?? 0, right.Children?.Count ?? 0);

            if (left.Children != null)
            {
                for (int i = 0; i < left.Children.Count; i++)
                {
                    var leftChild = left.Children[i];
                    var rightChild = right.Children[i];
                    
                    Assert.Equal(leftChild.Type, rightChild.Type);

                    switch (leftChild.Type)
                    {
                        case ComponentType.File:
                            CompareCopiedFiles(leftChild as FileComponent, rightChild as FileComponent);
                            break;
                        
                        case ComponentType.Folder:
                            CompareCopiedFolders(leftChild as FolderComponent, rightChild as FolderComponent);
                            break;
                        
                        default:
                            throw new InvalidEnumArgumentException();
                    }
                }
            }
        }
        
        public void CompareCopiedFiles(FileComponent left, FileComponent right)
        {
            Assert.NotEqual(left.Id, right.Id);
            Assert.Equal(left.Type, right.Type);
            Assert.Equal($"Copy of {left.Name}", right.Name);
            // Assert.Equal(left.ParentId, right.ParentId); 
            Assert.Equal(left.Size, right.Size);
            Assert.Equal(left.Src, right.Src);
        }
    }
}