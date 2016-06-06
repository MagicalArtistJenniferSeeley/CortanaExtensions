using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CortanaExtensions.Common;
using CortanaExtensions.Interfaces;

namespace CortanaExtensions.PhraseGroups
{
    public class PhraseList : Structure, IElement
    {
        /// <summary>
        /// Optional child of the CommandSet element. One CommandSet element can contain no more than 2,000 Item elements, and 2,000 Item elements is the combined total limit across all PhraseList elements in a CommandSet. Each Item specifies a word or phrase that can be recognized to initiate the command that references the PhraseList.
        /// </summary>
        /// <param name="Label">Name of the PhraseList</param>
        public PhraseList(string Label, IList<string> Phrases = null) { Build(Label, Phrases); }
        /// <summary>
        /// Optional child of the CommandSet element. One CommandSet element can contain no more than 2,000 Item elements, and 2,000 Item elements is the combined total limit across all PhraseList elements in a CommandSet. Each Item specifies a word or phrase that can be recognized to initiate the command that references the PhraseList.
        /// </summary>
        /// <param name="Label">Name of the PhraseList</param>
        /// <param name="Phrases">Phrases to add to the list</param>
        /// <param name="Disambiguate">Specifies whether this PhraseList will produce user disambiguation when multiple items from the list are simultaneously recognized. When false, this PhraseList will also be unusable from within Feedback elements and will not produce parameters for your application. That's useful for phrases that are alternative ways of saying the same thing, but do not require any specific action.</param>
        public PhraseList(string Label, IList<string> Phrases = null, bool Disambiguate = true) { Build(Label, Phrases); this.Disambiguate = Disambiguate; }
        /// <summary>
        /// Optional child of the CommandSet element. One CommandSet element can contain no more than 2,000 Item elements, and 2,000 Item elements is the combined total limit across all PhraseList elements in a CommandSet. Each Item specifies a word or phrase that can be recognized to initiate the command that references the PhraseList.
        /// </summary>
        /// <param name="Label">Name of the PhraseList</param>
        /// <param name="Disambiguate">Specifies whether this PhraseList will produce user disambiguation when multiple items from the list are simultaneously recognized. When false, this PhraseList will also be unusable from within Feedback elements and will not produce parameters for your application. That's useful for phrases that are alternative ways of saying the same thing, but do not require any specific action.</param>
        public PhraseList(string Label, bool Disambiguate = true) { Build(Label, null); this.Disambiguate = Disambiguate; }
        internal void Build(string Label, IList<string> Phrases)
        {
            Element = new XElement(Schema + "PhraseList");
            this.Label = Label;
            this.Phrases = Phrases;
        }
        private string _label { get; set; }
        public string Label { get { return _label; } set { Element.SetAttributeValue("Label", value); _label = value; } }

        public IList<string> Phrases { get; set; }
        private XElement Element { get; set; }
        public bool? Disambiguate { get; set; }
        public XElement getElement()
        {
            if (Disambiguate != null) Element.SetAttributeValue("Disambiguate", Disambiguate);
            foreach(var phrase in Phrases)
            {
                Element.Add(new XElement(Schema + "Item", phrase));
            }
            return Element;
        }
    }
}
