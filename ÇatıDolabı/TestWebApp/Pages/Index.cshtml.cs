﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TestWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var a = Url.Action("AnotherOperation", "TestOperation");
            var v = Url.RouteUrl("TestOperation/AnotherOperation");
            var c = Request.Path.ToString();
            var d = Request.PathBase.ToString();
        }
    }
}
