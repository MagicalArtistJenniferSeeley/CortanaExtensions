# CortanaExtensions
Generate a Voice Command Definition (VCD) file for Cortana in Windows 10 via C#, using Intellisense, instead of having to write XML.

**This Library is inspired by the NotificationsExtensions Library from Microsoft.**

It is still worth looking over the Documentation for how to define Cortana Strings, visit: https://msdn.microsoft.com/en-us/windows/uwp/input-and-devices/support-natural-language-voice-commands-in-cortana for more info.

---

The main intention for this Library is to enable Developers to easily create CommandSets for multiple languages, the modularity of a CommandSet in code is that in combination with the MultiLingual toolkit, you can can send an XLF to Translators with instructions on creating Natural language commands for Cortana, and re-use your current translation flow.

Using this Library, the VCD file will be built at Runtime everytime, you could however, either, save it, and check if it exists, or you could use it to generate it in code outside your app, and copy the output file to your app directory.

**Here is an Example of how to use CortanaExtensions to build a VCD file:**
```C#
List<string> Places = new List<string> { "Auckland", "Wellington", "Christchurch" };
var VCD = new VoiceCommandDefinition()
{
	CommandSets =
	{
		new CommandSet("en")
		{
			Example = "Book trips to Destinations",
			AppName = "TravelPlan",
			Name = "TravelPlanCommands(English)",
			Commands =
			{
				new ForegroundActivatedVoiceCommand("Command")
				{
					Example = "Book a Trip to Wellington",
					Feedback = "Booking Trip",
					ListenStatements =
					{
						new ListenStatement("Book [a] Trip [to] {Places}", RequireAppName.BeforeOrAfterPhrase),
						new ListenStatement("[I] [would] [like] [to] Book [a] [my] [Holiday] [Trip] to {searchTerm}",
						RequireAppName.BeforeOrAfterPhrase)
					},
					AppTarget = "BOOKTRIP"
				}
			},
			PhraseLists =
			{
				new PhraseList("Places", Places)
			},
			PhraseTopics =
			{
				new PhraseTopic("searchTerm", PhraseTopicScenario.Search)
				{
					Subjects =
					{
						PhraseTopicSubject.CityORState
					}
				}
			}
		}
	}
};
await VCD.CreateAndInstall();
```
---

**To make the most of this Library, use MultiLingual Toolkit.**

**Here is some example code, using Multilingual Toolkit to generate different Languages in a VCD:**
```C#
public static async void BuildAndInstallCortanaVoiceCommands()
{
	var VCD = new VoiceCommandDefinition()
	{
		CommandSets =
		{
			AddLanguageSet("en"),
			AddLanguageSet("es")
		}
	};
	await VCD.CreateAndInstall();
}

public static CommandSet AddLanguageSet(string langCode)
{
	try
	{
		Strings.Culture = new System.Globalization.CultureInfo(langCode);
		var CommandSet = new CommandSet(langCode)
		{
			Name = $"TVPLANCommands({langCode})",
			AppName = Strings.APP_Name,
			Example = Strings.CortanaTVPLANExample,
			Commands =
			{
				new BackgroundActivatedVoiceCommand("MarkEpisodeWatched")
				{
					Example = Strings.CortanaMarkWatchedExample,
					ListenStatements = CortanaExtensions.Common.ValueInterpreter.DeserialiseListenStatement(Strings.CortanaMarkWatchedListenForGroups),
					Feedback = Strings.CortanaMarkWatchedResponse,
					BackgroundTarget = "Tasks.CortanaResponse.MarkWatched"
				},
				new BackgroundActivatedVoiceCommand("PlayTrailer")
				{
					Example = Strings.CortanaPlayTrailerExample,
					ListenStatements = CortanaExtensions.Common.ValueInterpreter.DeserialiseListenStatement(Strings.CortanaPlayTrailerListenForGroups),
					Feedback = Strings.CortanaPlayTrailerFeedback,
					BackgroundTarget = "Tasks.CortanaResponse.PlayTrailer"
				},
				new ForegroundActivatedVoiceCommand("SEARCH")
				{
					Example = Strings.CortanaSearchExample,
					ListenStatements = CortanaExtensions.Common.ValueInterpreter.DeserialiseListenStatement(Strings.CortanaSearchListenForGroups),
					Feedback = Strings.CortanaSearchFeedback
				}
			},
			PhraseTopics =
			{
				new PhraseTopic("searchResult", PhraseTopicScenario.Search)
			}
		};
		((List<PhraseList>)CommandSet.PhraseLists).AddRange(CortanaExtensions.Common.ValueInterpreter.DeserialisePhraseList(Strings.CortanaPhraseLists));
		Strings.Culture = TranslationManager.AppCulture;
		return CommandSet;
	}
	catch
	{
		Strings.Culture = TranslationManager.AppCulture;
		return null;
	}
}
```

This code produces this resulting [XML](http://puu.sh/pjTyT/d026cf7090.PNG), Strings produced from the ResX included below.

Custom Markup Format
=======

This Code uses a Custom Markup format to Deserialise a String for use in ListenFor and PhraseList cases, these can be very useful if you want the translator to create different ways of Saying a command that feels natural for that language. To help the translators, either copy and modify the below Descriptions, or link to this section using "tinyurl.com/CortanaCustomMarkup" in your XLF or ResX comments.

**ListenForGroups (Example shown for Search terms):**
Create multiple different suggestions for what Cortana can Listen for to register a Command. 
To build a suggestion, use square brackets '[]' to define words that are unnecessary to register the Commmand, these can be stacked, such as if you want to use multiple Determiners, such as "[my] [the]", which can be registered as my or the, or not at all. 

The search term spoken is registered by adding {searchResult} to a part of the string. 

Separated by " \/ " is the position of the app's name to be spoken by the user. The possible values are "BeforePhrase", "AfterPhrase", "BeforeOrAfterPhrase" or "ExplicitlySpecified". To use ExplicitlySpecified, you must insert "{builtin:AppName}" into your string. To use Expilictly specified or AfterPhrase or BeforeOrAfterPhrase, you must have at least 2 words inluding items in {}, but not including words that are surrounded in square brackets. 

To separate suggestions use " #!# ". Optional words with Square Brackets cannot be nested (e.g. Square brackets over a phrase list). 

Look at https://tinyurl.com/CortanaInteraction for more info on creating useful variations of commands using ListenForGroups.

Example of Implementation:
`Search [for] {searchResult} \/ BeforeOrAfterPhrase #!# Find {searchResult} \/ BeforeOrAfterPhrase #!# Look for {searchResult} \/ BeforeOrAfterPhrase #!# Look up {searchResult} \/ BeforeOrAfterPhrase #!# [Query] {searchResult} \/ BeforePhrase`

**Maximum of 10 ListenForGroups**

**PhraseLists:**
Add custom PhraseLists, these work in combination with ListenForGroups to help enhance vocabulary and natural language. You can use these to allow very similarly expressed words to be in the same command, such as "Play the Trailer", and "Show the Trailer". 

Use " \/ " to Separate the Name of the phrase list from the the values inside. Different Phrases are separated using "\". Use " #!# " to separate different Phrase lists. To use these phrases in a ListenForGroup use {} curly braces, e.g. {PlayVariations}.

Example of implementation: 
`PlayVariations \/ Play\Show\Display\Show me\Find #!# Series \/ Game of Thrones\Dexter\Arrested Development`

**Here is an example of a ResX file ready for Translation:**
![alt Example](http://puu.sh/pjRVb/f7acae2fac.PNG)

**Notes**  
If you encounter any issues or have a suggestion: don't hesitate to open a ticket, you will be helping me too.  
Should you wish to do so, you can contact me at `williamabradley@outlook.com`.
