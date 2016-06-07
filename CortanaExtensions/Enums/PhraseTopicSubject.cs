using CortanaExtensions.Common;

namespace CortanaExtensions.Enums
{
    /// <summary>
    /// The Subject child elements specify a subject specific to the Scenario attribute of the parent PhraseTopic to further refine the relevance of speech recognition results within spoken commands using the PhraseTopic. Subjects will be evaluated in the order provided and, when appropriate, later-specified subjects will constrain earlier-specified ones.
    /// </summary>
    public enum PhraseTopicSubject
    {
        [Description("Date/Time")]
        DateTime,
        [Description("Addresses")]
        Addresses,
        [Description("City/State")]
        CityORState,
        [Description("Person Names")]
        PersonNames,
        [Description("Movies")]
        Movies,
        [Description("Music")]
        Music,
        [Description("Phone Number")]
        PhoneNumber
    }
}
