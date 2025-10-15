using AgendaEstudos.Enum;
using AgendaEstudos.Model;

namespace AgendaEstudos.DTO;

public class TarefaDTO
{
    public string Titulo { get; set; }
    
    public string Descricao { get; set; }           
    
    public Prioridade Prioridade { get; set; }  
    
    public DateTime DataInicio { get; set; }
    
    public DateTime DataFim { get; set; }   
    
    public int MateriaId { get; set; }
}