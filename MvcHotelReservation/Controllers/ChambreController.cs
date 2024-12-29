using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcHotelReservation.Data;
using MvcHotelReservation.Models;

namespace MvcHotelReservation.Controllers
{
    public class ChambreController : Controller
    {
        private  ApplicationDbContext _context;

        public ChambreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chambre
        [HttpPost]
        public async Task<IActionResult> Index(DateTime checkin, DateTime checkout,int capacity)
        {
            TempData["checkin"] = checkin;
            TempData["checkout"] = checkout;
            HttpContext.Session.SetString("checkin", checkin.ToShortDateString());
            HttpContext.Session.SetString("checkinTime", checkin.ToShortTimeString());
            HttpContext.Session.SetString("checkout", checkout.ToShortDateString());
            HttpContext.Session.SetString("checkoutTime", checkin.ToShortTimeString());
            int daysDifference = (checkout - checkin).Days;
            ViewData["daysDifference"] = daysDifference;
                HttpContext.Session.SetInt32("daysDifference",daysDifference);
            var availableRooms = await _context.Chambres
                .Where(c => c.Disponibilite == true && c.Capacite>=capacity).OrderBy(PrixParNuit => PrixParNuit)
                .ToListAsync();
            return View(availableRooms);

           
        }
        
    
        // GET: Chambre/Details/5
        [Route("Chambre/Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chambre = await _context.Chambres
                .FirstOrDefaultAsync(m => m.IdChambre == id);
            if (chambre == null)
            {
                return NotFound();
            }

            return View(chambre);
        }

        // GET: Chambre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chambre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdChambre,NumeroChambre,TypeChambre,Capacite,PrixParNuit,Description,Disponibilite")] Chambre chambre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chambre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chambre);
        }

        // GET: Chambre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chambre = await _context.Chambres.FindAsync(id);
            if (chambre == null)
            {
                return NotFound();
            }
            return View(chambre);
        }

        // POST: Chambre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdChambre,NumeroChambre,TypeChambre,Capacite,PrixParNuit,Description,Disponibilite")] Chambre chambre)
        {
            if (id != chambre.IdChambre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chambre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChambreExists(chambre.IdChambre))
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
            return View(chambre);
        }

        // GET: Chambre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chambre = await _context.Chambres
                .FirstOrDefaultAsync(m => m.IdChambre == id);
            if (chambre == null)
            {
                return NotFound();
            }

            return View(chambre);
        }

        // POST: Chambre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chambre = await _context.Chambres.FindAsync(id);
            if (chambre != null)
            {
                _context.Chambres.Remove(chambre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChambreExists(int id)
        {
            return _context.Chambres.Any(e => e.IdChambre == id);
        }
    }
}
