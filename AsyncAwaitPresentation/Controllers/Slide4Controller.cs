using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AsyncAwaitPresentation.Controllers
{
    public class Slide4Controller : ApiController
    {
        private const int _delayTimeout = 5000;

        public async Task GetMethodWithAwait()
        {
            var httpCtxBefore = HttpContext.Current;

            await Task.Delay(_delayTimeout);

            var httpCtxAfter = HttpContext.Current;
            httpCtxAfter.Response.Headers.Set("async", "await");
        }

        public async Task GetMethodWithCofigureAwait()
        {
            var httpCtxBefore = HttpContext.Current;

            await Task.Delay(_delayTimeout).ConfigureAwait(false);

            var httpCtxAfter = HttpContext.Current;
            httpCtxAfter.Response.Headers.Set("async", "await");
        }
    }
}