using RazorMvc.Models;
using RazorMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorMvc.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly InternshipClass _internshipClass = new();

        public void RemoveMember(int id)
        {
            var itemToBeDeleted = _internshipClass.Members.Single(_ => _.Id == id);
            _internshipClass.Members.Remove(itemToBeDeleted);
        }

        public Intern AddMember(Intern intern)
        {
            _internshipClass.Members.Add(intern);
            return intern;
        }

        public void UpdateMember(Intern intern)
        {
            _internshipClass.Members[intern.Id] = intern;
        }

        public IList<Intern> GetMembers()
        {
            return _internshipClass.Members;
        }
    }
}