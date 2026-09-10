using Domain.Interfaces;
using Domain.Models;

namespace Application.Service;

public class CidadeService
{

    private readonly ICidadeRepositorio _repositorio;

    public CidadeService(ICidadeRepositorio repositorio)
    {
        this._repositorio = repositorio;
    }

    public async Task Crie(Cidade cidade)
    {
        await _repositorio.Crie(cidade);
    }

    public async Task Delete(int idCidade)
    {
        await _repositorio.Delete(idCidade);
    }

    public async Task<Cidade?> BusquePorId(int idCidade)
    {
        return await _repositorio.BusquePorId(idCidade);
    }

    public async Task<List<Cidade>> BusqueTodos()
    {
        return await _repositorio.BusqueTodos();
    }

    public async Task Atualize(Cidade cidade)
    {
        await _repositorio.Atualize(cidade);
    }

}