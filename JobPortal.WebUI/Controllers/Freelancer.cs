﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using static JobPortal.WebUI.Models.Freelancer;
using JobPortal.WebUI.Models;

namespace JobPortal.WebUI.Controllers
{
    public class Freelancer : Controller
    {

        private readonly ILogger<Freelancer> _logger;

        public Freelancer(ILogger<Freelancer> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult BecomeFreelancer()
        {
            List<Skill> skillList = new List<Skill>();
            skillList.Add(new Skill { Name = "Web Programming" });
            skillList.Add(new Skill { Name = "Data Entry" });
            skillList.Add(new Skill { Name = "Data Base" });
            skillList.Add(new Skill { Name = "Web Designing" });
            skillList.Add(new Skill { Name = "Desktop Application" });
            ViewData["Skill-List"] = skillList;
            Models.Freelancer freelancer = new Models.Freelancer();
            freelancer.UserSkill.Add(new Skill());
            return View(freelancer);
        }
        [HttpPost]
        public IActionResult BecomeFreelancer(Models.Freelancer f)
        {
            List<Skill> skillList = new List<Skill>();
            skillList.Add(new Skill { Name = "Web Programming" });
            skillList.Add(new Skill { Name = "Data Entry" });
            skillList.Add(new Skill { Name = "Data Base" });
            skillList.Add(new Skill { Name = "Web Designing" });
            skillList.Add(new Skill { Name = "Desktop Application" });
            ViewData["Skill-List"] = skillList;
            return View(f);
        }
        public IActionResult MyOrders()
        {
            List<Order> orders = new List<Order>();
            Order dummyOrder = new Order();
            dummyOrder.ID = 1;
            dummyOrder.StartDate = DateTime.Now;
            dummyOrder.Status = "Running";
            dummyOrder.ClientID = 1;
            dummyOrder.FreelancerID = 2;
            dummyOrder.GigID = 2;
            dummyOrder.freelancer = new User { UserName = "kashif" };
            dummyOrder.client = new User { UserName = "atif" };
            dummyOrder.gig = new GIG { Title = "This is Gig Title" };
            orders.Add(dummyOrder);
            ViewData["Order-List"]=orders;
            return View();
        }
        public IActionResult Gigs()
        {
            List<GIG> giglist = new List<GIG>();
            giglist.Add(new GIG {Title="Web Programming", Description = "Lorem ipsum dolor sit amet, consectetuer ad", Pricing=5});
            giglist.Add(new GIG{Title="Desktop Application",  Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit", Pricing=10});
            giglist.Add(new GIG {Title="Mobile Computing", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.",Pricing=20});
            ViewData["Gig-List"] = giglist;
            return View("MyGigs");
        }
    }
}
