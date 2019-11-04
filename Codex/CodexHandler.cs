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
        public static List<CodexEntry> Entries;

        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(CodexState)] = CodexState
            };
        }

        public override void Load(TagCompound tag)
        {
            CodexState = tag.GetInt(nameof(CodexState));
        }
    }
}
