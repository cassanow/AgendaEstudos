using AgendaEstudos.Database;
using AgendaEstudos.Interface;
using AgendaEstudos.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendaEstudos.Repository;

public class UserRepository : IUserRepository   
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _context.User.Where(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<User> GetById(int id)
    {
        return await _context.User.Where(u => u.Id == id).FirstOrDefaultAsync();    
    }

    public async Task<User> AddUser(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        _context.Update(user);  
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUser(User user)
    {
        _context.Remove(user);  
        await _context.SaveChangesAsync();  
        return true;
    }
}