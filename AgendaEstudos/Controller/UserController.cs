using System.Security.Claims;
using AgendaEstudos.DTO;
using AgendaEstudos.Interface;
using AgendaEstudos.Mapping;
using AgendaEstudos.Model;
using AgendaEstudos.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace AgendaEstudos.Controller;

[ApiController]
[Route("api/[controller]")]     
public class UserController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserRepository _repository;
    private readonly IPasswordService _service;

    public UserController(IUserRepository repository,  IPasswordService service)
    {
        _repository = repository;
        _service = service;
    }
    
    [HttpGet("GetByEmail/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _repository.GetByEmail(email);
        
        if (user == null)   
            return NotFound();  
        
        if(!user.IsActive)
            return Unauthorized();
        
        var response = new UserDTO
        {
            Email = user.Email,
            Name = user.Name,
            Materias = user.Materias
        };
        
        return Ok(response);
    }

    [HttpPut("UpdateUser/{id:int}")]
    [Authorize(Roles = "Admin")]    
    public async Task<IActionResult> UpdateUser(int id, UserDTO dto)
    {
        var user = await _repository.GetById(id);
        if (user == null)
            return NotFound();
        
        if(!user.IsActive)
            return Unauthorized();
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        UserMapping.ToUserDTO(user, dto);
        user.PasswordHash = _service.HashPassword(dto.Password);   
        
        await _repository.UpdateUser(user);
        
        return Ok(user);
    }

    [HttpPatch("ParcialUpdateUser/{id:int}")]
    public async Task<IActionResult> PatchUser(int id, UpdateUserDTO dto)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier); 
        
        if(!int.TryParse(userIdString, out var userId) || string.IsNullOrEmpty(userIdString) || userId != id || !await _repository.UserIsActive(userId))
            return Unauthorized();
        
        var user = await _repository.GetById(id);   
        if (user == null)
            return NotFound();  
        
        if(!await _repository.UserIsActive(userId))
            return Unauthorized();      
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if(!string.IsNullOrEmpty(dto.Email)) 
            user.Email = dto.Email;
        
        if (!string.IsNullOrEmpty(dto.Password))    
            user.PasswordHash = _service.HashPassword(dto.Password);    
        
        if (!string.IsNullOrEmpty(dto.Name))
            user.Name = dto.Name;

        await _repository.SaveChanges();
        
        return Ok(new { Mensagem = "Alterado com sucesso!" });    
    }

    [HttpDelete("DeleteUser/{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier); 
        
        if(!int.TryParse(userIdString, out var userId) || string.IsNullOrEmpty(userIdString) || userId != id || !await _repository.UserIsActive(userId))
            return Unauthorized();

        var user = await _repository.GetById(id);
          
        if(user == null)
            return NotFound();  
        
        if(!await _repository.UserIsActive(user.Id))
            return BadRequest();
        
        var response = await _repository.DeleteUser(user);    
        
        return Ok(response);
    }
}