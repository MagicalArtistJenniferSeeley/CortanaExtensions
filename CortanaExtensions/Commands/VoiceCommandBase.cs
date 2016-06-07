using CortanaExtensions.Common;
using CortanaExtensions.Exceptions;
using CortanaExtensions.Interfaces;
using CortanaExtensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CortanaExtensions.Commands
{
    /// <summary>
    /// (USE ForegroundActivatedVoiceCommand OR BackgroundActivatedVoiceCommand instead) Base Class for Foreground and Background Activated Voice Commands
    /// </summary>
    public class VoiceCommandBase : Structure, IElement
    {
        public VoiceCommandBase()
        {
            if(!(this is ForegroundActivatedVoiceCommand) && !(this is BackgroundActivatedVoiceCommand)) throw new Exception("USE ForegroundActivatedVoiceCommand OR BackgroundActivatedVoiceCommand instead");
        }
        internal void BuildCommand() { Element = new XElement(Schema + "Command"); }
        internal void BuildCommand(string Name, string Example, IEnumerable<ListenStatement> ListenStatements, string Feedback)
        {
            Element = new XElement(Schema + "Command");
            this.Name = Name;
            this.Example = Example;
            if(ListenStatements != null) ((List<ListenStatement>)this.ListenStatements).AddRange(ListenStatements);
            this.Feedback = Feedback;
        }

        private string _Name;
        /// <summary>
        /// Name of the Command (REQUIRED)
        /// </summary>
        public string Name { get { return _Name; } set { Element.SetAttributeValue("Name", value); _Name = value; } }

        private string _Example;
        /// <summary>
        /// Example of what the command does, shown in Cortana (REQUIRED)
        /// </summary>
        public string Example { get { return _Example; } set { CommandExample = new XElement(Schema + "Example", value); _Example = value; } }
        private XElement CommandExample;

        /// <summary>
        /// What Cortana will listen for to Activate the Command (Max 10)
        /// </summary>
        public IList<ListenStatement> ListenStatements { get; set; } = new List<ListenStatement>();

        private string _Feedback;
        /// <summary>
        /// What Cortana will return when Command called (REQUIRED)
        /// </summary>
        public string Feedback { get { return _Feedback; } set { CommandFeedback = new XElement(Schema + "Feedback", value); _Feedback = value; } }
        private XElement CommandFeedback;

        internal XElement BuildListenStatement(ListenStatement ListenStatement)
        {
            var listenfor = new XElement(Schema + "ListenFor", new XAttribute("RequireAppName", ListenStatement.AppNameLocation.ToString()));
            listenfor.Value = ListenStatement.ListenFor;
            return listenfor;
        }
        internal XElement ServiceTarget;
        internal XElement Element;
        public XElement getElement()
        {
            if (Name == null) throw new Exception("Name is a Required Field");
            if (Example == null) throw new Exception("Example is a Required Field");
            if (Feedback == null) throw new Exception("Feedback is a Required Field");
            if (ListenStatements.Count > 10) throw new FieldOverflowException(ListenStatements, "Can't have over 10 Listen Statements in a Command");
            if (!ListenStatements.Any()) throw new Exception("At least 1 Listen Statement Required");
            if (ServiceTarget == null) throw new Exception("App or Service Target is Required");

            Element.Add(CommandExample);
            foreach (var ListenStatement in ListenStatements)
            {
                Element.Add(BuildListenStatement(ListenStatement));
            }
            Element.Add(CommandFeedback);
            Element.Add(ServiceTarget);
            return Element;
        }
    }

}
