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

    public ICollection<Tattoo> Tattoos { get; set; } = new List<Tattoo>();

    public Client() { }

    public Client(string? name, string? cellPhone, string? email, DateOnly birthDate, string? cPF)
    {
        Name = name;
        CellPhone = cellPhone;
        Email = email;
        BirthDate = birthDate;
        CPF = cPF;
    }

    public void AddTattoo(Tattoo tattoo)
    {
        if (tattoo != null)
        {
            Tattoos.Add(tattoo);

            tattoo.ClientId = this.Id;
        }
    }

    public decimal GetValuePaidByPeriod(DateTime startDate, DateTime endDate)
    {
        var tatuagensFiltradas = Tattoos.Where(t => t.SessionDate >= startDate && t.SessionDate <= endDate);

        if (tatuagensFiltradas != null && tatuagensFiltradas.Any())
        {
            return tatuagensFiltradas.Sum(t => t.ValuePaid);
        }

        return 0;
    }

    /*
     public void RemoveTattoo(Tattoo tattoo)
     {
         if (tattoo != null && Tattoos.Contains(tattoo))
         {
             Tattoos.Remove(tattoo);
         }
     }
 
    public int TotalTattoos
    {
        get { return Tattoos.Count(); }
    }

    public decimal TotalValuePaid
    {
        get { return Tattoos.Sum(t => t.ValuePaid); }
    }*/
}
