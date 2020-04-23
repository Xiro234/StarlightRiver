using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Guardian
{
    class ExampleTome : Tome
    {
        public ExampleTome() : base(ModContent.ProjectileType<ExampleTomeProjectile>(), 1, 128, 75) { }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ent's Tome");
        }
        public override void EffectTooltip(List<TooltipLine> tooltips)
        {
            tooltips.Insert(1, new TooltipLine(mod, "Effect", "+" + 5 * Main.LocalPlayer.GetModPlayer<StarlightPlayer>().GuardDamage + "% damage, x2 for guardian damage"));
        }
    }
    class ExampleTomeProjectile : TomeProjectile
    {
        public override void SetDefaults()
        {
            projectile.timeLeft = 600;
        }
        public override void BoostPlayer(Player player)
        {
            player.allDamageMult += 0.05f * projectile.ai[0];
            player.GetModPlayer<StarlightPlayer>().GuardDamage += 0.05f * projectile.ai[0];
            Main.NewText(player.GetModPlayer<StarlightPlayer>().GuardDamage);
        }
        public override void SafeAI()
        {
            if (Main.rand.Next(3) == 0) Dust.NewDustPerfect(projectile.Center + Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(projectile.ai[1]), ModContent.DustType<Dusts.Starlight>(), new Vector2(0, -4));
        }
    }
}
