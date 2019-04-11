using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Industry4_0.Data;
using Industry4_0.Models;
using Microsoft.AspNetCore.Authorization;

namespace Industry4_0.Models
{
    public class CompaniesFeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompaniesFeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CompaniesFeedbacks
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompaniesFeedback.ToListAsync());
        }

        // GET: CompaniesFeedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companiesFeedback = await _context.CompaniesFeedback
                .SingleOrDefaultAsync(m => m.ID == id);
            if (companiesFeedback == null)
            {
                return NotFound();
            }

            return View(companiesFeedback);
        }

        [Authorize(Roles = "Administrator, User")]
        // GET: CompaniesFeedbacks/Create
        public IActionResult Create()
        {
            CompaniesFeedback co = new CompaniesFeedback();
            co.Date = DateTime.Now;
            co.Username = User.Identity.Name;
            return View(co);
        }

        // POST: CompaniesFeedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, User")]
        public async Task<IActionResult> Create([Bind("ID,Date,Username,Heading,Companies,Rating,Feedback,Agree,Disagree")] CompaniesFeedback companiesFeedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companiesFeedback);
                await _context.SaveChangesAsync();
                return RedirectToAction("CompaniesAndOrganizations", "Home", "feedback");
                    
            }
            return RedirectToAction("CompaniesAndOrganizations", "Home", "feedback");
        }

        // GET: CompaniesFeedbacks/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companiesFeedback = await _context.CompaniesFeedback.SingleOrDefaultAsync(m => m.ID == id);
            if (companiesFeedback == null)
            {
                return NotFound();
            }
            return View(companiesFeedback);
        }

        // POST: CompaniesFeedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,Username,Heading,Companies,Rating,Feedback,Agree,Disagree")] CompaniesFeedback companiesFeedback)
        {
            if (id != companiesFeedback.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companiesFeedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompaniesFeedbackExists(companiesFeedback.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CompaniesAndOrganizations", "Home", "feedback");
            }
            return RedirectToAction("CompaniesAndOrganizations", "Home", "feedback");
        }

        // GET: CompaniesFeedbacks/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companiesFeedback = await _context.CompaniesFeedback
                .SingleOrDefaultAsync(m => m.ID == id);
            if (companiesFeedback == null)
            {
                return NotFound();
            }

            return View(companiesFeedback);
        }

        // POST: CompaniesFeedbacks/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companiesFeedback = await _context.CompaniesFeedback.SingleOrDefaultAsync(m => m.ID == id);
            _context.CompaniesFeedback.Remove(companiesFeedback);
            await _context.SaveChangesAsync();
            return RedirectToAction("CompaniesAndOrganizations", "Home", "feedback");
        }

        private bool CompaniesFeedbackExists(int id)
        {
            return _context.CompaniesFeedback.Any(e => e.ID == id);
        }

        public async Task<IActionResult> Agree(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies =
            await _context.CompaniesFeedback.SingleOrDefaultAsync(m => m.ID == id);

            if (companies == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        if (!companies.AgreeAvail)
                        {
                            companies.Agree = companies.Agree + 1;
                            companies.AgreeAvail = true;
                        }
                        _context.Update(companies);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompaniesFeedbackExists(companies.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Redirect("~/Home/CompaniesAndOrganizations/#feedback");
        }
        public async Task<IActionResult> Disagree(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var companies =
            await _context.CompaniesFeedback.SingleOrDefaultAsync(m => m.ID == id);

            if (companies == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        if (!companies.DisagreeAvail)
                        {
                            companies.Disagree = companies.Disagree + 1;
                            companies.DisagreeAvail = true;
                        }
                        _context.Update(companies);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompaniesFeedbackExists(companies.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Redirect("~/Home/CompaniesAndOrganizations/#feedback");
        }
    }
}
