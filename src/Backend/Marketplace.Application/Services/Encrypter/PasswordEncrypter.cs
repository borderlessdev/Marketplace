using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Marketplace.Application.Services.Encrypter;

public class PasswordEncrypter
{
    private readonly string _addiotinalKey;

    public PasswordEncrypter(string addiotinalKey)
    {
        _addiotinalKey = addiotinalKey;
    }

    public string Encrypt(string password)
    {
        var passwordWithKey = $"{password}{_addiotinalKey}";

        var bytes = Encoding.UTF8.GetBytes(passwordWithKey);
        byte[] hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }


    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();

      
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}
