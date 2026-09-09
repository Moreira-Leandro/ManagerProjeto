using Application.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class AlunoController : Controller
{

    private readonly AlunoService _alunoService;

    public AlunoController(AlunoService service)
    {
        this._alunoService = service;
    }
    
    // GET
    public IActionResult AlunoIndex()
    {
        return View();
    }

    public IActionResult CriarAluno()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CriarAlunoInsert(AlunoFormViewModel model)
    {

        if (!ModelState.IsValid)
            return View(model);

        var aluno = new Aluno(model.Nome, model.DataNascimento, model.Cpf, model.CidadeId, model.Sexo);

        await _alunoService.CriarAluno(aluno);
        
        return RedirectToAction(nameof(AlunoIndex));

    }
    
}