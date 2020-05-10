using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace StarlightRiver.Codex
{
    class CodexHandler : ModPlayer
    {
        public int CodexState = 0; //0 = none, 1 = normal, 2 = void
        public List<CodexEntry> Entries = new List<CodexEntry>();

        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(CodexState)] = CodexState,
                [nameof(Entries)] = Entries
            };
        }

        public override void Load(TagCompound tag)
        {
            CodexState = tag.GetInt(nameof(CodexState));

            Entries = new List<CodexEntry>();
            List<TagCompound> entriesToLoad = (List<TagCompound>)tag.GetList<TagCompound>(nameof(Entries));

            foreach (TagCompound tagc in entriesToLoad) Entries.Add(CodexEntry.DeserializeData(tagc));
            foreach (Type type in mod.Code.GetTypes().Where(t => t.IsSubclassOf(typeof(CodexEntry)) && !Entries.Any(n => n.GetType() == t)))
            {
                CodexEntry ThisEntry = (CodexEntry)Activator.CreateInstance(type);               
                Entries.Add(ThisEntry);
            }
        }

        public override void OnEnterWorld(Player player)
        {
            (mod as StarlightRiver).codex = new GUI.Codex();
            (mod as StarlightRiver).customResources8.SetState((mod as StarlightRiver).codex);
        }
    }
}
