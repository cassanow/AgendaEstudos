using AgendaEstudos.DTO;
using AgendaEstudos.Interface;
using AgendaEstudos.Mapping;
using AgendaEstudos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AgendaEstudos.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserRepository _repository;
    private readonly IPasswordService _service;

    public AuthController(IUserRepository repository,  IPasswordService service)
    {
        _repository = repository;   
        _service = service;
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        var user = await _repository.GetByEmail(dto.Email);

        if (user == null)
            return NotFound("User or password is incorrect");
        
        if (user.Password != dto.Password)
            return Unauthorized("User or password is incorrect");

        var hashed =  _service.HashPassword(user.Password);
        user.Password = hashed;
        
        return Ok(user);        
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserDTO dto)
    {
        var user = await _repository.GetByEmail(dto.Email);
        
        if (user != null)
            return BadRequest("User already exists");
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var response = UserMapping.Touser(dto);
              
        var hashed =  _service.HashPassword(response.Password);
        response.Password = hashed;
        await _repository.AddUser(response);
        
        return Ok(response);        
    }
}