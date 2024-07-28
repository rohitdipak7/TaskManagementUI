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
    public class TicketController : Controller
    {
        private readonly string baseUrl = "https://localhost:7000/api/";

        // GET: Ticket
        public async Task<ActionResult> Index()
        {
            List<Ticket> tickets = new List<Ticket>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("ticket");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    tickets = JsonConvert.DeserializeObject<List<Ticket>>(data);
                }
            }
            return View(tickets);
        }

        // GET: Ticket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        public async Task<ActionResult> Create(Ticket ticket)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var content = new StringContent(JsonConvert.SerializeObject(ticket), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("ticket", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(ticket);
        }

        // GET: Ticket/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            Ticket ticket = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await client.GetAsync($"ticket/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    ticket = JsonConvert.DeserializeObject<Ticket>(data);
                }
            }
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Ticket ticket)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var content = new StringContent(JsonConvert.SerializeObject(ticket), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"ticket/{ticket.ID}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(ticket);
        }

        // GET: Ticket/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await client.DeleteAsync($"ticket/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}