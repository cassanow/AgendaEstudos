using AgendaEstudos.Database;
using AgendaEstudos.Interface;
using AgendaEstudos.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendaEstudos.Repository;


public class MateriaRepository : IMateriaRepository
{
    private readonly AppDbContext _context;

    public MateriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Materia> GetMateria(int id)
    {
        return await _context.Materia.Where(m => m.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Materia>> GetAllMaterias(int id)
    {
        return await _context.Materia.Where(u => u.UserId == id).ToListAsync();
    }

    public async Task<bool> MateriaExists(string name)
    {
       return await _context.Materia.AnyAsync(u => u.Nome == name);    
    }

    public async Task<Materia> Add(Materia materia)
    {
        _context.Materia.Add(materia);          
        await _context.SaveChangesAsync();  
        return materia;
    }

    public async Task<Materia> Update(Materia materia)
    {
        _context.Entry(materia).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return materia; 
    }

    public async Task<bool> Delete(Materia materia)
    {
        _context.Materia.Remove(materia);   
        await _context.SaveChangesAsync();
        return true;
    }
}