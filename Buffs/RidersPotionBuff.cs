using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Buffs
{
    public class RidersPotionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Riders Potion");
            Description.SetDefault("Increased critical strike chance while mounted");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.mount.Active)
            {
                player.thrownCrit += 25;
                player.rangedCrit += 25;
                player.meleeCrit += 25;
                player.magicCrit += 25;
            }
        }
    }
}
