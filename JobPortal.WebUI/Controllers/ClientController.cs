﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobPortal.WebUI.Models;
using System.Diagnostics;

namespace JobPortal.WebUI.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult RequestForm()
        {
            return View("PurchaseRequest");
        }

        public IActionResult PurchaseRequestCard() 
        {

            //TODO:  handle PurchaseRequestForm data to store in database
            List<PurchaseRequestForm> list = new List<PurchaseRequestForm>();
            for(int i=0; i<4;i++)
            {
                PurchaseRequestForm req = new PurchaseRequestForm { RequestID = 1, RequestNamer = "Alpha", RequestDescription = "I need a Logo designed by a Vue!", RequestOfferCount = 1, RequestDuration = 2, RequestBudget = 5.0, RequestDate = System.DateTime.Now.Date };
                list.Add(req);

            }
            ViewData["request-view-card"] = list;
            return View("PurchaseRequestCard");

        }

        public void RequestHandler()
        {

            //TODO:  handler for bid invites  

            Console.WriteLine("Handle Stuff");
        }


        public IActionResult PurchaseRequest()
        {
            //TODO:  handle PurchaseRequestForm data to store in database
            List<PurchaseRequestForm> list = new List<PurchaseRequestForm>();
           PurchaseRequestForm  req = new PurchaseRequestForm { RequestID = 1, RequestNamer = "Alpha", RequestDescription = "I need a Logo designed by a Vue!", RequestOfferCount = 1, RequestDuration = 2, RequestBudget = 5.0, RequestDate = System.DateTime.Now.Date , RequestCategory ="Design" };
            list.Add(req);

            ViewData["purchase-request"] = list;
            return View("PurchaseRequestView");
            
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
