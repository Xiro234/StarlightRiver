using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
{
    class VoidGoo : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = false;
            drop = mod.ItemType("VoidGoo");
            dustType = mod.DustType("Darkness");
            AddMapEntry(new Color(0, 0, 0));
            
            animationFrameHeight = 88;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.LocalPlayer;
            if (player.GetModPlayer<AbilityHandler>().ability is Superdash && player.GetModPlayer<AbilityHandler>().ability.Active)
            {
                Main.tile[i, j].inActive(true);
            }
            else
            {
                Main.tile[i, j].inActive(false);
            }
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 5)
            {
                frameCounter = 0;
                if (++frame >= 3)
                {
                    frame = 0;
                }
            }
        }
    }
}
