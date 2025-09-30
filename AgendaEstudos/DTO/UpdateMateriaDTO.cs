using AgendaEstudos.Enum;

namespace AgendaEstudos.DTO;

public class UpdateMateriaDTO
{
    public string? Nome { get; set; }    
    
    public Prioridade Prioridade { get; set; } 
}