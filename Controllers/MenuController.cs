using Lab2MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lab2MVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly HttpClient _httpClient;
        private string baseUri = "https://localhost:7032/"; 

        public MenuController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            // Anropa API för att hämta menydata
            var response = await _httpClient.GetAsync($"{baseUri}api/MenuItem");
            var json = await response.Content.ReadAsStringAsync();
            var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(json);

            return View(menuItems); // Skicka data till vyn
        }
    }
}
