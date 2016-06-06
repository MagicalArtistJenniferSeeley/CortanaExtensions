using System.ComponentModel;

namespace CortanaExtensions.Enums
{
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
