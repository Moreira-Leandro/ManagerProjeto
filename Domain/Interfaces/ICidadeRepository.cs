using Domain.Models;

namespace Application.Interfaces;

public interface ICidadeRepository
{
    
    public Task CreateCidade(Cidade cidade);
    public Task DeleteCidade(int idCidade);
    public Task UpdateCidade(Cidade cidade);
    public Task<Cidade?> FindById(int idCidade);
    public Task<List<Cidade>> FindAll();
    
}