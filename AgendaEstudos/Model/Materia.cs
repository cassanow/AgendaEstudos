using System.ComponentModel.DataAnnotations;
using AgendaEstudos.Enum;

namespace AgendaEstudos.Model;

public class Materia
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Nome { get; set; }
    
    [Required]  
    public string Descricao { get; set; }
    
    [Required]
    public Prioridade Prioridade { get; set; }
    
    public int UserId { get; set; }
    
    public User User { get; set; }
}