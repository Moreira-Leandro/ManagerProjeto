using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Domain.Models;

public class Aluno(int matricula, string nome, DateOnly dataNascimento, string? cpf, int cidadeId, Sexo sexo)
{
    public int Matricula { get; } = matricula;
    public string Nome { get; private set; } = nome;
    public DateOnly DataNascimento { get; private set; } = dataNascimento;
    public string? Cpf { get; private set; } = cpf;
    public int CidadeId { get; private set; } = cidadeId;
    public Sexo Sexo { get; private set; } = sexo;
    
}