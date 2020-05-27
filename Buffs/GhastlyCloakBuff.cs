using Microsoft.Xna.Framework;
using StarlightRiver.Projectiles;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Buffs
{
    public class GhastlyCloakBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Cloaked");
            Description.SetDefault("Increases most stats");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.immuneAlpha += 80;
            player.lifeRegen++;
            player.statDefense += 2;
            player.allDamage += 0.05f;
            player.moveSpeed += 0.4f;
            if (Main.rand.NextBool())
            {
                Dust.NewDust(player.position, player.width, player.height, 62);
            }
        }
    }
}
