using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Learn1.Models;

namespace Learn1.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IA_DB_1Context _context;

        public ProjectsController(IA_DB_1Context context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
				
            var iA_DB_1Context = _context.Project.Include(p => p.OrganizationNavigation).Include(p => p.UserNavigation);
            return View(await iA_DB_1Context.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.OrganizationNavigation)
                .Include(p => p.UserNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["Organization"] = new SelectList(_context.Organization, "Id", "Address1");
            ViewData["User"] = new SelectList(_context.User, "Id", "Address1");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User,Organization,CreateDate,Type,Desc,Comment,Industry")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Organization"] = new SelectList(_context.Organization, "Id", "Address1", project.Organization);
            ViewData["User"] = new SelectList(_context.User, "Id", "Address1", project.User);
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["Organization"] = new SelectList(_context.Organization, "Id", "Address1", project.Organization);
            ViewData["User"] = new SelectList(_context.User, "Id", "Address1", project.User);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,User,Organization,CreateDate,Type,Desc,Comment,Industry")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Organization"] = new SelectList(_context.Organization, "Id", "Address1", project.Organization);
            ViewData["User"] = new SelectList(_context.User, "Id", "Address1", project.User);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.OrganizationNavigation)
                .Include(p => p.UserNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(decimal id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
