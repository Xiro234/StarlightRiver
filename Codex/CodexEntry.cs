using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlightRiver.Codex
{
    class CodexEntry
    {
        public bool Locked = false;
        public bool RequiresUpgradedBook = false;
        public int Category;

        public string Title;
        public string Body;
        public Texture2D Image;

        public enum Categories
        {
            Abilities = 0,
            Biomes = 1,
            Relics = 2,
            Bosses = 3,
            RiftCrafting = 4,
            Misc = 5          
        }
    }
}
