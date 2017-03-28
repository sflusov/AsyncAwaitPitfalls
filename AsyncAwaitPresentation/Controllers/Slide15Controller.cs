using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;

namespace AsyncAwaitPresentation.Controllers
{
    public static class TaskExtension
    {
        public static Task ForEachAsync<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, Task<TResult>> taskSelector,
            Action<TSource, TResult> resultProcessor,
            int parallelCount)
        {
            var oneAtATime = new SemaphoreSlim(parallelCount);

            var tasksSet = new List<Task>();
            foreach (TSource oneSource in source)
            {
                var oneTask = ProcessAsync(oneSource, taskSelector, resultProcessor, oneAtATime);
                tasksSet.Add(oneTask);
            }

            return Task.WhenAll(tasksSet);
        }

        private static async Task ProcessAsync<TSource, TResult>(
            TSource item,
            Func<TSource, Task<TResult>> taskSelector,
            Action<TSource, TResult> resultProcessor,
            SemaphoreSlim oneAtATime)
        {
            await oneAtATime.WaitAsync();
            try
            {
                TResult result = await taskSelector(item);
                resultProcessor(item, result);
            }
            finally
            {
                oneAtATime.Release();
            }
        }
    }

    public class Slide15Controller : ApiController
    {
        private const int _delayTimeout = 1000;

        public async Task<string> GetCount(Guid itemId)
        {
            await Task.Delay(_delayTimeout).ConfigureAwait(false);

            Random rnd = new Random();
            return rnd.Next(16).ToString();
        }

        public async Task GetForeachAsync()
        {
            const int maxCount = 12;

            List<Guid> items = new List<Guid>(maxCount);
            for (int i = 0; i < maxCount; i++)
                items.Add(Guid.NewGuid());

            Dictionary<Guid, string> result = new Dictionary<Guid, string>(maxCount);

            using (var client = new HttpClient() { BaseAddress = new Uri("http://localhost:57616/") })
            {
                await items.ForEachAsync(
                    itemId => client.GetStringAsync($"/api/Slide14/GetCount?itemId={itemId}"),
                    (src, res) => result.Add(src, res),
                    4
                );
            }
        }
    }
}