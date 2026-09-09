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
    public IActionResult CidadeIndex()
    {
        return View();
    }

    public IActionResult CriarCidade()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CriarCidadeInsert(CidadeFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var cidade = new Cidade(model.NomeCidade, model.UfCidade);

        await _cidadeService.CriarCidade(cidade);

        return RedirectToAction(nameof(CidadeIndex));

    }
    
}