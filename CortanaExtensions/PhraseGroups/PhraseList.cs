using System.Collections.Generic;
using System.Xml.Linq;
using CortanaExtensions.Common;
using CortanaExtensions.Interfaces;

namespace CortanaExtensions.PhraseGroups
{
	/// <summary>
	/// Optional child of the CommandSet element. One CommandSet element can contain no more than 2,000 PhraseList Phrases, and 2,000 PhraseList Phrases is the combined total limit across all PhraseList elements in a CommandSet. Each Item specifies a word or phrase that can be recognized to initiate the command that references the PhraseList.
	/// </summary>
	public class PhraseList : Structure, IElement
    {
        /// <summary>
        /// Optional child of the CommandSet element. One CommandSet element can contain no more than 2,000 Item elements, and 2,000 Item elements is the combined total limit across all PhraseList elements in a CommandSet. Each Item specifies a word or phrase that can be recognized to initiate the command that references the PhraseList.
        /// </summary>
        public PhraseList(string Label) { Build(Label, null); }
        /// <summary>
        /// Optional child of the CommandSet element. One CommandSet element can contain no more than 2,000 Item elements, and 2,000 Item elements is the combined total limit across all PhraseList elements in a CommandSet. Each Item specifies a word or phrase that can be recognized to initiate the command that references the PhraseList.
        /// </summary>
        /// <param name="Label">Name of the PhraseList</param>
        /// <param name="Phrases">Phrases to add to the list</param>
        /// <param name="Disambiguate">Specifies whether this PhraseList will produce user disambiguation when multiple items from the list are simultaneously recognized. When false, this PhraseList will also be unusable from within Feedback elements and will not produce parameters for your application. That's useful for phrases that are alternative ways of saying the same thing, but do not require any specific action.</param>
        public PhraseList(string Label, IEnumerable<string> Phrases = null, bool Disambiguate = true) { Build(Label, Phrases); this.Disambiguate = Disambiguate; }
        /// <summary>
        /// Optional child of the CommandSet element. One CommandSet element can contain no more than 2,000 Item elements, and 2,000 Item elements is the combined total limit across all PhraseList elements in a CommandSet. Each Item specifies a word or phrase that can be recognized to initiate the command that references the PhraseList.
        /// </summary>
        /// <param name="Label">Name of the PhraseList</param>
        /// <param name="Disambiguate">Specifies whether this PhraseList will produce user disambiguation when multiple items from the list are simultaneously recognized. When false, this PhraseList will also be unusable from within Feedback elements and will not produce parameters for your application. That's useful for phrases that are alternative ways of saying the same thing, but do not require any specific action.</param>
        public PhraseList(string Label, bool Disambiguate = true) { Build(Label, null); this.Disambiguate = Disambiguate; }

        internal void Build(string Label, IEnumerable<string> Phrases)
        {
            Element = new XElement(Schema + "PhraseList");
            this.Label = Label;
            if(Phrases != null)((List<string>)this.Phrases).AddRange(Phrases);
        }

        private string _label { get; set; }
        /// <summary>
        /// Name of the PhraseList
        /// </summary>
        public string Label { get { return _label; } set { Element.SetAttributeValue("Label", value); _label = value; } }

        /// <summary>
        /// Phrases to add to the list
        /// </summary>
        public IList<string> Phrases { get; set; } = new List<string>();

        private XElement Element { get; set; }
		/// <summary>
		/// Specifies whether this PhraseList will produce user disambiguation when multiple items from the list are simultaneously recognized. When false, this PhraseList will also be unusable from within Feedback elements and will not produce parameters for your application. That's useful for phrases that are alternative ways of saying the same thing, but do not require any specific action.
		/// </summary>
		public bool Disambiguate { get; set; } = true;

        public XElement getElement()
        {
            if (Disambiguate == false) Element.SetAttributeValue("Disambiguate", Disambiguate);
            foreach(var phrase in Phrases)
            {
                Element.Add(new XElement(Schema + "Item", phrase));
            }
            return Element;
        }
    }
}
