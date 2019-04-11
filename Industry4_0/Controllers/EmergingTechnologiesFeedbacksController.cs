using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Industry4_0.Data;
using Industry4_0.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Industry4_0.Controllers
{
    public class EmergingTechnologiesFeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
       
        public EmergingTechnologiesFeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        // GET: EmergingTechnologiesFeedbacks
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmergingTechnologiesFeedback.ToListAsync());
        }

        // GET: EmergingTechnologiesFeedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emergingTechnologiesFeedback = await _context.EmergingTechnologiesFeedback
                .SingleOrDefaultAsync(m => m.ID == id);
            if (emergingTechnologiesFeedback == null)
            {
                return NotFound();
            }

            return View(emergingTechnologiesFeedback);
        }

        // GET: EmergingTechnologiesFeedbacks/Create
        [Authorize(Roles ="Administrator, User")]
        public IActionResult Create()
        {
            EmergingTechnologiesFeedback et = new EmergingTechnologiesFeedback();
            et.Date = DateTime.Now;
            et.Username = User.Identity.Name;
    
            return View(et);
        }

        // POST: EmergingTechnologiesFeedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, User")]
        public async Task<IActionResult> Create([Bind("ID,Date,Username,Heading,EmergingTechnologies,Rating,Feedback,Agree,Disagree")] EmergingTechnologiesFeedback emergingTechnologiesFeedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emergingTechnologiesFeedback);
                await _context.SaveChangesAsync();
                return RedirectToAction("EmergingTechnologies", "Home", "feedback");
            }
            return RedirectToAction("EmergingTechnologies", "Home", "feedback");
        }

        // GET: EmergingTechnologiesFeedbacks/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emergingTechnologiesFeedback = await _context.EmergingTechnologiesFeedback.SingleOrDefaultAsync(m => m.ID == id);
            if (emergingTechnologiesFeedback == null)
            {
                return NotFound();
            }
            return View(emergingTechnologiesFeedback);
        }

        // POST: EmergingTechnologiesFeedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,Username,Heading,EmergingTechnologies,Rating,Feedback,Agree,Disagree")] EmergingTechnologiesFeedback emergingTechnologiesFeedback)
        {
            if (id != emergingTechnologiesFeedback.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emergingTechnologiesFeedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmergingTechnologiesFeedbackExists(emergingTechnologiesFeedback.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("EmergingTechnologies", "Home", "feedback");
            }
            return RedirectToAction("EmergingTechnologies", "Home", "feedback");
        }

        // GET: EmergingTechnologiesFeedbacks/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emergingTechnologiesFeedback = await _context.EmergingTechnologiesFeedback
                .SingleOrDefaultAsync(m => m.ID == id);
            if (emergingTechnologiesFeedback == null)
            {
                return NotFound();
            }

            return View(emergingTechnologiesFeedback);
        }

        // POST: EmergingTechnologiesFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emergingTechnologiesFeedback = await _context.EmergingTechnologiesFeedback.SingleOrDefaultAsync(m => m.ID == id);
            _context.EmergingTechnologiesFeedback.Remove(emergingTechnologiesFeedback);
            await _context.SaveChangesAsync();
            return RedirectToAction("EmergingTechnologies", "Home", "feedback");
        }

        private bool EmergingTechnologiesFeedbackExists(int id)
        {
            return _context.EmergingTechnologiesFeedback.Any(e => e.ID == id);
        }
        [Authorize(Roles = "Administrator, User")]
        public async Task<IActionResult> Agree(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emergingTechnologies =
            await _context.EmergingTechnologiesFeedback.SingleOrDefaultAsync(m => m.ID == id);

            if (emergingTechnologies == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        if (!emergingTechnologies.AgreeAvail)
                    {
                            emergingTechnologies.Agree = emergingTechnologies.Agree + 1;
                            emergingTechnologies.AgreeAvail = true;
                        }
                        _context.Update(emergingTechnologies);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmergingTechnologiesFeedbackExists(emergingTechnologies.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Redirect("~/Home/EmergingTechnologies/#feedback");
        }
        [Authorize(Roles = "Administrator, User")]
        public async Task<IActionResult> Disagree(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emergingTechnologies =
            await _context.EmergingTechnologiesFeedback.SingleOrDefaultAsync(m => m.ID == id);

            if (emergingTechnologies == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        if (!emergingTechnologies.DisagreeAvail)
                        {
                            emergingTechnologies.Disagree = emergingTechnologies.Disagree + 1;
                            emergingTechnologies.DisagreeAvail = true;
                        }
                    }
                    _context.Update(emergingTechnologies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmergingTechnologiesFeedbackExists(emergingTechnologies.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Redirect("~/Home/EmergingTechnologies/#feedback");
        }
    }

}
