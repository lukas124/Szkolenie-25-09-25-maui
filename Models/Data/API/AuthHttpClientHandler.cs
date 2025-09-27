using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace MauiStart.Models.Data.API;

public class AuthHttpClientHandler : HttpClientHandler
{
    private static readonly string ExpectedPublicKeyHash = Constants.SSLPin;
    
    public AuthHttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = ValidateServerCertificate;
    }

    public static bool ValidateServerCertificate(
        HttpRequestMessage request,
        X509Certificate certificate,
        X509Chain chain,
        SslPolicyErrors sslPolicyErrors)
    {
        var x509 = new X509Certificate2(certificate);

        byte[] spkiBytes = ExportSubjectPublicKeyInfo(x509);
        if (spkiBytes == null)
            return false;

        using var sha256 = SHA256.Create();
        byte[] hash = sha256.ComputeHash(spkiBytes);
        string actualHash = Convert.ToBase64String(hash);
        
        var result = actualHash == ExpectedPublicKeyHash && sslPolicyErrors == SslPolicyErrors.None;

        if (!result)
        {
            request.Options.Set(new HttpRequestOptionsKey<bool>("PinCheckFailed"), true);
        }

        return result;
    }
    
    private static byte[] ExportSubjectPublicKeyInfo(X509Certificate2 cert)
    {
        if (cert.GetRSAPublicKey() is RSA rsa)
        {
            return rsa.ExportSubjectPublicKeyInfo();
        }
        if (cert.GetECDsaPublicKey() is ECDsa ecdsa)
        {
            return ecdsa.ExportSubjectPublicKeyInfo();
        }

        return null;
    }
}