using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementUI.Models
{
    public class Team
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }

    }

}