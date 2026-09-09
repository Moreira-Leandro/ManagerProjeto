using Domain.Enum;

namespace Web.Models;

public class CidadeFormViewModel
{
    public int IdCidade { get; set; }
    public string NomeCidade { get; set; }
    public UFCidade UfCidade { get; set; }
}