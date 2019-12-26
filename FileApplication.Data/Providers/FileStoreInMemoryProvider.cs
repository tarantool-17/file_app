using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using FileApplication.BL.Providers;

namespace FileApplication.Data.Providers
{
    public class FileStoreInMemoryProvider : IFileStoreProvider
    {
        private readonly ConcurrentDictionary<string, Stream> _files;
        
        public async Task<Stream> GetDocumentStreamAsync(string src)
        {
            return _files[src];
        }

        public async Task<string> UploadDocumentAsync(Stream stream)
        {
            var fileName = Guid.NewGuid();
            var src = $"/{fileName}";
            
            if (!_files.TryAdd(src, stream))
            {
                throw new ArgumentException();
            }

            return src;
        }

        public async Task DeleteDocumentAsync(string src)
        {
            if (!_files.TryRemove(src, out _))
            {
                throw new ArgumentException();
            }
        }
    }
}