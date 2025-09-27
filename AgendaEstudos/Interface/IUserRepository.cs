using AgendaEstudos.Model;

namespace AgendaEstudos.Interface;

public interface IUserRepository
{
    
    Task<User> GetByEmail(string email);
    
    Task<User> GetById(int id);     
    
    Task<User> AddUser(User user);
    
    Task<User> UpdateUser(User user);
    
    Task<bool> DeleteUser(User user);
}