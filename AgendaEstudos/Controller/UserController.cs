using System.Security.Claims;
using AgendaEstudos.DTO;
using AgendaEstudos.Interface;
using AgendaEstudos.Mapping;
using AgendaEstudos.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaEstudos.Controller;

[ApiController]
[Route("api/[controller]")]     
public class UserController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("GetByEmail/{email}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _repository.GetByEmail(email);
        
        if (user == null)   
            return NotFound();  
        
        return Ok(user);
    }

    [HttpPut("UpdateUser/{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, UserDTO dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if (userId != id)  
            return Forbid();
        
        var user = await _repository.GetById(id);
        if (user == null)
            return NotFound();
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        UserMapping.ToUserDTO(user, dto);
        
        await _repository.UpdateUser(user);
        
        return Ok(user);
    }

    [HttpDelete("DeleteUser/{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 
        
        if (userId != id)           
            return Forbid();
        
        var user = await _repository.GetById(id);
          
        if(user == null)
            return NotFound();  
        
        var response = await _repository.DeleteUser(user);    
        
        return Ok(response);
    }
}