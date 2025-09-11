using WebAppTattoo.Models;
using WebAppTattoo.Models.Enums;

namespace WebAppTattoo.Data;

public class SeedingService
{
    private WebAppTattooContext _context;

    public SeedingService(WebAppTattooContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Client.Any() || _context.Tattoo.Any())
        {
            return; // DB has been seeded
        }

        Client c1 = new Client
        {
            Name = "jurema",
            CellPhone = "2186456048",
            Email = "ju@gmail.com",
            BirthDate = new DateOnly(1991, 9, 9)
        };

       // Client c2 = new Client { Name = "Biriba", CellPhone = "23456789", Email = "Biri@gmail.com", BirthDate = new DateOnly(1995, 20, 5) };

        Tattoo t1 = new Tattoo
        {
            SessionDate = new DateTime(2025, 8, 9),
            ValuePaid = 1000,
            PaymentMethod = PaymentMethod.Debito,
            PostScript = "Borboleta no Braço",
            Client = c1
        };

        Tattoo t2 = new Tattoo
        {
            SessionDate = new DateTime(2025, 9, 9),
            ValuePaid = 500,
            PaymentMethod = PaymentMethod.AVista,
            PostScript = "Retoque",
            Client = c1
        };

        _context.Tattoo.AddRange(t1, t2);

        _context.SaveChanges();
    }
}