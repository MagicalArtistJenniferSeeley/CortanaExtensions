using System;
using CortanaExtensions.Common;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace CortanaExtensions
{
    /// <summary>
    /// Base Class for the VoiceCommandDefinitions File, this is the starting point to build from.
    /// </summary>
    public class VoiceCommandDefinition : Structure
    {
        public VoiceCommandDefinition(IList<CommandSet> CommandSets = null)
        {
            VCD = new XDocument(Declaration);
            root = new XElement(Schema + "VoiceCommands");
            this.CommandSets = CommandSets;
        }
        private XElement root { get; set; }
        private XDocument VCD { get; set; }
        /// <summary>
        /// Add Command Sets for each language you want to support
        /// </summary>
        public IList<CommandSet> CommandSets { get; set; }

        /// <summary>
        ///  Returns the raw XML of the VCD file        
        ///  </summary>
        public XDocument getXml()
        {
            foreach (var commandset in CommandSets)
            {
                root.Add(commandset.getElement());
            }
            VCD.Add(root);
            return VCD;
        }

        public async Task SaveToFile(StorageFile File) { using (var stream = await File.OpenStreamForWriteAsync()) { VCD.Save(stream); } }
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
        public override string ToString() { return VCD.ToString(); }
    }
}
