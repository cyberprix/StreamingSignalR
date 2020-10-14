using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace StreamingSignalR.Hubs
{
    public interface IStreamHub
    {
        Task GetServerTime(string date);
    }
}