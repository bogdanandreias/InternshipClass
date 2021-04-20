using System.Collections.Generic;
using RazorMvc.Hubs;
using RazorMvc.Models;

namespace RazorMvc.Services
{
    public interface IInternshipService
    {
        Intern AddMember(Intern intern);

        IList<Intern> GetMembers();

        void RemoveMember(int index);

        void UpdateMember(Intern intern);

        Intern GetMemberById(int id);

        void UpdateLocation(int id, int locationId);
    }
}