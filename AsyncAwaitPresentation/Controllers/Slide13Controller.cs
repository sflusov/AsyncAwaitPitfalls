using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace AsyncAwaitPresentation.Controllers
{
    public class Slide13Controller : ApiController
    {
        private const int _delayTimeout = 5000;

        private object _sync = new object();

        public async Task GetLockWithAwait()
        {
            Monitor.Enter(_sync);
            try
            {
                await Task.Delay(_delayTimeout);
            }
            finally
            {
                Monitor.Exit(_sync);
            }
        }

        public async Task GetLockAsMonitorWithCofigureAwait()
        {
            Monitor.Enter(_sync);
            try
            {
                await Task.Delay(_delayTimeout).ConfigureAwait(false);
            }
            finally
            {
                Monitor.Exit(_sync);
            }
        }
    }
}