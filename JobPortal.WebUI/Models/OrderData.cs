﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Models
{
    public class OrderData
    {
        public int ID { get; set; }
        public Order order { get; set; }
        public int OrderID { get; set; }
        public String Details { get; set; }
        public String FileURL { get; set; }
        public IFormFile File { get; set; }
        public DateTime Date;
    }
}