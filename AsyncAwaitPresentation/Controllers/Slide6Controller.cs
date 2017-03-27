using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AsyncAwaitPresentation.Controllers
{
    public class Slide6HttpClient
    {
        private HttpClient _client = new HttpClient()
        {
            BaseAddress = new System.Uri("http://localhost:57616/")
        };

        public async Task<string> GetMethod()
        {
            using (var response = await _client.GetAsync("api/Slide6/GetMethod"))
            {
                return await response.Content.ReadAsStringAsync();
            }
        } 
    }

    public class Slide6Controller : ApiController
    {
        public IHttpActionResult GetMethod()
        {
            return Ok("ConfigureAwait");
        }

        public IHttpActionResult GetResultThroughHttp()
        {
            Slide6HttpClient client = new Slide6HttpClient();

            string result = client.GetMethod().Result;
            return Ok(result);
        }
    }
}