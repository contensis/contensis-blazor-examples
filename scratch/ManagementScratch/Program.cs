using System;
using System.Threading.Tasks;
using Zengenti.Contensis.Management;

namespace ManagementScratch
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootUri = "https://cms-develop.cloud.contensis.com";
            var clientId = "ff8beb53-446a-44cf-a136-d0c8ea4af3d4";
            var sharedSecret = "13c291feefa645d5b13252f94884665d-91c88c5e073e4a5997629fc4c04042fd-fda88cd43ae14b69838bf9a0e904cade";
            var managementClient = ManagementClient.Create(rootUri, clientId, sharedSecret);

            var projects = await managementClient.Projects.ListAsync();

            foreach (var project in projects)
            {
                Console.WriteLine(project.Name);
            }

            Console.ReadKey();
        }
    }
}