using AgendaEstudos.Model;

namespace AgendaEstudos.Interface;

public interface ITokenService
{
    string GenerateToken(User user); 
}