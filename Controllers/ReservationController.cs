using Lab2MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lab2MVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HttpClient _httpClient;
        private string baseUri = "https://localhost:7032"; 

        public ReservationController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            // Anropa API för att hämta reservationsdata
            var response = await _httpClient.GetAsync($"{baseUri}api/Reservation");
            var json = await response.Content.ReadAsStringAsync();
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(json);

            return View(reservations);
        }
    }
}
