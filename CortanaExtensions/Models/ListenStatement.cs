using CortanaExtensions.Enums;

namespace CortanaExtensions.Models
{
    /// <summary>
    /// What Cortana will listen for to Activate the Command
    /// </summary>
    public class ListenStatement
    {
        /// <summary>
        /// What Cortana will listen for to Activate the Command
        /// </summary>
        /// <param name="ListenFor">What Cortana will listen for to Activate the Command</param>
        /// <param name="AppNameLocation">Specified to indicate whether the value of the AppName element can be prepended, appended, or used inline with the ListenFor element.</param>
        public ListenStatement(string ListenFor, RequireAppName AppNameLocation = RequireAppName.BeforePhrase)
        {
            this.ListenFor = ListenFor;
            this.AppNameLocation = AppNameLocation;
        }

        /// <summary>
        /// What Cortana will listen for to Activate the Command
        /// </summary>
        public string ListenFor { get; set; }
        /// <summary>
        /// Specified to indicate whether the value of the AppName element can be prepended, appended, or used inline with the ListenFor element.
        /// </summary>
        public RequireAppName AppNameLocation { get; set; }
    }
}
