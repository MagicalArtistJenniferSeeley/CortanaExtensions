using System.Xml.Linq;

namespace CortanaExtensions.Common
{
    public class Structure
    {
        /// <summary>
        /// VoiceCommandDefinition Schematics V1.2
        /// </summary>
        internal XNamespace Schema = XNamespace.Get("http://schemas.microsoft.com/voicecommands/1.2");
        /// <summary>
        /// XML Declaration for File
        /// </summary>
        internal XDeclaration Declaration = new XDeclaration("1.0", "utf-8", null);
    }
}
