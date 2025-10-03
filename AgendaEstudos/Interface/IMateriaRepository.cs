using AgendaEstudos.Model;

namespace AgendaEstudos.Interface;

public interface IMateriaRepository
{
    Task<IEnumerable<Materia>> GetAllMaterias(int id);    
    
    
    Task<Materia> GetMateria(int id);   
    
    Task<bool> MateriaExists(string nome, int userId);   
    
    Task<Materia> Add(Materia materia);
    
    Task<Materia> Update(Materia materia);
    
    Task<bool> Delete(Materia materia);
}