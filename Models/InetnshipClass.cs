using System;
using System.Collections.Generic;

namespace RazorMvc.Models
{
    public class InternshipClass
    {
        private readonly List<Intern> _members;

        public InternshipClass()
        {
            _members = new List<Intern>
            {
                new Intern { Name = "Borys", DateOfJoin = DateTime.Parse("2021-04-01") },
                new Intern { Name = "Liova", DateOfJoin = DateTime.Parse("2021-04-01") },
                new Intern { Name = "Orest", DateOfJoin = DateTime.Parse("2021-03-31") },
            };
        }

        public IList<Intern> Members
        {
            get { return _members; }
        }
    }
}