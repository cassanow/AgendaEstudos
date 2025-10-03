using System.Security.Claims;
using AgendaEstudos.DTO;
using AgendaEstudos.Interface;
using AgendaEstudos.Mapping;
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
  
    [HttpGet("GetMaterias")]
    public async Task<IActionResult> GetAll()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var materias = await _materiaRepository.GetAllMaterias(userId);
        
        if(!materias.Any())
            return NoContent();
        
        
        return Ok(materias);    
    }


    [HttpPost("AddMateria")]
    public async Task<IActionResult> AddMateria(MateriaDTO dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);
        

        var response = new Materia
        {
            Nome = dto.Nome,
            Prioridade = dto.Prioridade,
            UserId = userId
        };
        
        if(await _materiaRepository.MateriaExists(response.Id))
            return BadRequest();
        
        await _materiaRepository.Add(response);
        
        return Ok(response);    
    }

    [HttpPut("UpdateMateria/{id:int}")]
    public async Task<IActionResult> UpdateMateria(int id, UpdateMateriaDTO dto)
    {
        var materia = await _materiaRepository.GetMateria(id);
        
        if(materia == null) 
            return NotFound();
        
        materia.Nome = dto.Nome;
        materia.Prioridade = dto.Prioridade;    
        
        await _materiaRepository.Update(materia);
        
        return Ok(materia); 
    }

    [HttpDelete("DeleteMateria/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var materia = await _materiaRepository.GetMateria(id);
        
        if(materia.UserId != userId)
            return Unauthorized();  
        
        if (materia == null)
            return NotFound();  

        await _materiaRepository.Delete(materia);
        return Ok();        
    }
}