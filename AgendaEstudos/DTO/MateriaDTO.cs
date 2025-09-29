using AgendaEstudos.Enum;

namespace AgendaEstudos.DTO;

public class MateriaDTO
{
    public string Nome { get; set; }
    
    public string Descricao { get; set; }
    
    public Prioridade Prioridade { get; set; }
  
}