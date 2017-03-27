using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AsyncAwaitPresentation.Controllers
{
    public class Slide10Controller : ApiController
    {
       private const int _delayTimeout = 5000;

        public Task GetMethodWithAwait()
        {
            var httpCtxBefore = HttpContext.Current;
            var syncContext = SynchronizationContext.Current;

            return Task.Delay(_delayTimeout).ContinueWith(
                continuationAction: t =>
                {
                    //switch to context Synchronously
                    syncContext.Send(
                        d: new SendOrPostCallback(s => 
                        {
                            var httpCtxAfter = HttpContext.Current;
                            httpCtxAfter.Response.Headers.Set("async", "await");
                        }), 
                        state: null);
                });
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