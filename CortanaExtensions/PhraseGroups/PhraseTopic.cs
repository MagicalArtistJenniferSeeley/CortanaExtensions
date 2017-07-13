using System.Collections.Generic;
using CortanaExtensions.Interfaces;
using System.Xml.Linq;
using CortanaExtensions.Enums;
using CortanaExtensions.Common;

namespace CortanaExtensions.PhraseGroups
{
    /// <summary>
    /// Optional child of the CommandSet element. Specifies a topic for large vocabulary recognition. The topic may specify a single (0 or 1) Scenario attribute and several (0 to 20) Subject child elements for the scenario, which may be used to improve the relevance of the recognition achieved. A PhraseTopic requires the Label attribute, the value of which may appear—enclosed by curly braces—inside ListenFor or Feedback elements, and is used to reference the PhraseTopic.
    /// </summary>
    public class PhraseTopic : Structure, IElement
    {
        /// <summary>
        /// Optional child of the CommandSet element. Specifies a topic for large vocabulary recognition. The topic may specify a single (0 or 1) Scenario attribute and several (0 to 20) Subject child elements for the scenario, which may be used to improve the relevance of the recognition achieved. A PhraseTopic requires the Label attribute, the value of which may appear—enclosed by curly braces—inside ListenFor or Feedback elements, and is used to reference the PhraseTopic.
        /// </summary>
        public PhraseTopic(string Label) { Build(Label); }

        /// <summary>
        /// Optional child of the CommandSet element. Specifies a topic for large vocabulary recognition. The topic may specify a single (0 or 1) Scenario attribute and several (0 to 20) Subject child elements for the scenario, which may be used to improve the relevance of the recognition achieved. A PhraseTopic requires the Label attribute, the value of which may appear—enclosed by curly braces—inside ListenFor or Feedback elements, and is used to reference the PhraseTopic.
        /// </summary>
        /// <param name="Label">Name of the Phrase Topic</param>
        /// <param name="Subjects">Specifies a subject specific to the Scenario attribute of the parent PhraseTopic to further refine the relevance of speech recognition results within spoken commands using the PhraseTopic. Subjects will be evaluated in the order provided and, when appropriate, later-specified subjects will constrain earlier-specified ones.</param>
        public PhraseTopic(string Label, IEnumerable<PhraseTopicSubject> Subjects = null) { Build(Label); if (Subjects != null) ((List<PhraseTopicSubject>)this.Subjects).AddRange(Subjects); }

        /// <summary>
        /// Optional child of the CommandSet element. Specifies a topic for large vocabulary recognition. The topic may specify a single (0 or 1) Scenario attribute and several (0 to 20) Subject child elements for the scenario, which may be used to improve the relevance of the recognition achieved. A PhraseTopic requires the Label attribute, the value of which may appear—enclosed by curly braces—inside ListenFor or Feedback elements, and is used to reference the PhraseTopic.
        /// </summary>
        /// <param name="Label">Name of the Phrase Topic</param>
        /// <param name="Scenario">The Scenario attribute (default "Dictation") specifies the desired scenario for this PhraseTopic, which may optimize the underlying speech recognition of voice commands using the PhraseTopic to produce results that are better-suited to the desired context of the command.</param>
        /// <param name="Subjects">Specifies a subject specific to the Scenario attribute of the parent PhraseTopic to further refine the relevance of speech recognition results within spoken commands using the PhraseTopic. Subjects will be evaluated in the order provided and, when appropriate, later-specified subjects will constrain earlier-specified ones.</param>
        public PhraseTopic(string Label, PhraseTopicScenario Scenario = PhraseTopicScenario.Dictation, IEnumerable<PhraseTopicSubject> Subjects = null) { Build(Label, Scenario); if (Subjects != null) ((List<PhraseTopicSubject>)this.Subjects).AddRange(Subjects); }

        internal void Build(string Label, PhraseTopicScenario Scenario = PhraseTopicScenario.Dictation)
        {
            Element = new XElement(Schema + "PhraseTopic");
            this.Label = Label;
            this.Scenario = Scenario;
        }

        public XElement GetElement()
        {
            foreach (var Subject in Subjects)
            {
                Element.Add(new XElement("Subject", EnumHelper.GetDescription(Subject)));
            }
            return Element;
        }

        /// <summary>
        /// Name of the Phrase Topic
        /// </summary>
        public string Label { get { return _label; } set { Element.SetAttributeValue("Label", value); _label = value; } }

        /// <summary>
        /// The Scenario attribute (default "Dictation") specifies the desired scenario for this PhraseTopic, which may optimize the underlying speech recognition of voice commands using the PhraseTopic to produce results that are better-suited to the desired context of the command.
        /// </summary>
        public PhraseTopicScenario Scenario { get { return _Scenario; } set { Element.SetAttributeValue("Scenario", EnumHelper.GetDescription(value)); _Scenario = value; } }

        /// <summary>
        /// Specifies a subject specific to the Scenario attribute of the parent PhraseTopic to further refine the relevance of speech recognition results within spoken commands using the PhraseTopic. Subjects will be evaluated in the order provided and, when appropriate, later-specified subjects will constrain earlier-specified ones.
        /// </summary>
        public IList<PhraseTopicSubject> Subjects { get; set; } = new List<PhraseTopicSubject>();

        private XElement Element { get; set; }
        private string _label;
        private PhraseTopicScenario _Scenario;
    }
}