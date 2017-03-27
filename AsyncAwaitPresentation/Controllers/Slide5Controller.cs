using System.Threading.Tasks;
using System.Web.Http;

namespace AsyncAwaitPresentation.Controllers
{
    public class Slide5Controller : ApiController
    {
        private const int _delayTimeout = 5000;

        private async void VoidDelay()
        {
            await Task.Delay(_delayTimeout - 1000);
        }

        public async Task GetMethodWithAwait()
        {
            VoidDelay();
            await Task.Delay(_delayTimeout);
        }

        public async Task GetMethodWithCofigureAwait()
        {
            VoidDelay();
            await Task.Delay(_delayTimeout).ConfigureAwait(false);
        }

        public Task GetMethod()
        {
            VoidDelay();
            return Task.Delay(_delayTimeout);
        }
    }
}