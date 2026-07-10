using Microsoft.EntityFrameworkCore;
using WebAppTattoo.Data;
using WebAppTattoo.Models;

namespace WebAppTattoo.Services;

public class TattooService
{
    private readonly WebAppTattooContext _context;

    public TattooService(WebAppTattooContext context)
    {
        _context = context;
    }

    public async Task<List<Tattoo>> GetAllTattoosWithClientsAsync()
    {
        return await _context.Tattoo
            .Include(t => t.Client)
            .OrderByDescending(t => t.SessionDate)
            .ToListAsync();
    }

    public async Task<Tattoo?> GetTattooWithClientAsync(int id)
    {
        return await _context.Tattoo
            .Include(t => t.Client)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddTattooAsync(Tattoo tattoo)
    {
        _context.Tattoo.Add(tattoo);
        await _context.SaveChangesAsync();
    }

    public async Task<Tattoo?> FindTattooAsync(int id)
    {
        return await _context.Tattoo.FindAsync(id);
    }

    public async Task DeleteTattooAsync(int id)
    {
        var tattoo = await _context.Tattoo.FindAsync(id);
        if (tattoo != null)
        {
            _context.Tattoo.Remove(tattoo);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> TattooExistsAsync(int id)
    {
        return await _context.Tattoo.AnyAsync(e => e.Id == id);
    }

    public async Task UpdateTattooAsync(Tattoo tattoo)
    {
        _context.Update(tattoo);
        await _context.SaveChangesAsync();
    }

    // WebAppTattoo/Services/TattooService.cs

    public async Task<(int totalTattoos, decimal totalValue)> GetTattooSummaryAsync(DateTime startDate, DateTime endDate)
    {
        // A consulta ao banco de dados é feita aqui, filtrando pelo período.
        var tattoos = await _context.Tattoo
            .Where(t => t.SessionDate.Date >= startDate.Date && t.SessionDate.Date <= endDate.Date)
            .ToListAsync();

        int totalTattoos = tattoos.Count;
        decimal totalValue = tattoos.Sum(t => t.ValuePaid);

        // O método retorna uma tupla com a contagem e o valor.
        return (totalTattoos, totalValue);
    }
}
