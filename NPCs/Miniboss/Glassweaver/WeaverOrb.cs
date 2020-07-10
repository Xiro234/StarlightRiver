using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarlightRiver.NPCs.Miniboss.Glassweaver
{
    class WeaverOrb : ModNPC
    {
        internal ref float Timer => ref npc.ai[0];

        public override void SetDefaults()
        {
            npc.lifeMax = 100;
            npc.width = 128;
            npc.height = 128;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.noTileCollide = true;
            npc.damage = 20;
            npc.knockBackResist = 0;
        }

        public override void AI()
        {
            Timer++;
            npc.position.Y += (float)Math.Sin(Timer / 120f * 6.28f) * 2.3f;
            npc.rotation += 0.2f;

            if (Timer > 200) //suicide timer
            {
                npc.Kill();
                for (int k = 0; k < 10; k++) Projectile.NewProjectile(npc.Center, Vector2.One.RotatedBy(k / 10f * 6.28f), ProjectileType<WeaverSword>(), 5, 1);
            }
        }
    }
}
