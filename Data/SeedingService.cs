using WebAppTattoo.Models;

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
            BirthDate = new DateOnly(1991,9,9)
        };

        Tattoo t1 = new Tattoo
        {
            SessionDate = new DateTime (2025,8,9),
            ValuePaid = 1000,
            PaymentMethod = PaymentMethod.Debito,
            PostScript = "Borboleta no Braço",
            Client = c1
        };

        Tattoo t2 = new Tattoo
        {
            SessionDate = new DateTime(2025,9,9),
            ValuePaid = 500,
            PaymentMethod = PaymentMethod.AVista,
            PostScript = "Retoque",
            Client = c1
        };

        // c1.AddTattoo(t1);

        _context.Tattoo.AddRange(t1, t2);
        
        _context.SaveChanges();
    }
}