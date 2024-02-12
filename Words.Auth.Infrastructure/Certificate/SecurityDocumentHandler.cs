using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Words.Auth.Infrastructure.Certificate
{
    public static class SecurityDocumentHandler
    {
        public static X509Certificate2 GetSecurityDocument()
        {
            string root = Assembly.GetExecutingAssembly().Location;
            string path = Path.GetFullPath(Path.Combine(root, @"..\..\..\..\..\res"));
            return new X509Certificate2(Path.Combine(path, "wordsauth.pfx"), "eiRPD58@y8%5");
        }
    }
}
