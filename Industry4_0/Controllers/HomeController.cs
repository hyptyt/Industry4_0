using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Industry4_0.Models;
using Industry4_0.Data; 
using Microsoft.EntityFrameworkCore;

namespace Industry4_0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context) { _context = context; }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public async Task<IActionResult> CompaniesAndOrganizations()
        {
            ViewData["Message"] = "Your application description page.";
            var companiesResults = from result in _context.CompaniesFeedback
                             orderby result.Date descending
                             select result;
            return View(await companiesResults.ToListAsync());
        }
        public async Task<IActionResult> EmergingTechnologies()
        {
            var emergingTechnologiesResults = from result in _context.EmergingTechnologiesFeedback
                                   orderby result.Date descending
                                   select result;
  
            return View(await emergingTechnologiesResults.ToListAsync());
        }

        public IActionResult WorldMap()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
