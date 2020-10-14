using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StreamingSignalR.Hubs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StreamingSignalR.Intergration
{
    public class BackgroundServerTimeTimer : IDisposable, IHostedService
    {
        private IServiceProvider __provider;
        private ILogger<BackgroundServerTimeTimer> __log;
        private Timer __timer;

        public BackgroundServerTimeTimer(IServiceProvider service, ILogger<BackgroundServerTimeTimer> logger)
        {
            __log = logger;
            __provider = service;
        }

        private void OnTimerElapsed(object sender)
        {
            var hub = __provider.GetRequiredService<IHubContext<StreamHub, IStreamHub>>();
            if (null != hub)
            {
                __log.LogDebug("Request server time /start");
                hub.Clients.All.GetServerTime(DateTime.Now.ToString());
                __log.LogDebug("Response server time /end");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            __timer = new Timer(OnTimerElapsed, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            __timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (null != __timer)
            {
                __timer.Dispose();
            }
        }

    }
}
