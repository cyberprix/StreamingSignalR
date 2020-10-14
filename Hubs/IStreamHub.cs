using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace StreamingSignalR.Hubs
{
    public interface IStreamHub
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task GetServerTime(string date);
    }
}