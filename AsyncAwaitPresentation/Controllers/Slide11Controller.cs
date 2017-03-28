using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace AsyncAwaitPresentation.Controllers
{
    public class Slide11Controller : ApiController
    {
        private const int _delayTimeout = 1000;

        public async Task GetForeachWithAwait()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(_delayTimeout);
            }
        }

        public async Task GetForeachWithCofigureAwait()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(_delayTimeout).ConfigureAwait(false);
            }
        }

        public async Task GetForeachAsync()
        {
            const int maxCount = 10;

            List<Task> tasksSet = new List<Task>(maxCount);
            for (int i = 0; i < maxCount; i++)
            {
                tasksSet.Add(Task.Delay(_delayTimeout));
            }

            await Task.WhenAll(tasksSet);
        }
    }
}