using AgendaEstudos.Database;
using AgendaEstudos.Interface;
using AgendaEstudos.Model;

namespace AgendaEstudos.Repository;

public class MateriaRepository : IMateriaRepository
{
    private readonly AppDbContext _context;

    public MateriaRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Task<IEnumerable<Materia>> GetAllMaterias()
    {
        throw new NotImplementedException();
    }

    public Task<Materia> Add(Materia materia)
    {
        throw new NotImplementedException();
    }

    public Task<Materia> Update(Materia materia)
    {
        throw new NotImplementedException();
    }

    public Task<Materia> Delete(Materia materia)
    {
        throw new NotImplementedException();
    }
}