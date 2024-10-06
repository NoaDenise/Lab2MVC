using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Lab2MVC.Models;
using System.Text;

namespace Lab2MVC.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private string baseUri = "https://localhost:7032/";

        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [Route("ManageMenu")]
        public async Task<IActionResult> ManageMenu()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _httpClient.GetAsync($"{baseUri}api/MenuItem");
            var json = await response.Content.ReadAsStringAsync();
            var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(json);

            return View(menuItems);
        }

        [Route("AddMenuItem")]
        public IActionResult AddMenuItem()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        [Route("AddMenuItem")]
        public async Task<IActionResult> AddMenuItem(MenuItem newMenuItem)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(newMenuItem);
            }

            var json = JsonConvert.SerializeObject(newMenuItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{baseUri}api/MenuItem", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ManageMenu");
            }

            return View(newMenuItem);
        }

        [Route("EditMenuItem/{id}")]
        public async Task<IActionResult> EditMenuItem(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _httpClient.GetAsync($"{baseUri}api/MenuItem/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var menuItem = JsonConvert.DeserializeObject<MenuItem>(json);

            return View(menuItem);
        }

        [HttpPost]
        [Route("EditMenuItem/{id}")]
        public async Task<IActionResult> EditMenuItem(MenuItem menuItem)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(menuItem);
            }

            var json = JsonConvert.SerializeObject(menuItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{baseUri}api/MenuItem/{menuItem.MenuItemId}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ManageMenu");
            }

            return View(menuItem);
        }

        [Route("DeleteMenuItem/{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _httpClient.DeleteAsync($"{baseUri}api/MenuItem/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ManageMenu");
            }

            return View("ManageMenu");
        }

        [Route("ManageReservations")]
        public async Task<IActionResult> ManageReservations()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _httpClient.GetAsync($"{baseUri}api/Reservation");
            var json = await response.Content.ReadAsStringAsync();
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(json);

            return View(reservations);
        }

        // EditReservation Methods
        [Route("EditReservation/{id}")]
        public async Task<IActionResult> EditReservation(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _httpClient.GetAsync($"{baseUri}api/Reservation/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var reservation = JsonConvert.DeserializeObject<Reservation>(json);
                return View(reservation);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("EditReservation/{id}")]
        public async Task<IActionResult> EditReservation(Reservation reservation)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(reservation);
            }

            var json = JsonConvert.SerializeObject(reservation);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{baseUri}api/Reservation/{reservation.ReservationId}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ManageReservations");
            }

            return View(reservation);
        }

        // DeleteReservation Methods
        [Route("DeleteReservation/{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _httpClient.DeleteAsync($"{baseUri}api/Reservation/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ManageReservations");
            }

            return View("ManageReservations");
        }

        [Route("AddReservation")]
        public IActionResult AddReservation()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        [Route("AddReservation")]
        public async Task<IActionResult> AddReservation(Reservation newReservation)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(newReservation);
            }

            var json = JsonConvert.SerializeObject(newReservation);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{baseUri}api/Reservation", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ManageReservations"); // Omdirigera tillbaka till hantera bokningar
            }

            return View(newReservation);
        }

    }
}
