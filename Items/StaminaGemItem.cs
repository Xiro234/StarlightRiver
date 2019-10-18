using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public class StaminaGemItem : QuickTileItem { public StaminaGemItem() : base("Stamina Gem", "Use any ability on this to gain stamina!\n5 second cooldown", ModContent.TileType<Tiles.StaminaGem>(), 8) { } }
    public class StaminaOrbItem : QuickTileItem
    {
        public StaminaOrbItem() : base("Stamina Orb", "Pass through this to gain stamina!\n5 second cooldown", ModContent.TileType<Tiles.StaminaOrb>(), 8) { }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Itemname);
            Tooltip.SetDefault(Itemtooltip);
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(3, 6));
        }
    }
}
