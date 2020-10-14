using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace StreamingSignalR.Hubs
{
    public interface IStreamHub
    {
        /// <summary>
        /// get server time from the server.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task GetServerTime(string date);
    }
}