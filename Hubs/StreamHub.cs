using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace StreamingSignalR.Hubs
{

    public class StreamHub : Hub<IStreamHub>
    {
        public StreamHub()
        {
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public ChannelReader<int> DelayCounter(int delay, CancellationToken cancellation)
        {
            var channel = Channel.CreateUnbounded<int>();
            _ = WriteItems(channel.Writer, 1000000, delay);
            return channel.Reader;
        }

        private async Task WriteItems(ChannelWriter<int> writer, int count, int delay)
        {
            for (int i = 0; i < count; i++)
            {
                await writer.WriteAsync(i);
                await Task.Delay(delay);
            }
            writer.TryComplete();
        }
    }
}
