using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskManagementUI.Models;
using Newtonsoft.Json;

namespace TaskManagementUI.Controllers
{
    public class ReportsController : Controller
    {
        private readonly string baseUrl = "https://localhost:7000/api/";
        private readonly HttpClient _httpClient;

        public ReportsController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            var viewModel = new ReportViewModel();

            // Fetch tickets which are due in the next week
            HttpResponseMessage response = await _httpClient.GetAsync("reports/due-in-next/7");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                viewModel.TicketsDueInNextWeek = JsonConvert.DeserializeObject<List<Ticket>>(jsonString);
            }

            var startDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-ddTHH:mm:ss");
            var endDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            // Fetch completed tickets in the last month
            response = await _httpClient.GetAsync($"reports/completed-tickets?startDate={startDate}&endDate={endDate}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                viewModel.CompletedTickets = JsonConvert.DeserializeObject<List<Ticket>>(jsonString);
            }

            // Fetch team performance
            response = await _httpClient.GetAsync($"reports/team-performance?startDate={startDate}&endDate={endDate}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                viewModel.TeamPerformance = JsonConvert.DeserializeObject<List<TeamPerformance>>(jsonString);
            }

            return View(viewModel);
        }
    }

}