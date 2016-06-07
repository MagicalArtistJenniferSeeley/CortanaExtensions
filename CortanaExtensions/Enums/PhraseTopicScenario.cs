using CortanaExtensions.Common;

namespace CortanaExtensions.Enums
{
    /// <summary>
    /// The Scenario attribute (default "Dictation") specifies the desired scenario for this PhraseTopic, 
    /// which may optimize the underlying speech recognition of voice commands using the PhraseTopic to produce results that are better-suited to the desired context of the command.
    /// </summary>
    public enum PhraseTopicScenario
    {
        [Description("Natural Language")]
        NaturalLanguage,
        [Description("Search")]
        Search,
        [Description("Short Message")]
        ShortMessage,
        [Description("Dictation")]
        Dictation,
        [Description("Commands")]
        Commands,
        [Description("Form Filling")]
        FormFilling
    }
}
