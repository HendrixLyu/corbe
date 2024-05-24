//This service returns the location of the certificate file
public interface ICertLocationProvider
{
  string GetCertLocation();
}

public class CertLocationProvider : ICertLocationProvider
{
  public string GetCertLocation()
  {
    //HINT: The certificate file is located in the certs folder
    return Path.Combine(Directory.GetCurrentDirectory(), "Certs", "ValidCertificate.cert");
  }
}
