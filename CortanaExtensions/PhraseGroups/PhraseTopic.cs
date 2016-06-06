using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CortanaExtensions.Interfaces;
using System.Xml.Linq;
using CortanaExtensions.Enums;
using System.ComponentModel;
using CortanaExtensions.Common;

namespace CortanaExtensions.PhraseGroups
{
    public class PhraseTopic : Structure, IElement
    {
        public PhraseTopic(string Label, IList<PhraseTopicSubject> Subjects = null) { Build(Label); this.Subjects = Subjects; }
        public PhraseTopic(string Label, PhraseTopicScenario Scenario = PhraseTopicScenario.Dictation, IList<PhraseTopicSubject> Subjects = null) { Build(Label, Scenario); this.Subjects = Subjects; }
        internal void Build(string Label, PhraseTopicScenario Scenario = PhraseTopicScenario.Dictation)
        {
            Element = new XElement(Schema + "PhraseTopic");
            this.Label = Label;
            this.Scenario = Scenario;
        }

        private string _label { get; set; }
        public string Label { get { return _label; } set { Element.SetAttributeValue("Label", value); _label = value; } }

        private PhraseTopicScenario _Scenario { get; set; }
        public PhraseTopicScenario Scenario { get { return _Scenario; } set { Element.SetAttributeValue("Scenario", EnumHelper.GetDescription(value)); _Scenario = value; } }

        public IList<PhraseTopicSubject> Subjects { get; set; }
        private XElement Element { get; set; }
        public XElement getElement()
        {
            foreach(var Subject in Subjects)
            {
                Element.Add(new XElement("Subject", EnumHelper.GetDescription(Subject)));
            }
            return Element;
        }

    }
}
