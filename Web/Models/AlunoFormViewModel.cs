using Domain.Enum;

namespace Web.Models;

public class AlunoFormViewModel
{
    public int IdAluno { get; set; }
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string? Cpf { get; set; }
    public int CidadeId { get; set; }
    public Sexo Sexo { get; set; }
}