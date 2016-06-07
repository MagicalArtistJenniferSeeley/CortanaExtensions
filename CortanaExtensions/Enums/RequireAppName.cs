namespace CortanaExtensions.Enums
{
	/// <summary>
	/// Specified to indicate whether the value of the AppName element can be prepended, appended, or used inline with the ListenFor element. 
	/// </summary>
	public enum RequireAppName
    {
        /// <summary>
        /// The user must say the AppName before the ListenFor phrase.
        /// </summary>
        BeforePhrase,
        /// <summary>
        /// The user must say "In|On|Using|With" AppName after the ListenFor phrase.
        /// </summary>
        AfterPhrase,
        /// <summary>
        /// The user must say the AppName before or after the ListenFor phrase.
        /// </summary>
        BeforeOrAfterPhrase,
        /// <summary>
        /// The AppName is explicitly referenced in the ListenFor, using {builtin:AppName}. The user is not required to say the AppName before or after the ListenFor phrase.
        /// </summary>
        ExplicitlySpecified
    }
}
