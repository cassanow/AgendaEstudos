using AgendaEstudos.DTO;
using AgendaEstudos.Model;

namespace AgendaEstudos.Mapping;

public static class MateriaMapping
{
    public static void ToMateriaDTO(Materia materia, MateriaDTO dto)
    {
        materia.Nome = dto.Nome;
        materia.Prioridade = dto.Prioridade;
    }
}