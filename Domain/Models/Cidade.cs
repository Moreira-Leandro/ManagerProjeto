using Domain.Enum;

namespace Domain.Models;

public class Cidade
{
    
    public int Id { get; }
    public string Nome { get; private set; }
    public UFCidade Uf { get; private set; }

    public Cidade(int id, string nome, UFCidade uf)
    {
        this.Id = id;
        this.Nome = nome;
        this.Uf = uf;
    }
    
}