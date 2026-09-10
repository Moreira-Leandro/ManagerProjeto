using Domain.Enum;

namespace Domain.Models;

public class Aluno
{
    public int Matricula { get; }
    public string Nome { get; private set; }
    public DateOnly DataNascimento { get; private set; }
    public string? Cpf { get; private set; }
    public int CidadeId { get; private set; }
    public Sexo Sexo { get; private set; }

    public Aluno(int matricula, string nome, DateOnly dataNascimento, string? cpf, int cidadeId, Sexo sexo)
    {
        this.Matricula = matricula;
        this.Nome = nome;
        this.DataNascimento = dataNascimento;
        this.Cpf = cpf;
        this.CidadeId = cidadeId;
        this.Sexo = sexo;
    }

}