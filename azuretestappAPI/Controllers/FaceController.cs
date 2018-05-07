using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using azuretestappAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace azuretestappAPI.Controllers
{
    [Route("api/[controller]")]
    public class FaceController : Controller
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromQuery] string query, [FromBody]VisionParameter visionParameter)
        {
            const string baseUrl = "https://westeurope.api.cognitive.microsoft.com/";
            const string key = "25eb90a2564c40f58c156c877c44ea82";

            var httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(visionParameter);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"face/v1.0/detect?{query}";

            var response = httpClient.PostAsync(url, stringContent).Result;

            var content = response.Content.ReadAsStringAsync().Result;
            return Ok(JObject.Parse(content));
        }
    }
}
