using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Runtime;
using Tasks.Interfaces;

namespace Tasks.Client
{
    public class TaskClientHostedService : IHostedService
    {
        private readonly IClusterClient _client;

        public TaskClientHostedService(IClusterClient client)
        {
            this._client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // example of calling grains from the initialized client
            var friend = this._client.GetGrain<ITaskGrain>("test");
            var response = await friend.SayHello("Good morning, my friend!");
            Console.WriteLine($"{response}");


        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
