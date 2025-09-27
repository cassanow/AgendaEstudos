using AgendaEstudos.DTO;
using AgendaEstudos.Model;

namespace AgendaEstudos.Mapping;

public static class LoginMapping
{
    public static LoginDTO toLogin(User user)
    {
        return new LoginDTO
        {
            Email = user.Email, 
            Password = user.Password    
        };
    }
}