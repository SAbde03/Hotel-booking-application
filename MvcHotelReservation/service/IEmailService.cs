namespace MvcHotelReservation.service;

public interface IEmailService
{
    
    Task SendEmailAsync(string? getString, string confirmationDeVotreRéservation, string s, byte[] pdfContent, string bookingconfirmationPdf);
    Task SendEmailAsync(string? to, string subject, string message);
}