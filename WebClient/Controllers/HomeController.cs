using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json.Serialization;
using IdentityModel.Client;
using Newtonsoft.Json;
using WebClient.Models;
using WebClient.Service;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenService _tokenService;

        public HomeController(ILogger<HomeController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Weather()
        {
            var token = await _tokenService.GetToken("myApi.read");
            using var client = new HttpClient();
            client.SetBearerToken(token.AccessToken);
            var response = await client.GetAsync("https://localhost:7267/weatherforecast");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weather = JsonConvert.DeserializeObject<List<WeatherModel>>(content);
                return View(weather);
            }

            throw new Exception("Failed to get Data from API");
        }
    }
}