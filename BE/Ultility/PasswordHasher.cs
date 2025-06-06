using BCrypt.Net;

public class PasswordHasher
{

    public static string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be null or empty");
        }


        return BCrypt.Net.BCrypt.HashPassword(password);
    }


    public static bool VerifyPassword(string password, string hashedPassword)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
        {
            return false;
        }

        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
