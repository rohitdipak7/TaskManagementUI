using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly string baseUrl = "https://localhost:7000/api/";
        private readonly HttpClient _httpClient;

        public AccountController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            var loginData = new { Username = username, Password = password };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("login", content);

            if (response.IsSuccessStatusCode)
            {
                // Handle successful login
                return RedirectToAction("Index", "Home"); // Redirect to a different page or dashboard
            }
            else
            {
                // Handle failed login
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }
        }
    }
}