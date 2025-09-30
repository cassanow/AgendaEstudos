using System.Security.Claims;
using AgendaEstudos.DTO;
using AgendaEstudos.Interface;
using AgendaEstudos.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaEstudos.Controller;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TarefaController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaController(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }
    
    [HttpGet("GetTarefas")]
    public async Task<IActionResult> GetTarefas()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var tarefas = await _tarefaRepository.GetTarefas(userId);
        
        if (tarefas == null)
            return NotFound();
        
        return Ok(tarefas); 
    }

    [HttpPost("AddTarefas")]
    public async Task<IActionResult> AddTarefas(TarefaDTO dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = new Tarefa
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            DataInicio = dto.DataInicio,
            DataFim = dto.DataFim,
            Prioridade = dto.Prioridade,
            UserId = userId
        };
        
        await _tarefaRepository.AddTarefa(response);
        
        return Ok(response);    
    }

    [HttpPatch("UpdateTarefas{id:int}")]
    public async Task<IActionResult> UpdateTarefas(int id, TarefaDTO dto)
    {
        var tarefa = await _tarefaRepository.GetTarefa(id);
        
        if(tarefa == null)
            return NotFound();
        
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);

        if (!string.IsNullOrEmpty(dto.Titulo))
            tarefa.Titulo = dto.Titulo;
        
        if (!string.IsNullOrEmpty(dto.Descricao))
            tarefa.Descricao = dto.Descricao;
        
        if(dto.DataInicio != null)
            tarefa.DataInicio = dto.DataInicio;
        
        if(dto.DataFim != null)
            tarefa.DataFim = dto.DataFim;
        
        if(dto.Prioridade != null)
            tarefa.Prioridade = dto.Prioridade;
        
        await _tarefaRepository.UpdateTarefa(tarefa);   
        
        return Ok(dto);
    }

    [HttpDelete("DeleteTarefas{id:int}")]
    public async Task<IActionResult> DeleteTarefas(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var tarefa = await _tarefaRepository.GetTarefa(id);     
        
        if(userId != tarefa.UserId)
            return Unauthorized();
        
        if(tarefa == null)
            return NotFound();
        
        await _tarefaRepository.DeleteTarefa(tarefa);
        
        return Ok();  
    }
        
}