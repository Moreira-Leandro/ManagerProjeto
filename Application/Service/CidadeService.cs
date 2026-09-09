using Application.Interfaces;

namespace Application.Service;

public class CidadeService
{

    private readonly ICidadeRepository _repository;

    public CidadeService(ICidadeRepository repository)
    {
        this._repository = repository;
    }

}