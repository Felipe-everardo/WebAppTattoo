using System.ComponentModel.DataAnnotations;

namespace WebAppTattoo.Models.ViewModels;

public class TattooSummaryViewModel
{
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; } = DateTime.Today;

    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; } = DateTime.Today;

    public int TotalTattoos { get; set; }
    public decimal TotalValue { get; set; }
    public bool HasResult { get; set; }
}
