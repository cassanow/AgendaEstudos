using AgendaEstudos.Model;

namespace AgendaEstudos.Interface;

public interface IMateriaRepository
{
    Task<IEnumerable<Materia>> GetAllMaterias();    
    
    Task<Materia> Add(Materia materia);
    
    Task<Materia> Update(Materia materia);
    
    Task<Materia> Delete(Materia materia);
}