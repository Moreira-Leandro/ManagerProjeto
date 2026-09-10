using Domain.Models;

namespace Domain.Interfaces;

public interface ICidadeRepositorio
{
    
    public Task Crie(Cidade cidade);
    public Task Delete(int idCidade);
    public Task Atualize(Cidade cidade);
    public Task<Cidade?> BusquePorId(int idCidade);
    public Task<List<Cidade>> BusqueTodos();
    
}