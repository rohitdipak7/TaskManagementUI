using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementUI.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsManager { get; set; }
        public bool IsAdmin { get; set; }
        public UserRoles Role { get; set; }
        public Guid? ManagerID { get; set; }
        public Guid? TeamID { get; set; }
        public Team Team { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
    public enum UserRoles
    {
        Employee, Manager, Admin
    }
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }

}