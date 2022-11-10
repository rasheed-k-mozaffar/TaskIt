using System;
namespace TaskIt.Web.Models
{
    public class TaskItem
    {
        public long Id { get; set; }
        public string TaskName { get; set; }
        public bool IsComplete { get; set; }
        public Todo Todo { get; set; }
    }
}


