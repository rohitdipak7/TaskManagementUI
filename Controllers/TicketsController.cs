using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskManagementUI.Helper;
using TaskManagementUI.Models;

namespace TaskManagementUI.Controllers
{
    public class TicketsController : Controller
    {
        private readonly HttpClient _httpClient;

        public TicketsController()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7000/api/") };
        }

        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("ticket");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var tickets = JsonConvert.DeserializeObject<List<Ticket>>(jsonString);
                return View(tickets);
            }
            return View();
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var response = await _httpClient.GetAsync($"ticket/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var tickets = JsonConvert.DeserializeObject<List<Ticket>>(jsonString);
                return View(tickets);
            }
            return View();
        }

        public ActionResult Create()
        {
            return PartialView("_CreateOrEdit", new Ticket());
        }


        [HttpPost]
        public async Task<ActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(ticket);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"ticket", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var response = await _httpClient.GetAsync($"ticket/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var ticket = JsonConvert.DeserializeObject<Ticket>(jsonString);
                return PartialView("_CreateOrEdit", ticket);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(ticket);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"ticket/{ticket.ID}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"ticket/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"ticket/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }


}