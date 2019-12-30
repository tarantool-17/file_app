using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace FileApplication.Tests
{
    public class FileAppFixture
    {
        private static TestServer _server { get; }

        public TestServer Server => _server;

        static FileAppFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>();
            
            _server = new TestServer(builder);
        }
    }
}