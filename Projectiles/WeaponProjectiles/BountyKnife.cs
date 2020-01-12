using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    class BountyKnife : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 1000;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bounty Knife");
        }

        public override void AI()
        {
            Dust.NewDustPerfect(projectile.Center, ModContent.DustType<Dusts.Starlight>(), Vector2.Zero);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.boss && !target.dontTakeDamage && !target.immortal && !target.friendly)
            {
                /*if (target.aiStyle == 1) NPC.NewNPC((int)target.position.X, (int)target.position.Y, ModContent.NPCType<NPCs.Beasts.SlimeBeast>()); 
                else if (!target.noGravity && !target.noTileCollide) NPC.NewNPC((int)target.position.X, (int)target.position.Y, ModContent.NPCType<NPCs.Beasts.GroundBeast>());
                else if (target.noGravity && !target.noTileCollide) NPC.NewNPC((int)target.position.X, (int)target.position.Y, ModContent.NPCType<NPCs.Beasts.AirBeast>());
                else if (target.noGravity && target.noTileCollide) NPC.NewNPC((int)target.position.X, (int)target.position.Y, ModContent.NPCType<NPCs.Beasts.PhaseBeast>());
                else NPC.NewNPC((int)target.position.X, (int)target.position.Y, ModContent.NPCType<NPCs.Beasts.FailsafeBeast>()); //probably stupid rare*/
            }
        }       
    }
}
