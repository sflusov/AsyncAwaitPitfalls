using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace AsyncAwaitPresentation.Controllers
{
    public class Slide14Controller : ApiController
    {
        private const int _delayTimeout = 5000;

        public static SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public async Task GetLockWithAwait()
        {
            await _semaphore.WaitAsync();
            try
            {
                await Task.Delay(_delayTimeout);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}