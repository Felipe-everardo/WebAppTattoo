using System.ComponentModel.DataAnnotations;

namespace WebAppTattoo.Models;

public class Client
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do cliente.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no maximo 100 caracteres.")]
    [Display(Name = "Nome")]
    public string? Name { get; set; }

    [Phone(ErrorMessage = "Informe um telefone valido.")]
    [StringLength(20, ErrorMessage = "O telefone deve ter no maximo 20 caracteres.")]
    [Display(Name = "Telefone")]
    public string? CellPhone { get; set; }

    [EmailAddress(ErrorMessage = "Informe um e-mail valido.")]
    [StringLength(120, ErrorMessage = "O e-mail deve ter no maximo 120 caracteres.")]
    [Display(Name = "E-mail")]
    public string? Email { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Data de nascimento")]
    public DateOnly BirthDate { get; set; }

    [StringLength(14, ErrorMessage = "O CPF deve ter no maximo 14 caracteres.")]
    [Display(Name = "CPF")]
    public string? CPF { get; set; }

    [StringLength(200, ErrorMessage = "O endereco deve ter no maximo 200 caracteres.")]
    [Display(Name = "Endereco")]
    public string? Address { get; set; }

    [StringLength(80, ErrorMessage = "O Instagram deve ter no maximo 80 caracteres.")]
    [Display(Name = "Instagram")]
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
        if (tattoo == null)
        {
            return;
        }

        Tattoos.Add(tattoo);
        tattoo.ClientId = Id;
    }

    public decimal GetValuePaidByPeriod(DateTime startDate, DateTime endDate)
    {
        return Tattoos
            .Where(t => t.SessionDate >= startDate && t.SessionDate <= endDate)
            .Sum(t => t.ValuePaid);
    }
}
