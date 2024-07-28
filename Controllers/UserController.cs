using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskManagementUI.Models;

namespace TaskManagementUI.Controllers
{
    public class UserController : Controller
    {
        private readonly string baseUrl = "https://localhost:7000/api/";

        // GET: User
        public async Task<ActionResult> Index()
        {
            List<User> users = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("user");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(data);
                }
            }
            return View(users);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return PartialView("_CreateOrEdit", new Ticket());
        }

        // POST: User/Create
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("user", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        // GET:
        public async Task<ActionResult> Edit(Guid id)
        {
            User user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await client.GetAsync($"user/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(data);
                }
            }
            return PartialView("_CreateOrEdit", user);
        }

        // POST:
        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"user/{user.ID}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await client.DeleteAsync($"user/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}