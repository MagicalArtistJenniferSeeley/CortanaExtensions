using CortanaExtensions.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CortanaExtensions.Commands
{
	/// <summary>
	/// Voice command that activates the App in foreground when requested. AppTarget is an optional string to provide parameter to you app on Activation.
	/// </summary>
	public class ForegroundActivatedVoiceCommand : VoiceCommandBase
    {
        /// <summary>
        /// Voice command that activates the App in foreground when requested.
        /// </summary>
        /// <param name="Name">Name of the Command (REQUIRED)</param>
        public ForegroundActivatedVoiceCommand(string Name) { BuildCommand(); this.Name = Name; ServiceTarget = new XElement(Schema + "Navigate"); }
        /// <summary>
        /// Voice command that activates the App in foreground when requested.
        /// </summary>
        /// <param name="Name">Name of the Command (REQUIRED)</param>
        /// <param name="Example">Example of what the command does, shown in Cortana (REQUIRED)</param>
        /// <param name="ListenStatements">What Cortana will listen for to Activate the Command</param>
        /// <param name="Feedback">What Cortana will return when Command called (REQUIRED)</param>
        /// <param name="AppTarget">An optional string to provide parameter to you app on Activation.</param>
        public ForegroundActivatedVoiceCommand(string Name, string Example, IEnumerable<ListenStatement> ListenStatements, string Feedback, string AppTarget = "")
        {
            BuildCommand(Name, Example, ListenStatements, Feedback);
            if (string.IsNullOrWhiteSpace(AppTarget)) ServiceTarget = new XElement(Schema + "Navigate");
            else this.AppTarget = AppTarget;
        }

        internal string _AppTarget;
        /// <summary>
        /// An optional string to provide parameter to you app on Activation.
        /// </summary>
        public string AppTarget { get { return _AppTarget; } set { ServiceTarget = new XElement(Schema + "Navigate", new XAttribute("Target", value)); _AppTarget = value; } }
    }
}
