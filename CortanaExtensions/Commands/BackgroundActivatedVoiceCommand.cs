using System.Collections.Generic;
using System.Xml.Linq;
using CortanaExtensions.Interfaces;
using CortanaExtensions.Common;

namespace CortanaExtensions.Commands
{
    public class BackgroundActivatedVoiceCommand : Structure, IVoiceCommand
    {
        public BackgroundActivatedVoiceCommand(string Name, string Example, IList<ListenStatement> ListenStatements, string Feedback, string BackgroundTarget)
        {
            Element = this.BuildCommand(Name, Example, ListenStatements, Feedback);
            var serviceTarget = new XElement(Schema + "VoiceCommandService", new XAttribute("Target", BackgroundTarget));
            Element.Add(serviceTarget);
        }
        private XElement Element { get; set; }
        public List<ListenStatement> ListenStatements { get; set; }

        XElement IVoiceCommand.BuildCommand(string Name, string Example, IList<ListenStatement> ListenStatements, string Feedback) { return this.BuildCommand(Name, Example, ListenStatements, Feedback); }
        public void AddListenStatement(ListenStatement ListenStatement) { Element.Add(this.AddListenStatementInternal(ListenStatement)); }
        public XElement getElement() { return Element; }
    }
}
