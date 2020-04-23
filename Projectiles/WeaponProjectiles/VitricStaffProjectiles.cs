using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.NPCs;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    public class VitricIcicleProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.extraUpdates = 1;
            projectile.timeLeft = 800;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment 2");
        }
        public override void AI()
        {
            
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
        public override void Kill(int timeLeft)
        {

        }
    }
}