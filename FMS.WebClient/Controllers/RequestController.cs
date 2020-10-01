using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FMS.WebClient.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult NewCustomerRequest()
        {
            return View();
        }
    }
}