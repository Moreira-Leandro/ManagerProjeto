using Domain.Enum;

namespace Domain.Models;

public class Cidade(int id, string nome, UFCidade uf)
{

    public int Id { get; } = id;
    public string Nome { get; private set; } = nome;
    public UFCidade Uf { get; private set; } = uf;
    
}