using AgendaEstudos.Model;

namespace AgendaEstudos.Interface;

public interface ITarefaRepository
{
    Task <ICollection<Tarefa>> GetTarefas();
    
    Task<Tarefa> GetTarefa(int id);
    
    Task<Tarefa> AddTarefa(Tarefa tarefa);
    
    Task<Tarefa> UpdateTarefa(Tarefa tarefa);       
    
    Task<bool> DeleteTarefa(Tarefa tarefa);      
}