using AgendaEstudos.Database;
using AgendaEstudos.Interface;
using AgendaEstudos.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendaEstudos.Repository;

public class TarefaRepository : ITarefaRepository
{
    private readonly AppDbContext _context;

    public TarefaRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Tarefa>> GetTarefas()
    {
        return await _context.Tarefa.ToListAsync();
    }

    public async Task<Tarefa> GetTarefa(int id)
    {
        return await _context.Tarefa.Where(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Tarefa> AddTarefa(Tarefa tarefa)
    {
        _context.Tarefa.Add(tarefa); 
        await _context.SaveChangesAsync();
        return tarefa;
    }

    public async Task<Tarefa> UpdateTarefa(Tarefa tarefa)
    {
        _context.Entry(tarefa).State = EntityState.Modified;                
        await _context.SaveChangesAsync();
        return tarefa;          
    }

    public async Task<bool> DeleteTarefa(Tarefa tarefa)
    {
        _context.Tarefa.Remove(tarefa);
        await _context.SaveChangesAsync();
        return true;    
    }
}