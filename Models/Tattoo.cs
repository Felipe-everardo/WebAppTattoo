namespace WebAppTattoo.Models;

public class Tattoo
{
    public int id { get; set; }
    public DateTime SessionDate { get; set; }
    public decimal ValuePaid { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? PostScript { get; set; }
    
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    public Tattoo() { }

    public Tattoo(DateTime sessionDate, decimal valuePaid, PaymentMethod paymentMethod,Client client, int clientId)
    {
        SessionDate = sessionDate;
        ValuePaid = valuePaid;
        PaymentMethod = paymentMethod;
        Client = client;
        ClientId = clientId;
    }
}
