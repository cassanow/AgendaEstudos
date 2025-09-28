using AgendaEstudos.DTO;
using AgendaEstudos.Model;

namespace AgendaEstudos.Mapping;

public static class UserMapping
{
    public static void ToUserDTO(User user, UserDTO userDTO)
    {
        user.Email = userDTO.Email;     
        user.Name = userDTO.Name;
        user.PasswordHash = userDTO.Password;
    }
}