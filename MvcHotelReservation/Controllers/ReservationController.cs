using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcHotelReservation.Data;
using MvcHotelReservation.Models;
using MvcHotelReservation.service;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
namespace MvcHotelReservation.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public ReservationController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            var utilisateur = _context.Utilisateurs
                .FirstOrDefault(u => u.idUtilisateur == HttpContext.Session.GetInt32("idClient"));
            var reservations = await _context.Reservations.Include(r => r.Chambre)
                .Where(r => r.Utilisateur.idUtilisateur == utilisateur.idUtilisateur).OrderByDescending(r => r.DateReservation)
                .ToListAsync(); 
            return View(reservations);
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.IdReservation == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Create()
        {
            DateTime dateDebut = DateTime.Parse(@HttpContext.Session.GetString("checkin"));
            DateTime dateFin = DateTime.Parse(@HttpContext.Session.GetString("checkout"));
            int? montant = @HttpContext.Session.GetInt32("montant");
            Chambre chambre = _context.Chambres
                .FirstOrDefault(u => u.IdChambre == HttpContext.Session.GetInt32("idChambre"));
            var utilisateur = _context.Utilisateurs
                .FirstOrDefault(u => u.idUtilisateur == HttpContext.Session.GetInt32("idClient"));
            if (dateDebut > dateFin || dateDebut<DateTime.Now )
            {
                return BadRequest("checkin are reversed or invalid");
            }
            if (ModelState.IsValid)
            {
                var newReservation = new Reservation
                {

                    DateDebut = dateDebut,
                    DateFin = dateFin,
                    MontantTotal = montant ?? 0,
                    Utilisateur = utilisateur,
                    Chambre = chambre,
                };
                await _context.Reservations.AddAsync(newReservation);
                
                _context.Add(newReservation);
                
                await _context.SaveChangesAsync();
            }

                await ReservationChambre(HttpContext.Session.GetInt32("idChambre"));
                var pdfContent = GenerateBookingPdf(HttpContext.Session.GetString("FullName"), chambre.NumeroChambre,dateDebut,dateFin,montant??0);
            
            await _emailService.SendEmailAsync(HttpContext.Session.GetString("email"), "Confirmation de votre réservation", $"Votre réservation a été confirmée pour la chambre {chambre.NumeroChambre} du {dateDebut.ToShortDateString()} au {dateFin.ToShortDateString()}.\nMontant total: {montant} Dirhams",pdfContent,"Receipt.pdf"
             );
            
            return RedirectToAction("Index", "Home");
        }
        public byte[] GenerateBookingPdf(string customerName, string roomNumber, DateTime checkInDate, DateTime checkOutDate, int totalAmount)
        {
            using (var memoryStream = new MemoryStream())
            {
                var document = new PdfDocument(); 
                var page = document.AddPage();
                var graphics = XGraphics.FromPdfPage(page); 
                var font = new XFont("Verdana", 12, XFontStyle.Regular); 
                var fontTitle = new XFont("Verdana", 18, XFontStyle.Bold);
                var fontHeader = new XFont("Verdana", 12, XFontStyle.Bold);
                var fontBody = new XFont("Verdana", 10, XFontStyle.Regular);
                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Marriott-International-Logo-500x281.png");
                XImage logo = XImage.FromFile(logoPath);
                string QrPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/pdt-App-Hero-QRCode-EN1541865-170785521063833.png");
                XImage Qrcode = XImage.FromFile(QrPath);
                double xPosition = (page.Width.Point - 150) / 2;
                int yPosition = 80;
                graphics.DrawImage(Qrcode, 360, yPosition+200, 230, 160);
                graphics.DrawImage(logo, xPosition, 20, 130, 100);
                yPosition += (int)100 + 20;
                graphics.DrawString("Marriott International", fontTitle, XBrushes.Black, new XPoint(40, yPosition));
                yPosition += 40;
                graphics.DrawString("Booking Receipt", fontHeader, XBrushes.Black, new XPoint(40, yPosition));
                yPosition += 20;
                graphics.DrawString($"Receipt #: {Guid.NewGuid()}", fontBody, XBrushes.Black, new XPoint(40, yPosition));
                yPosition += 20;
                graphics.DrawString($"Date: {DateTime.Now.ToShortDateString()}", fontBody, XBrushes.Black, new XPoint(40, yPosition));
                yPosition += 40;
                graphics.DrawString($"Dear {customerName},", font, XBrushes.Black, new XPoint(40, yPosition));
                yPosition += 30;
                graphics.DrawString($"Thank you for your reservation.", font, XBrushes.Black, new XPoint(40, yPosition));
                yPosition += 30;
                graphics.DrawString($"Room Number: {roomNumber}", font, XBrushes.Black, new XPoint(40, yPosition));
                yPosition += 30;
                graphics.DrawString($"Check-in: {checkInDate.ToShortDateString()}", font, XBrushes.Black, new XPoint(40, yPosition));
                yPosition += 30;
                graphics.DrawString($"Check-out: {checkOutDate.ToShortDateString()}", font, XBrushes.Black, new XPoint(40, yPosition));
                yPosition += 30;
                graphics.DrawString($"Total Amount: {totalAmount} Dirhams", font, XBrushes.Black, new XPoint(40, yPosition));

                double infoStartY =page.Height.Point - 120;
                string hotelName = "Marriot International";
                string phone = "+212 556 7890";
                string email = "mariotte@hotelreservation.com";
                string location = " Av Moulay Hassan, Marrackech, Morocco";

                XFont infoFont = new XFont("Verdana", 12, XFontStyle.Regular);
                XFont nameFont = new XFont("Verdana", 14, XFontStyle.Bold);

                graphics.DrawString(hotelName, nameFont, XBrushes.Black, 
                    new XRect(0, infoStartY, page.Width.Point, 20), XStringFormats.TopCenter);
                infoStartY += 20;

                graphics.DrawString($"Phone: {phone}", infoFont, XBrushes.Black, 
                    new XRect(0, infoStartY, page.Width.Point, 20), XStringFormats.TopCenter);
                infoStartY += 20;

                graphics.DrawString($"Email: {email}", infoFont, XBrushes.Black, 
                    new XRect(0, infoStartY, page.Width.Point, 20), XStringFormats.TopCenter);
                infoStartY += 20;

                graphics.DrawString(location, infoFont, XBrushes.Black, 
                    new XRect(0, infoStartY, page.Width.Point, 20), XStringFormats.TopCenter);
                document.Save(memoryStream, false);
                return memoryStream.ToArray();
            }
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReservation,IdUtilisateur,IdChambre,DateDebut,DateFin,Statut,MontantTotal,DateReservation")] Reservation reservation)
        {
            if (id != reservation.IdReservation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.IdReservation))
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
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Chambre)
                .FirstOrDefaultAsync(m => m.IdReservation == id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.Statut = "Canceled";
            reservation.Chambre.Disponibilite = true;
            _context.Reservations.Update(reservation);
            await _emailService.SendEmailAsync(HttpContext.Session.GetString("email"), "Annulation de votre réservation", $"Votre réservation a été Annulée pour la chambre {reservation.Chambre.NumeroChambre} du {reservation.DateDebut} au {reservation.DateFin}.\nMontant total: {reservation.MontantTotal} Dirhams");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            
            
            return _context.Reservations.Any(e => e.IdReservation == id);
        }
        
        public async Task<IActionResult> ReservationChambre(int? id)
        {
            var chambre = await _context.Chambres
                .FirstOrDefaultAsync(m => m.IdChambre == id);
            if (chambre == null)
            {
                return NotFound();
            }
            // Étape 2 : Marquer la chambre comme réservée
            chambre.Disponibilite = false;
            _context.Chambres.Update(chambre);

            // Vous pouvez également effectuer des modifications supplémentaires ici si nécessaire.

            // Sauvegarder les changements
            await _context.SaveChangesAsync();
            return View();
        }
        }
    }
    

