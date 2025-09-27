using System.Security.Cryptography;
using AgendaEstudos.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AgendaEstudos.Service;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128/8);
        
        string hashed =  Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt:  salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,  
            numBytesRequested: 256 / 8
            ));  
        
        return hashed;      
    }
}