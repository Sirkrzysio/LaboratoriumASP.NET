using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Context;
using WebApplication2.Entities;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class UniversityController : Controller
    {
        private readonly UniversityDB _context;

        public UniversityController(UniversityDB context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("University/UniversityRankingDetails/{universityId}/{rankingSystemId}")]
        public async Task<IActionResult> UniversityRankingDetails(int universityId, int rankingSystemId)
        {
            // Pobierz szczegóły notowania dla danej uczelni i systemu rankingowego
            var rankingDetails = await _context.UniversityRankingYears
                .Include(ury => ury.RankingCriteria)
                .ThenInclude(rc => rc.RankingSystem)
                .Where(ury => ury.UniversityId == universityId && ury.RankingCriteria.RankingSystemId == rankingSystemId)
                .ToListAsync();

            if (!rankingDetails.Any())
            {
                return NotFound("No ranking details found for the specified university and ranking system.");
            }

            // Przekaż dane do widoku
            ViewData["University"] = await _context.Universities
                .Where(u => u.Id == universityId)
                .Select(u => u.UniversityName)
                .FirstOrDefaultAsync();

            ViewData["RankingSystem"] = await _context.RankingSystems
                .Where(rs => rs.Id == rankingSystemId)
                .Select(rs => rs.SystemName)
                .FirstOrDefaultAsync();

            return View(rankingDetails);
        }
        
        // GET: University
        public async Task<IActionResult> Index(int page = 1, int size = 10)
        {
            var totalRecords = await _context.Universities.CountAsync();
            var universities = await _context
                .Universities
                .Include(u => u.Country)
                .OrderBy(e => e.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToListAsync();
            
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalRecords / (double)size);
            ViewBag.PageSize = size;
            
            
            return View(universities);
        }

        // GET: University/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.Universities
                .Include(u => u.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // GET: University/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id");
            return View();
        }

        // POST: University/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CountryId,UniversityName")] University university)
        {
            if (ModelState.IsValid)
            {
                _context.Add(university);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", university.CountryId);
            return View(university);
        }

        // GET: University/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.Universities.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", university.CountryId);
            return View(university);
        }

        // POST: University/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CountryId,UniversityName")] University university)
        {
            if (id != university.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(university);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversityExists(university.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", university.CountryId);
            return View(university);
        }

        // GET: University/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.Universities
                .Include(u => u.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // POST: University/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var university = await _context.Universities.FindAsync(id);
            if (university != null)
            {
                _context.Universities.Remove(university);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversityExists(int id)
        {
            return _context.Universities.Any(e => e.Id == id);
        }
        
        // GET: University/AddRanking
        public IActionResult AddRanking()
        {
            ViewData["RankingSystemId"] = new SelectList(_context.RankingSystems, "Id", "SystemName");
            ViewData["RankingCriteriaId"] = new SelectList(_context.RankingCriteria, "Id", "CriteriaName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRanking([Bind("Year,RankingSystemId,Score,RankingCriteriaId,UniversityId")] UniversityRankingYear ranking)
        {
            if (ranking == null)
            {
                return BadRequest("Ranking data is null.");
            }

            if (ModelState.IsValid)
            {
                if (_context == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Database context is not available.");
                }

                _context.Add(ranking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            if (_context == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database context is not available.");
            }

            ViewData["RankingSystemId"] = new SelectList(_context.RankingSystems, "Id", "SystemName", ranking.RankingSystemId);
            ViewData["RankingCriteriaId"] = new SelectList(_context.RankingCriteria, "Id", "CriteriaName", ranking.RankingCriteriaId);
            return RedirectToAction("Index");
        }
        //test
    }
}