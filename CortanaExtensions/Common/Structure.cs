using System.Xml.Linq;

namespace CortanaExtensions.Common
{
    public class Structure
    {
        internal XNamespace Schema = XNamespace.Get("http://schemas.microsoft.com/voicecommands/1.2");
        internal XDeclaration Declaration = new XDeclaration("1.0", "utf-8", null);
    }
}
