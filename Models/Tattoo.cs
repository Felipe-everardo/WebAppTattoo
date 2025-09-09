using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTattoo.Models;

public class Tattoo
{
    public int id { get; set; }
    public DateTime SessionDate { get; set; }
    public decimal ValuePaid { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? PostScript { get; set; }
    
    public int ClientId { get; set; }
    public Client Client { get; set; }
}
