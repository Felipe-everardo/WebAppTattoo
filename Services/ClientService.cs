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

    // Retorna todos os clientes do banco de dados
    public async Task<List<Client>> GetAllClientsAsync()
    {
        return await _context.Client.ToListAsync();
    }

    // Adiciona um novo cliente ao banco de dados com uma validação de CPF
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

    // Busca um cliente por Id, incluindo suas tatuagens associadas
    public async Task<Client?> GetClientWithTattoosAsync(int clientId)
    {
        return await _context.Client
            .Include(c => c.Tattoos)
            .FirstOrDefaultAsync(c => c.Id == clientId);
    }

    // Encontra um cliente pelo Id (útil para as operações de Edit e Delete)
    public async Task<Client?> FindClientAsync(int clientId)
    {
        return await _context.Client.FindAsync(clientId);
    }

    // Atualiza um cliente existente
    public async Task UpdateClientAsync(Client client)
    {
        _context.Update(client);
        await _context.SaveChangesAsync();
    }

    // Deleta um cliente existente e suas tatuagens associadas
    public async Task DeleteClientAsync(int clientId)
    {
        var client = await _context.Client.FindAsync(clientId);
        if (client != null)
        {
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
        }
    }

    // Verifica se um cliente com um dado Id existe no banco de dados
    public async Task<bool> ClientExistsAsync(int id)
    {
        return await _context.Client.AnyAsync(e => e.Id == id);
    }
}