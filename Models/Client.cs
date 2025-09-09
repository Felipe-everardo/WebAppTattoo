using System.ComponentModel.DataAnnotations;

namespace WebAppTattoo.Models;

public class Client
{
    
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CellPhone { get; set; }
    public string? Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? CPF { get; set; }
    public string? Address { get; set; }
    public string? Instagram { get; set; }

    public ICollection<Tattoo>? Tattoos { get; set; }
}
