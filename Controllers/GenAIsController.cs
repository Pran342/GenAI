using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GenAITech.Data;
using GenAITech.Models;
using Microsoft.AspNetCore.Authorization;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment; 

namespace GenAITech.Controllers
{
    public class GenAIsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public GenAIsController(ApplicationDbContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        // GET: GenAIs
        public async Task<IActionResult> Index()
        {
              return _context.GenAI != null ? 
                          View(await _context.GenAI.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GenAI'  is null.");
        }

        // GET: GenAIs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GenAI == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAI
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genAI == null)
            {
                return NotFound();
            }

            return View(genAI);
        }

        // GET: GenAIs/Create
        [Authorize(Roles = "admin, user")]
        public IActionResult Create()
        {
            var genAI = new GenAI();
            genAI.AnchorLink = "dfsdgds";
            genAI.ImageFileName = "dgfsdd";
            return View(genAI);
        }

        // POST: GenAIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Create([Bind("Id, GenAIName, AnchorLink, Summary, ImageFileName, Like")] GenAI genAI, Uploadfile uploadFile)
        {
            if (uploadFile.File != null)
            {
                var fileName = Path.GetFileName(uploadFile.File.FileName);
                var filePath = Path.Combine(_hostingEnv.WebRootPath, "images", fileName);
                genAI.ImageFileName = fileName;
                genAI.AnchorLink = genAI.GenAIName;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadFile.File.CopyToAsync(fileStream);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(genAI);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genAI);
        }

        // GET: GenAIs/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GenAI == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAI.FindAsync(id);
            if (genAI == null)
            {
                return NotFound();
            }
            return View(genAI);
        }

        // POST: GenAIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("GenAIName, AnchorLink, Summary, ImageFileName")] GenAI genAI)
        {
            if (id != genAI.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genAI);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenAIExists(genAI.Id))
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
            return View(genAI);
        }

        // GET: GenAIs/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GenAI == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAI
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genAI == null)
            {
                return NotFound();
            }

            return View(genAI);
        }

        // POST: GenAIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GenAI == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GenAI'  is null.");
            }
            var genAI = await _context.GenAI.FindAsync(id);
            if (genAI != null)
            {
                _context.GenAI.Remove(genAI);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenAIExists(int id)
        {
          return (_context.GenAI?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
