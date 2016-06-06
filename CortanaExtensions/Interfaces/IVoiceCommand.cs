using CortanaExtensions.Common;
using CortanaExtensions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CortanaExtensions.Interfaces
{
    internal static class VoiceCommandMethods
    {
        private class Commands : Structure { }
        static Commands structure = new Commands();
        public static XElement BuildCommand(this IVoiceCommand command, string Name, string Example, IList<ListenStatement> ListenStatements, string Feedback)
        {
            command.ListenStatements = ListenStatements.ToList();
            var Element = new XElement(structure.Schema + "Command", new XAttribute("Name", Name));
            Element.Add(new XElement(structure.Schema + "Example", Example));
            foreach (var ListenStatement in ListenStatements)
            {
                var listenfor = new XElement(structure.Schema + "ListenFor", new XAttribute("RequireAppName", ListenStatement.AppNameLocation.ToString()));
                listenfor.Value = ListenStatement.ListenFor;
                Element.Add(listenfor);
            }
            Element.Add(new XElement(structure.Schema + "Feedback", Feedback));
            return Element;
        }

        public static XElement AddListenStatementInternal(this IVoiceCommand command, ListenStatement ListenStatement)
        {
            if (command.ListenStatements == null) command.ListenStatements = new List<ListenStatement>();
            command.ListenStatements.Add(ListenStatement);

            if (command.ListenStatements.Count > 10) throw new ListenStatementOverflowException(command.ListenStatements);

            var listenfor = new XElement(structure.Schema + "ListenFor", new XAttribute("RequireAppName", ListenStatement.AppNameLocation.ToString()));
            listenfor.Value = ListenStatement.ListenFor;
            return listenfor;
        }
    }
    public interface IVoiceCommand : IElement
    {
        XElement BuildCommand(string Name, string Example, IList<ListenStatement> ListenStatements, string Feedback);
        void AddListenStatement(ListenStatement ListenStatement);
        List<ListenStatement> ListenStatements { get; set; }
    }

}
