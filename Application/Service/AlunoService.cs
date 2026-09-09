using Application.Interfaces;
using Domain.Models;

namespace Application.Service;

public class AlunoService 
{

    private readonly IAlunoRepository _repository;

    public AlunoService(IAlunoRepository repository)
    {
        this._repository = repository;
    }

    public async Task CriarAluno(Aluno aluno)
    {
        await _repository.CreateAluno(aluno);
    }

    public async Task DeletarAluno(int idAluno)
    {
        await _repository.DeleteAluno(idAluno);
    }

    public async Task<Aluno?> BuscarAluno(int idAluno)
    {
        return await _repository.FindById(idAluno);
    }

    public async Task<List<Aluno>> BuscarAlunos()
    {
        return await _repository.FindAll();
    }
    
}