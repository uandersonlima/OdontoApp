﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OdontoApp.Controllers
{
    public class PerguntasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}