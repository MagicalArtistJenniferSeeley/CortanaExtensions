using System;
using CortanaExtensions.Common;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using CortanaExtensions.Exceptions;
using System.Linq;
using CortanaExtensions.Models;

namespace CortanaExtensions
{
    /// <summary>
    /// Base Class for the VoiceCommandDefinitions File, this is the starting point to build from. Contains between 1 and 15 CommandSet elements, each of which represents the voice commands for a single language.
    /// </summary>
    public class VoiceCommandDefinition : Structure
    {
        public VoiceCommandDefinition(IEnumerable<CommandSet> CommandSets = null)
        {
            VCD = new XDocument(Declaration);
            Root = new XElement(Schema + "VoiceCommands");
            if (CommandSets != null) ((List<CommandSet>)this.CommandSets).AddRange(CommandSets);
        }

        private XElement Root { get; set; }
        private XDocument VCD { get; set; }

        /// <summary>
        /// Add Command Sets for each language you want to support
        /// </summary>
        public IList<CommandSet> CommandSets { get; set; } = new List<CommandSet>();

        /// <summary>
        ///  Returns the raw XML of the VCD file
        ///  </summary>
        public XDocument GetXml()
        {
            Build();
            return VCD;
        }

        /// <summary>
        /// Builds the File parts
        /// </summary>
        private void Build()
        {
            foreach (var commandset in CommandSets)
            {
                Root.Add(commandset.GetElement());
            }
            VCD.Add(Root);

            if (!CommandSets.Any()) throw new Exception("Voice Command Definitions require at least 1 Command Set");
            else if (CommandSets.Count > 15) throw new FieldOverflowException(CommandSets, "Voice Command Definitions can only have up to 15 Languages");
        }

        /// <summary>
        /// Saves VCD to Storage File of your creation, ensure that it replaces itself
        /// </summary>
        /// <param name="File">File to save to</param>
        public async Task SaveToFile(StorageFile File) { Build(); using (var stream = await File.OpenStreamForWriteAsync()) { VCD.Save(stream); } }

        /// <summary>
        /// Creates the VCD file in the default location (VCD.xml in the LocalState Folder), Then it installs the file into Cortana
        /// </summary>
        public async Task CreateAndInstall()
        {
            StorageFolder Local = ApplicationData.Current.LocalFolder;
            StorageFile File = await Local.CreateFileAsync("VCD.xml", CreationCollisionOption.ReplaceExisting);
            await SaveToFile(File);
            await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(File);
        }

        public override string ToString()
        {
            Build(); return VCD.ToString();
        }
    }
}