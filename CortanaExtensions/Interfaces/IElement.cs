using System.Xml.Linq;

namespace CortanaExtensions.Interfaces
{
	/// <summary>
	/// Fetches the Internal XML Element for processing
	/// </summary>
	public interface IElement
    {
        /// <summary>
        /// Fetches the Internal XML Element for processing
        /// </summary>
        XElement GetElement();
    }
}
