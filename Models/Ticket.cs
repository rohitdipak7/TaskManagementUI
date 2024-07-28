using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementUI.Models
{
    public class Ticket
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DueDate { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
    public enum TicketPriority
    {
        None,
        Trivial,
        Minor,
        Major,
        Critical,
        Blocker
    }
    public enum TicketStatus
    {
        Backlog,
        ToDo,
        InProgress,
        InReview,
        QA,
        UAT,
        Done
    }

    public class Document
    {
        public Guid ID { get; set; }
        public Guid TicketID { get; set; }
        //public Ticket Ticket { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }

    public class Note
    {
        public Guid ID { get; set; }
        public Guid TicketID { get; set; }
        //public Ticket Ticket { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}