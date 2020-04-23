using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
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
            List<bool> UnlockStates = new List<bool>();
            foreach (CodexEntry entry in Entries) { UnlockStates.Add(entry.Locked); }

            return new TagCompound
            {
                [nameof(CodexState)] = CodexState,
                [nameof(Entries)] = UnlockStates
            };
        }

        public override void Load(TagCompound tag)
        {           
            CodexState = tag.GetInt(nameof(CodexState));

            Entries = new List<CodexEntry>();
            List<bool> UnlockStates = (List<bool>)tag.GetList<bool>(nameof(Entries));

            foreach (Type type in mod.Code.GetTypes().Where(t => t.IsSubclassOf(typeof(CodexEntry))))
            {
                CodexEntry ThisEntry = (CodexEntry)Activator.CreateInstance(type);
                ThisEntry.Locked = (UnlockStates.Count > Entries.Count && UnlockStates.Count != 0) ? UnlockStates.ElementAt(Entries.Count) : true;
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
