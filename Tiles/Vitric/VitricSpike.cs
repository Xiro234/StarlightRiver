using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace StarlightRiver.Tiles.Vitric
{
    class VitricSpike : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            minPick = int.MaxValue;
            soundStyle = SoundID.CoinPickup;
            dustType = ModContent.DustType<Dusts.Air>();

            Main.tileMerge[Type][ModContent.TileType<VitricSand>()] = true;
        }
        public override bool Dangersense(int i, int j, Player player) { return true; }
        public override void FloorVisuals(Player player)
        {
            player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " thought glass shards would be soft..."), 25, 0);
            player.velocity.Y -= 5;
        }
    }
}
