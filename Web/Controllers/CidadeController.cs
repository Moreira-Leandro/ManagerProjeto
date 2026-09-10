using Application.Service;
using Domain.Models;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class CidadeController : Controller
{

    private readonly CidadeService _cidadeService;

    public CidadeController(CidadeService cidadeService)
    {
        _cidadeService = cidadeService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        List<Cidade> cidades = await _cidadeService.BusqueTodos();
        return View(cidades);
    }

    public IActionResult Criar()
    {
        return View(new CidadeFormViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Criar(CidadeFormViewModel model)
    {

        if (!ModelState.IsValid)
            return View(model);

        Cidade cidade = new Cidade(model.IdCidade, model.NomeCidade, model.UfCidade);
        await _cidadeService.Crie(cidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Deletar(int idCidade)
    {

        try
        {
            await _cidadeService.Delete(idCidade);
        }
        catch (FbException ex) when (ex.Message.Contains("FK_ALUNO_CIDADE"))
        {
            TempData["Erro"] = "Cidade não pode ser apagada pois possui aluno cadastrado com vinculo";
        }
        
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Editar(int idCidade)
    {
        Cidade cidade = await _cidadeService.BusquePorId(idCidade);

        if (cidade is null)
            return NotFound();

        CidadeFormViewModel model = new CidadeFormViewModel()
        {
            IdCidade = cidade.Id,
            NomeCidade = cidade.Nome,
            UfCidade = cidade.Uf
        };

        return View("Criar", model);

    }

    [HttpPost]
    public async Task<IActionResult> Editar(CidadeFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        Cidade cidade = new Cidade(model.IdCidade, model.NomeCidade, model.UfCidade);
        await _cidadeService.Atualize(cidade);
        
        return RedirectToAction(nameof(Index));
    }
    
}