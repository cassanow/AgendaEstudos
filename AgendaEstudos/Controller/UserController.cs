using AgendaEstudos.DTO;
using AgendaEstudos.Interface;
using AgendaEstudos.Mapping;
using AgendaEstudos.Model;
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
        var user = await _repository.GetById(id);
        
        if (user == null)
            return NotFound();
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = UserMapping.Touser(dto);
        
        return Ok(response);
    }

    [HttpDelete("DeleteUser/{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _repository.GetById(id);
          
        if(user == null)
            return NotFound();  
        
        var response = await _repository.DeleteUser(user);    
        
        return Ok(response);
    }
}