using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Ability
{
    class DashFlame : Dash
    {
        Mod mod = StarlightRiver.Instance;

        public DashFlame() : base()
        {

        }

        public override void OnCast()
        {
            Active = true;
            Main.PlaySound(SoundID.Item45);
            Main.PlaySound(SoundID.Item104);

            Projectile proj = Main.projectile[Projectile.NewProjectile(new Vector2(player.position.X - 21, player.position.Y-12), Vector2.Zero, mod.ProjectileType<Projectiles.Ability.DashFire>(), 10, 1f)];
            proj.owner = player.whoAmI;

            X = ((player.controlLeft) ? -1 : 0) + ((player.controlRight) ? 1 : 0);
            Y = ((player.controlUp) ? -1 : 0) + ((player.controlDown) ? 1 : 0);
            timer = 5;
        }

        public override void InUse()
        {
            player.maxFallSpeed = 999;

            timer--;

            if (X != 0 || Y != 0)
            {
                player.velocity = Vector2.Normalize(new Vector2(X, Y)) * 45;
            }

            if(timer <= 0)
            {
                Active = false;
                OnExit();               
            }
        }
        public override void UseEffects()
        {
            for (int k = 0; k <= 15; k++)
            {
                Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, DustID.Fire);
                Dust.NewDustPerfect(player.Center + Vector2.Normalize(player.velocity) * Main.rand.Next(0, 50), mod.DustType<Dusts.FireDust>(), -player.velocity*Main.rand.NextFloat(-2, 5) + new Vector2(Main.rand.Next(-1, 2), Main.rand.Next(-1, 2)),0, default, 2f);
            }
        }

        public override void OnExit()
        {
            player.velocity.X *= 0.15f;
            player.velocity.Y *= 0.15f;
        }
    }
}
