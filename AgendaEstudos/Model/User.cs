using System.ComponentModel.DataAnnotations;
using AgendaEstudos.DTO;
using AgendaEstudos.Enum;

namespace AgendaEstudos.Model;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)] 
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    [Required]  
    public Role Role { get; set; }
    
    public bool IsActive { get; set; } = true; 

    public ICollection<Materia> Materias { get; set; }  
}