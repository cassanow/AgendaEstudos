using AgendaEstudos.Model;

namespace AgendaEstudos.Interface;

public interface IMateriaRepository
{
    Task SaveChanges(); 
    Task<IEnumerable<Materia>> GetAllMaterias(int id);    
    
    Task<Materia> GetMateria(int id);   
    
    Task<bool> MateriaExists(int id);   
    
    Task<Materia> Add(Materia materia);
    
    Task<bool> Delete(Materia materia);
}