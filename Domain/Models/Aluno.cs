using Domain.Enum;

namespace Domain.Models;

public class Aluno
{

    public int IdAluno { get; }
    public string NomeAluno { get; private set; }
    public DateOnly DataNascimentoAluno { get; private set; }
    public string? CpfAluno { get; private set; }
    public int CidadeIdAluno { get; private set; }
    public Sexo SexoAluno { get; private set; }

    public Aluno(string nomeAluno, DateOnly dataNascimentoAluno, string? cpfAluno, int cidadeIdAluno, Sexo sexoAluno)
    {
        this.NomeAluno = nomeAluno;
        this.DataNascimentoAluno = dataNascimentoAluno;
        this.CpfAluno = cpfAluno;
        this.CidadeIdAluno = cidadeIdAluno;
        this.SexoAluno = sexoAluno;
    }
    
    public Aluno(int idAluno, string nomeAluno, DateOnly dataNascimentoAluno, string? cpfAluno, int cidadeIdAluno, Sexo sexoAluno)
        : this(nomeAluno, dataNascimentoAluno, cpfAluno, cidadeIdAluno, sexoAluno)
    {
        IdAluno = idAluno;
    }
    
}