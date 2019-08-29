using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spritersguildwip.Ability;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Tiles
{
    class Dark : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = false;
            drop = mod.ItemType("voidgoo");
            AddMapEntry(new Color(0, 0, 0));

            animationFrameHeight = 88;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.LocalPlayer;
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            if ((Vector2.Distance(player.position, mp.start) >= mp.objective.Length() || ((player.position - player.oldPosition).Length() < 14) && mp.shadowcd <= 3))
            {
                Main.tile[i, j].inActive(false);
            }
            else
            {
                Main.tile[i, j].inActive(true);
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
