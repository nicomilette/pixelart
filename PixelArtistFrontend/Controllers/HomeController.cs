using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PixelArtist.Models;
using PixelArtist.Extensions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixelArtist.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BackendClient");
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            ViewData["User"] = user;
            return View();
        }

        public IActionResult LoginRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = new User { Username = username, Password = password };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/auth/login", data);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                // You can add more logic to handle the result as needed
                HttpContext.Session.SetObjectAsJson("User", user);
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Login failed";
            return View("LoginRegister");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new User { Username = username, Password = password };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/auth/register", data);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                // You can add more logic to handle the result as needed
                HttpContext.Session.SetObjectAsJson("User", user);
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Registration failed";
            return View("LoginRegister");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("LoginRegister");
        }
    }
}
