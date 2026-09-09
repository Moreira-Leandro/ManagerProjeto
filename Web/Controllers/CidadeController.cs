using Application.Service;
using Domain.Models;
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
    public async Task<IActionResult> CidadeIndex()
    {
        var cidades = await _cidadeService.BuscarCidades();
        return View(cidades);
    }

    public async Task<IActionResult> CriarCidade(int? id)
    {

        if (id == null)
            return View(new CidadeFormViewModel());

        var cidade = await _cidadeService.BuscarCidade(id.Value);

        if (cidade == null)
            return NotFound();

        var model = new CidadeFormViewModel()
        {
            IdCidade = cidade.IdCidade,
            
            NomeCidade = cidade.NomeCidade,
            UfCidade = cidade.UfCidade
        };
        
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SalvarCidade(CidadeFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.IdCidade == 0)
        {
            var cidade = new Cidade(model.NomeCidade, model.UfCidade);
            await _cidadeService.CriarCidade(cidade);
        }
        else
        {
            var cidade = new Cidade(model.IdCidade, model.NomeCidade, model.UfCidade);
            await _cidadeService.AtualizarCidade(cidade);
        }

        return RedirectToAction(nameof(CidadeIndex));

    }

    [HttpPost]
    public async Task<IActionResult> DeletarCidade(int idCidade)
    {
        await _cidadeService.DeletarCidade(idCidade);
        return RedirectToAction(nameof(CidadeIndex));
    }
    
}