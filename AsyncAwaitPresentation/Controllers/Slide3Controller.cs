using System.Threading.Tasks;
using System.Web.Http;

namespace AsyncAwaitPresentation.Controllers
{
    public class Slide3Controller : ApiController
    {
        private const int _delayTimeout = 5000;

        public async Task GetMethodWithAwait()
        {
            await Task.Delay(_delayTimeout);
        }

        public async Task GetMethodWithCofigureAwait()
        {
            await Task.Delay(_delayTimeout).ConfigureAwait(false);
        }

        public Task GetMethod()
        {
            return Task.Delay(_delayTimeout);
        }
    }
}