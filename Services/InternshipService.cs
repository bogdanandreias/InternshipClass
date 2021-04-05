using RazorMvc.Models;
using RazorMVC.Data;
using System;
using System.Collections.Generic;

namespace RazorMvc.Services
{
    public class InternshipService
    {
        private readonly InternshipClass _internshipClass = new();
        
        public void RemoveMember(int index)
        {
            _internshipClass.Members.RemoveAt(index);
        }

        public int AddMember(Intern intern)
        {
            _internshipClass.Members.Add(intern);
            return intern.Id;
        }

        public InternshipClass GetClass()
        {
            return _internshipClass;
        }

        internal void UpdateMember(Intern intern)
        {
            
        }
    }
}