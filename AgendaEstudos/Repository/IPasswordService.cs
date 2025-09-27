namespace AgendaEstudos.Repository;

public interface IPasswordService
{
    string HashPassword(string password);     
}