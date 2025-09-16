using Microsoft.EntityFrameworkCore;
using WebAppTattoo.Data;
using WebAppTattoo.Models;

namespace WebAppTattoo.Services;

public class ClientService
{
    private readonly WebAppTattooContext _context;

    public ClientService(WebAppTattooContext context)
    {
        _context = context;
    }

    public async Task<List<Client>> GetAllClientsAsync()
    {
        return await _context.Client.ToListAsync();
    }

    public async Task AddClientAsync(Client client)
    {
        var existingClient = await _context.Client.FirstOrDefaultAsync(c => c.CPF == client.CPF);

        if (existingClient != null)
        {
            throw new Exception("Cliente já cadastrado");
        }

        _context.Client.Add(client);
        await _context.SaveChangesAsync();
    }

    public async Task<Client?> GetClientWithTattoosAsync(int clientId)
    {
        return await _context.Client
            .Include(c => c.Tattoos)
            .FirstOrDefaultAsync(c => c.Id == clientId);
    }

    public async Task<Client?> FindClientAsync(int clientId)
    {
        return await _context.Client.FindAsync(clientId);
    }

    public async Task UpdateClientAsync(Client client)
    {
        _context.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClientAsync(int clientId)
    {
        var client = await _context.Client.FindAsync(clientId);
        if (client != null)
        {
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ClientExistsAsync(int id)
    {
        return await _context.Client.AnyAsync(e => e.Id == id);
    }
}