using Domain.Models;

namespace Domain.Interfaces;

public interface IAlunoRepositorio
{
    
    public Task Crie(Aluno aluno);
    public Task Delete(int matriculaAluno);
    public Task Atualize(Aluno aluno);
    public Task<Aluno?> BusquePorId(int matriculaAluno);
    public Task<List<Aluno>> BusqueTodos();
    
}