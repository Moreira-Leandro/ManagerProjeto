using Domain.Enum;

namespace Domain.Models;

public class Cidade
{
    
    public int IdCidade { get; set; }
    public string NomeCidade { get; set; }
    public UFCidade UfCidade { get; set; }

    public Cidade(string nomeCidade, UFCidade ufCidade)
    {
        this.NomeCidade = nomeCidade;
        this.UfCidade = ufCidade;
    }
    
}