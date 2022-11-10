using System;
using System.ComponentModel.DataAnnotations;

namespace TaskIt.Web.Models
{
    public class Todo
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Owner { get; set; }
        public bool IsComplete { get; set; } = false;



    }
}

