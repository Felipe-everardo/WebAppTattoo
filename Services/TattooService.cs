using Microsoft.EntityFrameworkCore;
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

    // Retorna todas as tatuagens, incluindo as informações do cliente
    public async Task<List<Tattoo>> GetAllTattoosWithClientsAsync()
    {
        return await _context.Tattoo.Include(t => t.Client).ToListAsync();
    }

    // Busca uma tatuagem por Id, incluindo o cliente
    public async Task<Tattoo?> GetTattooWithClientAsync(int id)
    {
        return await _context.Tattoo
            .Include(t => t.Client)
            .FirstOrDefaultAsync(m => m.id == id);
    }

    // Adiciona uma nova tatuagem
    public async Task AddTattooAsync(Tattoo tattoo)
    {
        _context.Tattoo.Add(tattoo);
        await _context.SaveChangesAsync();
    }

    // Encontra uma tatuagem pelo Id
    public async Task<Tattoo?> FindTattooAsync(int id)
    {
        return await _context.Tattoo.FindAsync(id);
    }

    // Deleta uma tatuagem
    public async Task DeleteTattooAsync(int id)
    {
        var tattoo = await _context.Tattoo.FindAsync(id);
        if (tattoo != null)
        {
            _context.Tattoo.Remove(tattoo);
            await _context.SaveChangesAsync();
        }
    }

    // Verifica se uma tatuagem existe
    public async Task<bool> TattooExistsAsync(int id)
    {
        return await _context.Tattoo.AnyAsync(e => e.id == id);
    }

    // Atualiza uma tatuagem
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