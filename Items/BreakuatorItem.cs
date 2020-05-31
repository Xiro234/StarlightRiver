using Microsoft.Xna.Framework;
using StarlightRiver.Tiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    internal class BreakuatorItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = ItemRarityID.Blue;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Breakuator");
            Tooltip.SetDefault("Breaks tiles when powered");
        }

        public override bool UseItem(Player player)
        {
            if (Vector2.Distance(player.Center, Main.MouseWorld) <= 500)
            {
                Breakuator.breakuator.Add(new Point16((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16));
            }
            return true;
        }
    }
}