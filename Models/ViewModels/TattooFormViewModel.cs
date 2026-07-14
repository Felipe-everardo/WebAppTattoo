using System.ComponentModel.DataAnnotations;
using WebAppTattoo.Models.Enums;

namespace WebAppTattoo.Models.ViewModels;

public class TattooFormViewModel : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe a data da sessao.")]
    [DataType(DataType.Date)]
    [Display(Name = "Data da sessao")]
    public DateTime SessionDate { get; set; } = DateTime.Today;

    [Range(1, int.MaxValue, ErrorMessage = "Selecione um cliente.")]
    [Display(Name = "Cliente")]
    public int ClientId { get; set; }

    [StringLength(300, ErrorMessage = "A descricao deve ter no maximo 300 caracteres.")]
    [Display(Name = "Descricao")]
    public string? PostScript { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Informe a forma de pagamento.")]
    [Display(Name = "Forma de pagamento")]
    public PaymentMethod PaymentMethod { get; set; }

    [Range(0.01, 999999.99, ErrorMessage = "O valor pago deve ser maior que zero.")]
    [DataType(DataType.Currency)]
    [Display(Name = "Valor pago")]
    public decimal ValuePaid { get; set; }

    public int? ReturnToClientId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (SessionDate.Date > DateTime.Today)
        {
            yield return new ValidationResult(
                "A data da sessao nao pode ser futura.",
                new[] { nameof(SessionDate) });
        }
    }
}
