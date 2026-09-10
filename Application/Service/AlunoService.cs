using Domain.Interfaces;
using Domain.Models;

namespace Application.Service;

public class AlunoService
{

    private readonly IAlunoRepositorio _repositorio;

    public AlunoService(IAlunoRepositorio repositorio)
    {
        this._repositorio = repositorio;
    }

    public async Task Crie(Aluno aluno)
    {
        await _repositorio.Crie(aluno);
    }

    public async Task Delete(int idAluno)
    {
        await _repositorio.Delete(idAluno);
    }

    public async Task<Aluno?> BusquePorId(int idAluno)
    {
        return await _repositorio.BusquePorId(idAluno);
    }

    public async Task<List<Aluno>> BusqueTodos()
    {
        return await _repositorio.BusqueTodos();
    }

    public async Task Atualize(Aluno aluno)
    {
        await _repositorio.Atualize(aluno);
    }

}
