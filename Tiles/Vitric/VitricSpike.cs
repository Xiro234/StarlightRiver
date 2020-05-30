using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Vitric
{
    internal class VitricSpike : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            minPick = int.MaxValue;
            soundStyle = SoundID.CoinPickup;
            dustType = ModContent.DustType<Dusts.Air>();
            AddMapEntry(new Color(95, 162, 138));

            Main.tileMerge[Type][ModContent.TileType<VitricSand>()] = true;
        }

        public override bool Dangersense(int i, int j, Player player)
        {
            return true;
        }

        public override void FloorVisuals(Player player)
        {
            player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " thought glass shards would be soft..."), 25, 0);
            player.velocity.Y -= 5;
        }
    }
}