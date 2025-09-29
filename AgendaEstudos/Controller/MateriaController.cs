using System.Security.Claims;
using AgendaEstudos.DTO;
using AgendaEstudos.Interface;
using AgendaEstudos.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaEstudos.Controller;

[Authorize]
[ApiController]
[Route("[controller]")]     
public class MateriaController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IMateriaRepository _materiaRepository;


    public MateriaController(IMateriaRepository materiaRepository)
    {
        _materiaRepository = materiaRepository;
    }
  
    [HttpGet("GetAllMaterias/{userId:int}")]
    public async Task<IActionResult> GetAll(int userId)
    {
        
        var materias = await _materiaRepository.GetAllMaterias(userId);
        
        return Ok(materias);    
    }


    [HttpPost("AddMateria")]
    public async Task<IActionResult> AddMateria(MateriaDTO dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        if(await _materiaRepository.MateriaExists(dto.Nome))
            return BadRequest("Essa materia ja existe");

        var response = new Materia
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Prioridade = dto.Prioridade,
            UserId = userId
        };

        await _materiaRepository.Add(response);
        
        return Ok(response);    
    }

    [HttpPatch("UpdateMateria")]
    public async Task<IActionResult> UpdateMateria(int id, MateriaDTO dto)
    {
        var materia = await _materiaRepository.GetMateria(id);
        
        if(!await _materiaRepository.MateriaExists(dto.Nome))
            return NotFound();      
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!string.IsNullOrEmpty(dto.Nome))
            dto.Nome = materia.Nome;
        
        if(!string.IsNullOrEmpty(dto.Descricao))
            dto.Descricao = materia.Descricao;
        
        if(!string.IsNullOrEmpty(dto.Prioridade.ToString()))
            dto.Prioridade = materia.Prioridade;

        await _materiaRepository.Update(materia);
        
        return Ok(dto); 
    }

    [HttpDelete("DeleteMateria/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var materia = await _materiaRepository.GetMateria(id);
        
        if(materia.UserId != userId)
            return Unauthorized();  
        
        if(!await _materiaRepository.MateriaExists(materia.Nome))
            return NotFound();

        await _materiaRepository.Delete(materia);
        return Ok();        
    }
}