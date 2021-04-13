﻿using Microsoft.AspNetCore.SignalR;
using RazorMvc.Models;
using RazorMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorMvc.Hubs
{
    public class MessageHub : Hub
    {
        private readonly MessageService messageService;
        private readonly IInternshipService internshipService;

        public MessageHub(MessageService messageService, IInternshipService internshipService)
        {
            this.messageService = messageService;
            this.internshipService = internshipService;

        }

        public async Task SendMessage(string user, string message)
        {
            messageService.AddMessage(user, message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
