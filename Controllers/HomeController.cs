﻿using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RazorMvc.Data;
using RazorMvc.Hubs;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInternshipService intershipService;
        private readonly IHubContext<MessageHub> hubContext;
        private readonly MessageService messageService;

        public HomeController(ILogger<HomeController> logger, IInternshipService intershipService, IHubContext<MessageHub> hubContext, MessageService messageService)
        {
            _logger = logger;
            this.intershipService = intershipService;
            this.hubContext = hubContext;
            this.messageService = messageService;
        }

        public IActionResult Index()
        {
            return View(intershipService.GetMembers());
        }

        public IActionResult Privacy()
        {
            return View(intershipService.GetMembers());
        }

        public IActionResult Chat()
        {
            return View(messageService.GetAllMessages());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            intershipService.RemoveMember(index);
        }

        [HttpGet]
        public Intern AddMember(string memberName)
        {
            Intern intern = new Intern();
            intern.Name = memberName;
            intern.DateOfJoin = DateTime.Now;
            var newMember = intershipService.AddMember(intern);
            hubContext.Clients.All.SendAsync("AddMember", newMember.Name, newMember.Id);
            return newMember;
        }

        [HttpPut]
        public void UpdateMember(int id, string newName)
        {
            Intern intern = new Intern();
            intern.Id = id;
            intern.Name = newName;
            intern.DateOfJoin = DateTime.Now;
            intershipService.UpdateMember(intern);
        }

    }
}
