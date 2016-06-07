using CortanaExtensions.Common;
using CortanaExtensions.Interfaces;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using CortanaExtensions.PhraseGroups;
using CortanaExtensions.Exceptions;
using CortanaExtensions.Commands;
using System.Linq;

namespace CortanaExtensions.Models
{
    /// <summary>
    /// A container for all the voice commands that an app will accept in the language specified by the required LanguageCode property. The value of the LanguageCode property must be unique in the VoiceCommand document, and it is a single, specific language, specified in language name form, that corresponds to a language that is available in the Speech control panel. The Name attribute is optional and can be any arbitrary string; however, the Name attribute is required in order to reference and update a CommandSet element's PhraseList programmatically.
    /// </summary>
    public class CommandSet : Structure, IElement
    {
        /// <summary>
        /// A container for all the voice commands that an app will accept in the language specified by the required LanguageCode property. The value of the LanguageCode property must be unique in the VoiceCommand document, and it is a single, specific language, specified in language name form, that corresponds to a language that is available in the Speech control panel. The Name attribute is optional and can be any arbitrary string; however, the Name attribute is required in order to reference and update a CommandSet element's PhraseList programmatically.
        /// </summary>
        /// <param name="LanguageCode">Language of the Command Set, each set must have a unique Language (REQUIRED)</param>
        public CommandSet(string LanguageCode) { Element = new XElement(Schema + "CommandSet"); this.LanguageCode = LanguageCode; }
        /// <summary>
        /// A container for all the voice commands that an app will accept in the language specified by the required LanguageCode property. The value of the LanguageCode property must be unique in the VoiceCommand document, and it is a single, specific language, specified in language name form, that corresponds to a language that is available in the Speech control panel. The Name attribute is optional and can be any arbitrary string; however, the Name attribute is required in order to reference and update a CommandSet element's PhraseList programmatically.
        /// </summary>
        /// <param name="Name">Name of the Command Set, Language Code is not Automatically added to this String</param>
        /// <param name="LanguageCode">Language of the Command Set, each set must have a unique Language (REQUIRED)</param>
        /// <param name="AppName">Specifies a user-friendly name for an app that a user can speak when giving a voice command. This is useful for apps with names that are long or are difficult to pronounce. Avoid using prefixes that conflict with other voice-enabled experiences</param>
        /// <param name="Example">Gives a representative example of what a user can say for a CommandSet as a whole</param>
        /// <param name="Commands">Cortana Commands</param>
        public CommandSet(string Name, string LanguageCode, string AppName, string Example, IEnumerable<VoiceCommandBase> Commands = null)
        {
            Element = new XElement(Schema + "CommandSet");
            _LanguageCode = LanguageCode;
            this.Name = Name;
            this.AppName = AppName;
            this.Example = Example;
            if(Commands != null) ((List<VoiceCommandBase>)this.Commands).AddRange(Commands);
        }
        private string _Name;
        /// <summary>
        /// Name of the Command Set, Language Code is not Automatically added to this String
        /// </summary>
        public string Name { get { return _Name; } set { Element.SetAttributeValue("Name", value); _Name = value; } }

        private string _LanguageCode;
        /// <summary>
        /// Language of the Command Set, each set must have a unique Language (REQUIRED)
        /// </summary>
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set
            {
                Element.SetAttributeValue(XNamespace.Xml + "lang", value);
                _LanguageCode = value;
            }
        }

        private string _AppName;
        /// <summary>
        /// Specifies a user-friendly name for an app that a user can speak when giving a voice command. This is useful for apps with names that are long or are difficult to pronounce. Avoid using prefixes that conflict with other voice-enabled experiences
        /// </summary>
        public string AppName { get { return _AppName; } set { AppNameConvention = new XElement(Schema + "AppName", value); _AppName = value; } }

        private string _Example;
        /// <summary>
        /// Gives a representative example of what a user can say for a CommandSet as a whole
        /// </summary>
        public string Example { get { return _Example; } set { AppCommandsExample = new XElement(Schema + "Example", value); _Example = value; } }

        private XElement AppNameConvention;
        private XElement AppCommandsExample;

        /// <summary>
        /// Cortana Commands
        /// </summary>
        public IList<VoiceCommandBase> Commands { get; set; } = new List<VoiceCommandBase>();
        /// <summary>
        /// Optional child of the CommandSet element. One CommandSet element can contain no more than 2,000 Item elements, and 2,000 Item elements is the combined total limit across all PhraseList elements in a CommandSet. Each Item specifies a word or phrase that can be recognized to initiate the command that references the PhraseList.
        /// </summary>
        public IList<PhraseList> PhraseLists { get; set; } = new List<PhraseList>();
        /// <summary>
        /// Optional child of the CommandSet element. Specifies a topic for large vocabulary recognition. The topic may specify a single (0 or 1) Scenario attribute and several (0 to 20) Subject child elements for the scenario, which may be used to improve the relevance of the recognition achieved. A PhraseTopic requires the Label attribute, the value of which may appear—enclosed by curly braces—inside ListenFor or Feedback elements, and is used to reference the PhraseTopic.
        /// </summary>
        public IList<PhraseTopic> PhraseTopics { get; set; } = new List<PhraseTopic>();

        private XElement Element { get; set; }
        public XElement getElement()
        {
            int PhraseListItemCount = 0;
            if (!string.IsNullOrWhiteSpace(AppName)) Element.Add(AppNameConvention);
            Element.Add(AppCommandsExample);
            if (Example == null) throw new Exception("Example is a Required Field in a Command Set");

            foreach (var command in Commands) { Element.Add(command.getElement()); }
            foreach (var phraseList in PhraseLists)
            {
                PhraseListItemCount += phraseList.Phrases.Count;
                Element.Add(phraseList.getElement());
            }
            foreach (var phraseTopic in PhraseTopics) { Element.Add(phraseTopic.getElement()); }
            if (Commands.Count > 100) throw new FieldOverflowException(Commands, "Can't have over 100 commands in Command Sets");
            else if (!Commands.Any()) throw new Exception("At Least 1 Command Required in a Command Set");
            if (PhraseListItemCount > 2000) throw new FieldOverflowException(PhraseLists, "Can't have over 2000 items in PhraseLists");
            return Element;
        }
    }
}
