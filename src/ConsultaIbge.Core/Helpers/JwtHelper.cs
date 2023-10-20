using System.Text;

namespace ConsultaIbge.Core.Authentication;

public static class JwtHelper
{
    public static string PrivateKey { get; private set; } = null!;
    public static byte[] PrivateKeyBytes => Encoding.UTF8.GetBytes(PrivateKey);

    public static void LoadFromSettings(string privateKey)
    {
        byte[] data = Convert.FromBase64String(privateKey);
        PrivateKey = Encoding.UTF8.GetString(data);
    }
}
