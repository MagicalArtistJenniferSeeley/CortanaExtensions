using System.Collections.Generic;
using System.Xml.Linq;
using CortanaExtensions.Models;

namespace CortanaExtensions.Commands
{
	/// <summary>
	/// Voice command that activates the App in background when requested. BackgroundTarget is required and must match the value of the Name attribute of the AppService element in the app package manifest.
	/// </summary>
	public class BackgroundActivatedVoiceCommand : VoiceCommandBase
    {
        /// <summary>
        /// Voice command that activates the App in background when requested.
        /// </summary>
        /// <param name="Name">Name of the Command (REQUIRED)</param>
        public BackgroundActivatedVoiceCommand(string Name) { BuildCommand(); this.Name = Name; }
        /// <summary>
        /// Voice command that activates the App in background when requested.
        /// </summary>
        /// <param name="Name">Name of the Command (REQUIRED)</param>
        /// <param name="Example">Example of what the command does, shown in Cortana (REQUIRED)</param>
        /// <param name="ListenStatements">What Cortana will listen for to Activate the Command</param>
        /// <param name="Feedback">What Cortana will return when Command called (REQUIRED)</param>
        /// <param name="BackgroundTarget">Must match the value of the Name attribute of the AppService element in the app package manifest. (REQUIRED)</param>
        public BackgroundActivatedVoiceCommand(string Name, string Example, IEnumerable<ListenStatement> ListenStatements, string Feedback, string BackgroundTarget)
        {
            BuildCommand(Name, Example, ListenStatements, Feedback);
            this.BackgroundTarget = BackgroundTarget;
        }

        internal string _BackgroundTarget;
        /// <summary>
        /// Must match the value of the Name attribute of the AppService element in the app package manifest. (REQUIRED)
        /// </summary>
        public string BackgroundTarget { get { return _BackgroundTarget; } set { ServiceTarget = new XElement(Schema + "VoiceCommandService", new XAttribute("Target", value)); _BackgroundTarget = value; } }
    }
}
