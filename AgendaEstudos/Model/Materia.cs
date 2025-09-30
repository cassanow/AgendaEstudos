using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AgendaEstudos.Enum;

namespace AgendaEstudos.Model;

public class Materia
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Nome { get; set; }
    
    [Required]
    public Prioridade Prioridade { get; set; }
    
    public int UserId { get; set; }
    
    [JsonIgnore]
    public User? User { get; set; }
    
    public ICollection<Tarefa> Tarefas { get; set; } 
}