using Domain.Models;

namespace Application.Interfaces;

public interface ICidadeRepository
{
    
    public Task CreateLab(Cidade cidade);
    public Task DeleteLab(int idCidade);
    public Task UpdateLab(Cidade cidade);
    public Task<Cidade?> FindById(int idCidade);
    public Task<List<Cidade>> FindAll();
    
}