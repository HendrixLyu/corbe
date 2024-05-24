using System.Security.Cryptography;
using System.Text;

//HINT: This service reads the secret key from a file and computes the SHA256 hash
public interface ISecretKeyService
{
  string GetCertHash();
}

public class SecretKeyService : ISecretKeyService
{
  ICertLocationProvider _locationProvider;

  public SecretKeyService(ICertLocationProvider locationProvider)
  {
    this._locationProvider = locationProvider;
  }

  public string GetCertHash()
  {
    var filePath = _locationProvider.GetCertLocation();
    string key = ReadKeyFromFile(filePath);
    string hashedKey = ComputeSHA256Hash(key);
    return hashedKey;
  }

  private string ReadKeyFromFile(string filePath)
  {
    string key = ReadKeyFromFile(filePath);
    try
    {
      key = File.ReadAllText(filePath);
    }
    catch (Exception ex)
    {
      //HINT: Is the path correct? Is the file present?
      Console.WriteLine($"SecretKeyService: Error reading key from file: {ex.Message}");
      throw;
    }
    return key;
  }

  private string ComputeSHA256Hash(string input)
  {
    using (SHA256 sha256 = SHA256.Create())
    {
      byte[] bytes = Encoding.UTF8.GetBytes(input);
      byte[] hashBytes = sha256.ComputeHash(bytes);
      StringBuilder builder = new StringBuilder();
      foreach (byte b in hashBytes)
      {
        builder.Append(b.ToString("x2"));
      }
      return builder.ToString();
    }
  }
}