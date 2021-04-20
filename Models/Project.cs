using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RazorMvc.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsPublished { get; set; }

        public string Url { get; set; }

        [JsonIgnore]
        public ICollection<Intern> Interns { get; set; }
    }
}
