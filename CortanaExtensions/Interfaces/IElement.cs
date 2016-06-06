using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CortanaExtensions.Interfaces
{
    public interface IElement
    {
        /// <summary>
        /// Fetches the Internal XML Element for processing
        /// </summary>
        XElement getElement();
    }
}
