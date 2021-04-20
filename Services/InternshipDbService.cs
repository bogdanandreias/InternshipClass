﻿using Microsoft.Extensions.Configuration;
using RazorMvc.Data;
using RazorMvc.Hubs;
using RazorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorMvc.Services
{
    public class InternshipDbService : IInternshipService
    {
        private readonly InternDbContext db;
        private IConfiguration configuration;
        private Location defaultLocation;

        public InternshipDbService(InternDbContext db)
        {
            this.db = db;
        }
        public Intern AddMember(Intern member)
        {
            if(member.Location == null)
            {
                member.Location = GetDefaultLocation();
            }
            db.Interns.AddRange(member);
            db.SaveChanges();
            return member;
        }

        private Location GetDefaultLocation()
        {
            if (defaultLocation == null)
            {
                var defaultLocationName = configuration["DefaultLocation"];
                defaultLocation = db.Locations.Where(_ => _.Name == defaultLocationName).OrderBy(_ => _.Id).FirstOrDefault();
            }

            return defaultLocation;
        }

        public void UpdateMember(Intern intern)
        {
            var itemToBeUpdated = GetMemberById(intern.Id);
            itemToBeUpdated.Name = intern.Name;
            if (intern.DateOfJoin > DateTime.MinValue)
            {
                itemToBeUpdated.DateOfJoin = DateTime.Now;
            }

            db.Interns.Update(itemToBeUpdated);
            db.SaveChanges();
        }

        public IList<Intern> GetMembers()
        {
            var interns = db.Interns.ToList();
            return interns;
        }

        public void RemoveMember(int id)
        {
            var intern = db.Find<Intern>(id);
            if (intern == null) return;
            db.Remove<Intern>(intern);
            db.SaveChanges();
        }
        public Intern GetMemberById(int id)
        {
            var intern = db.Find<Intern>(id);
            db.Entry(intern).Reference(_ => _.Location).Load();
            return intern;
        }

        public void UpdateLocation(int id, int locationId)
        {
            var intern = db.Find<Intern>(id);
            var location = db.Find<Location>(locationId);
            intern.Location = location;
            db.SaveChanges();
        }
    }
}
