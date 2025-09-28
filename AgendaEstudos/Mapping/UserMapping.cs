using AgendaEstudos.DTO;
using AgendaEstudos.Model;

namespace AgendaEstudos.Mapping;

public static class UserMapping
{
    public static User Touser(UserDTO userDTO)
    {
        return new User
        {
            Email = userDTO.Email,
            Name = userDTO.Name,
            PasswordHash = userDTO.Password,
        };
    }

    public static UserDTO TouserDTO(User user)
    {
        return new UserDTO
        {
            Email = user.Email,
            Name = user.Name,
            Password = user.PasswordHash,
        };
    }
}