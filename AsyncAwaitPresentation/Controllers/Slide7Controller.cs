using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AsyncAwaitPresentation.Controllers
{
    public class Slide7HttpClient
    {
        private HttpClient _client = new HttpClient()
        {
            BaseAddress = new System.Uri("http://localhost:57616/")
        };

        public async Task<string> GetMethod()
        {
            using (var response = await _client.GetAsync("api/Slide7/GetMethod")
                                               .ConfigureAwait(false))
            {
                return await response.Content.ReadAsStringAsync()
                                             .ConfigureAwait(false);
            }
        } 
    }

    public class Slide7HttpClientFacade
    {
        private Slide7HttpClient _client = new Slide7HttpClient();

        public async Task<string> GetMethod()
        {
            return await _client.GetMethod();
        }
    }

    public class Slide7Controller : ApiController
    {
        public IHttpActionResult GetMethod()
        {
            return Ok("ConfigureAwait");
        }

        public IHttpActionResult GetResultThroughHttp()
        {
            Slide7HttpClientFacade facade = new Slide7HttpClientFacade();

            string result = facade.GetMethod().Result;
            return Ok(result);
        }
    }
}