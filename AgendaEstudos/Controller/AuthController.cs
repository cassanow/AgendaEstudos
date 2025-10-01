using AgendaEstudos.DTO;
using AgendaEstudos.Enum;
using AgendaEstudos.Interface;
using AgendaEstudos.Mapping;
using AgendaEstudos.Model;
using AgendaEstudos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AgendaEstudos.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserRepository _repository;
    private readonly IPasswordService _service;
    private readonly ITokenService _tokenService;

    public AuthController(IUserRepository repository,  IPasswordService service,  ITokenService tokenService)
    {
        _repository = repository;   
        _service = service;
        _tokenService = tokenService;   
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        var user = await _repository.GetByEmail(dto.Email);

        if (user == null)
            return NotFound("User or password is incorrect");
        
        var valid = _service.VerifyPassword(user.PasswordHash, dto.Password);
        
        if (!valid)
            return Unauthorized("Username or password is incorrect");
        
        return Ok(_tokenService.GenerateToken(user));        
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserDTO dto)
    {
        var user = await _repository.GetByEmail(dto.Email);
        
        if (user != null)
            return BadRequest("User already exists");
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = new User
        {
            Name = dto.Name,
            Email = dto.Email,
        };
        
        response.PasswordHash = _service.HashPassword(dto.Password);
        response.Role = Role.User;
        await _repository.AddUser(response);
        
        return Ok(dto);        
    }
}