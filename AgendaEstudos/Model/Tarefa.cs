using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AgendaEstudos.Enum;

namespace AgendaEstudos.Model;

public class Tarefa
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Titulo { get; set; }
    
    public string? Descricao { get; set; }
    
    [Required]
    public DateTime DataInicio { get; set; }    
    
    [Required]
    public DateTime DataFim { get; set; }   
    
    public int UserId { get; set; }
    
    
    public int MateriaId { get; set; }
    
    [Required]
    public Prioridade? Prioridade { get; set; }
    
    [JsonIgnore]
    public User? User { get; set; }
    
    [JsonIgnore]
    public Materia? Materia { get; set; }
    
}