using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetaVerseApi.Test
{
    public class InjectionFixture
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public InjectionFixture()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            client = server.CreateClient();
        }

        public IServiceProvider ServiceProvider => server.Host.Services;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                server.Dispose();
                client.Dispose();
            }
        }
    }
}
