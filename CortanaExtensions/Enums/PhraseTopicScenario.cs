using System.ComponentModel;

namespace CortanaExtensions.Enums
{
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
