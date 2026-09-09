using Application.Interfaces;
using Domain.Models;

namespace Application.Service;

public class CidadeService
{

    private readonly ICidadeRepository _repository;

    public CidadeService(ICidadeRepository repository)
    {
        this._repository = repository;
    }

    public async Task CriarCidade(Cidade cidade)
    {
        await _repository.CreateCidade(cidade);
    }

    public async Task DeletarCidade(int idCidade)
    {
        await _repository.DeleteCidade(idCidade);
    }

    public async Task<Cidade?> BuscarCidade(int idCidade)
    {
        return await _repository.FindById(idCidade);
    }

    public async Task<List<Cidade>> BuscarCidades()
    {
        return await _repository.FindAll();
    }

    public async Task AtualizarCidade(Cidade cidade)
    {
        await _repository.UpdateCidade(cidade);
    }

}