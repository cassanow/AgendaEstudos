using System.ComponentModel.DataAnnotations;
using AgendaEstudos.Model;

namespace AgendaEstudos.DTO;

public class UserDTO
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }   
    
    [Required]
    public string Password { get; set; }    
    
}