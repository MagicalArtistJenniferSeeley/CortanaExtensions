using CortanaExtensions.Common;
using CortanaExtensions.Interfaces;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using CortanaExtensions.PhraseGroups;
using CortanaExtensions.Exceptions;

namespace CortanaExtensions
{
    public class CommandSet : Structure, IElement
    {
        public CommandSet() { Element = new XElement(Schema + "CommandSet"); }
        public CommandSet(string Name, string LanguageCode, string AppName, string Example, IList<IVoiceCommand> Commands = null)
        {
            Element = new XElement(Schema + "CommandSet");
            _LanguageCode = LanguageCode;
            this.Name = Name;
            this.AppName = AppName;
            this.Example = Example;
            this.Commands = Commands;
        }
        private string _Name { get; set; }
        public string Name { get { return _Name; } set { Element.SetAttributeValue("Name", $"{value}_{LanguageCode}"); _Name = value; } }

        private string _LanguageCode { get; set; }
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set
            {
                Element.SetAttributeValue("Name", $"{Name}_{value}");
                Element.SetAttributeValue(XNamespace.Xml + "lang", value);
                _LanguageCode = value;
            }
        }

        private string _AppName { get; set; }
        public string AppName { get { return _AppName; } set { AppNameConvention = new XElement(Schema + "AppName", AppName); _AppName = value; } }

        private string _Example { get; set; }
        public string Example { get { return _Example; } set { AppCommandsExample = new XElement(Schema + "Example", Example); _Example = value; } }

        public void AddCommand(IVoiceCommand command) { Element.Add(command.getElement()); }
        private XElement AppNameConvention { get; set; }
        private XElement AppCommandsExample { get; set; }
        public IList<IVoiceCommand> Commands { get; set; }
        public IList<PhraseList> PhraseLists { get; set; }
        public IList<PhraseTopic> PhraseTopics { get; set; }

        private XElement Element { get; set; }
        public XElement getElement()
        {
            int PhraseListItemCount = 0;
            Element.Add(AppNameConvention);
            Element.Add(AppCommandsExample);
            foreach(var command in Commands) { Element.Add(command.getElement()); }
            foreach(var phraseList in PhraseLists)
            {
                PhraseListItemCount += phraseList.Phrases.Count;
                Element.Add(phraseList.getElement());
            }
            foreach (var phraseTopic in PhraseTopics) { Element.Add(phraseTopic.getElement()); }
            if (PhraseListItemCount > 2000) throw new PhraseListIemOverflowException(PhraseLists);
            return Element;
        }
    }
}
