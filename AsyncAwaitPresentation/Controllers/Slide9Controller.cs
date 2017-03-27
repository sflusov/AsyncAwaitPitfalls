using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AsyncAwaitPresentation.Controllers
{
    public class Slide9Controller : ApiController
    {
        private const int _delayTimeout = 5000;

        public Task GetMethodWithAwait()
        {
            var httpCtxBefore = HttpContext.Current;
            var syncContext = TaskScheduler.FromCurrentSynchronizationContext();

            return Task.Delay(_delayTimeout).ContinueWith(
                continuationAction: t =>
                {
                    var httpCtxAfter = HttpContext.Current;
                    httpCtxAfter.Response.Headers.Set("async", "await");
                },
                scheduler: syncContext);
        }

        public Task GetMethodWithCofigureAwait()
        {
            var httpCtxBefore = HttpContext.Current;

            return Task.Delay(_delayTimeout).ContinueWith(
                continuationAction: t =>
                {
                    var httpCtxAfter = HttpContext.Current;
                    httpCtxAfter.Response.Headers.Set("async", "await");
                },
                continuationOptions: TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}