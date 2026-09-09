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
    public async Task<IActionResult> AlunoIndex()
    {
        var alunos = await _alunoService.BuscarAlunos();
        return View(alunos);
    }

    public async Task<IActionResult> CriarAluno(int? id)
    {

        if (id == null)
            return View(new AlunoFormViewModel());
        
        var aluno = await _alunoService.BuscarAluno(id.Value);

        if (aluno == null)
            return NotFound();

        var model = new AlunoFormViewModel
        {
            IdAluno = aluno.IdAluno,
            Nome = aluno.NomeAluno,
            DataNascimento = aluno.DataNascimentoAluno,
            Cpf = aluno.CpfAluno,
            CidadeId = aluno.CidadeIdAluno,
            Sexo = aluno.SexoAluno
        };
        
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SalvarAluno(AlunoFormViewModel model)
    {

        if (!ModelState.IsValid)
            return View(model);

        if (model.IdAluno == 0)
        {
            var aluno = new Aluno(model.Nome, model.DataNascimento, model.Cpf, model.CidadeId, model.Sexo);
            await _alunoService.CriarAluno(aluno);
        }
        else
        {
            var aluno = new Aluno(model.IdAluno, model.Nome, model.DataNascimento, model.Cpf, model.CidadeId, model.Sexo);
            await _alunoService.AtualizarAluno(aluno);
        }

        return RedirectToAction(nameof(AlunoIndex));

    }

    [HttpPost]
    public async Task<IActionResult> DeletarAluno(int idAluno)
    {
        await _alunoService.DeletarAluno(idAluno);
        return RedirectToAction(nameof(AlunoIndex));
    }

}
