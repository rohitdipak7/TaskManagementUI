using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementUI.Models
{
    public class ReportViewModel
    {
        public List<Ticket> TicketsDueInNextWeek { get; set; }
        public List<Ticket> CompletedTickets { get; set; }
        public List<TeamPerformance> TeamPerformance { get; set; }

    }
    public class TeamPerformance
    {
        public Guid TeamID { get; set; }
        public Team Team { get; set; }
        public string TeamName { get; set; }
        public int CompletedTasks { get; set; }
    }
}
