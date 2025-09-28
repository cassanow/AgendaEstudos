using System.ComponentModel.DataAnnotations;

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
}