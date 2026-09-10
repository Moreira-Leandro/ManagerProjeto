using Application.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class AlunoController : Controller
{

    private readonly AlunoService _alunoService;
    private readonly CidadeService _cidadeService;

    public AlunoController(AlunoService serviceAluno, CidadeService serviceCidade)
    {
        this._alunoService = serviceAluno;
        this._cidadeService = serviceCidade;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        List<Aluno> alunos = await _alunoService.BusqueTodos();
        return View(alunos);
    }

    public async Task<IActionResult> Criar()
    {
        ViewBag.Cidades = await _cidadeService.BusqueTodos();
        return View(new AlunoFormViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Criar(AlunoFormViewModel model)
    {

        if (!ModelState.IsValid)
            return View(model);

        Aluno aluno = new Aluno(model.IdAluno, model.Nome, model.DataNascimento, model.Cpf, model.CidadeId, model.Sexo);
        await _alunoService.Crie(aluno);
        
        return RedirectToAction(nameof(Index));

    }

    [HttpPost]
    public async Task<IActionResult> Deletar(int idAluno)
    {
        
        await _alunoService.Delete(idAluno);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Editar(int id)
    {
        ViewBag.Cidades = await _cidadeService.BusqueTodos();
        Aluno aluno = await _alunoService.BusquePorId(id);

        if (aluno is null)
            return NotFound();

        AlunoFormViewModel model = new AlunoFormViewModel
        {
            IdAluno = aluno.Matricula,
            Nome = aluno.Nome,
            DataNascimento = aluno.DataNascimento,
            Cpf = aluno.Cpf,
            CidadeId = aluno.CidadeId,
            Sexo = aluno.Sexo
        };

        return View("Criar", model);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(AlunoFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        Aluno aluno = new Aluno(model.IdAluno, model.Nome, model.DataNascimento, model.Cpf, model.CidadeId, model.Sexo);
        await _alunoService.Atualize(aluno);

        return RedirectToAction(nameof(Index));
    }

}