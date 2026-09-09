using Domain.Models;

namespace Application.Interfaces;

public interface IAlunoRepository
{
    
    public Task CreateAluno(Aluno aluno);
    public Task DeleteAluno(int idAluno);
    public Task UpdateAluno(Aluno aluno);
    public Task<Aluno?> FindById(int idAluno);
    public Task<List<Aluno>> FindAll();
    
}