using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace GameServer
{
    public class Server
    {
        public Server()
        {
            WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build().Run();
        }
    }
}
