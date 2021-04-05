﻿using RazorMvc.Models;
using RazorMVC.Data;
using System;
using System.Collections.Generic;

namespace RazorMvc.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly InternshipClass _internshipClass = new();

        public void RemoveMember(int index)
        {
            _internshipClass.Members.RemoveAt(index);
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