using AgendaEstudos.DTO;
using AgendaEstudos.Model;

namespace AgendaEstudos.Interface;

public interface ITarefaRepository
{
    Task <ICollection<Tarefa>> GetTarefas(int userId);
    
    Task<bool> TarefaExists(int userId, string titulo, int materiaId);
    
    Task<Tarefa> GetTarefa(int id);
    
    Task<Tarefa> AddTarefa(Tarefa tarefa);
    
    Task<Tarefa> UpdateTarefa(Tarefa tarefa);       
    
    Task<bool> DeleteTarefa(Tarefa tarefa);      
}