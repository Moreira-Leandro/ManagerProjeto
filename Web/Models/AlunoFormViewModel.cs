using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Web.Models;

public class AlunoFormViewModel
{
    public int IdAluno { get; set; }
    [Required(ErrorMessage = "Informe o nome do aluno")]
    [StringLength(120)]
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "CPF inválido")]
    public string? Cpf { get; set; }
    public int CidadeId { get; set; }
    public Sexo Sexo { get; set; }
}