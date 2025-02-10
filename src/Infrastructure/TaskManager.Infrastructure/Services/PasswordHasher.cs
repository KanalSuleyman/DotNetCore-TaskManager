using TaskManager.Application.Interfaces;

namespace TaskManager.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
    }

    public bool VerifyHashedPassword(string hashedPassword, string inputPassword)
    {
        // If the hashed password is not in BCrypt format, assume it is plaintext
        if (!hashedPassword.StartsWith("$2"))
        {
            return hashedPassword == inputPassword; // Plaintext comparison
        }

        return BCrypt.Net.BCrypt.EnhancedVerify(inputPassword, hashedPassword);
    }
}