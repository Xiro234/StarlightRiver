using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Hostile
{
    class CrystalMage : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glassweaver");
        }
        public override void SetDefaults()
        {
            npc.width = 48;
            npc.knockBackResist = 0f;
            npc.height = 48;
            npc.lifeMax = 45;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.aiStyle = -1;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            Player target = Main.player[npc.target];
            if (npc.localAI[0] < 6.28) { npc.localAI[0] += 0.1f; } else { npc.localAI[0] = 0; }
            if (npc.localAI[1] <= 300) { npc.localAI[1]++; } else { npc.localAI[1] = 0; }

            for (int k = 1; k <= Vector2.Distance(npc.Center, target.Center); k++)
            {
                Vector2 check = Vector2.Lerp(npc.Center, target.Center, k / Vector2.Distance(npc.Center, target.Center));
                if (Main.tile[(int)(check.X / 16), (int)(check.Y / 16)].collisionType != 0 && Main.tile[(int)(check.X / 16), (int)(check.Y / 16)].type != TileID.Trees)
                {
                    npc.localAI[2] = 1;
                }
            }

            npc.position.Y += (float)Math.Sin(npc.localAI[0]);
            if (npc.localAI[2] == 0) { npc.velocity = Vector2.Normalize(npc.Center - target.Center) * -1.1f; }
            else { npc.velocity *= 0.98f; }

            if (npc.localAI[1] == 300)
            {
                if (npc.localAI[2] == 0)
                {
                    Projectile.NewProjectile(npc.Center, -Vector2.Normalize(npc.Center - target.Center) * 5, ModContent.ProjectileType<Projectiles.SandBolt>(), 5, 0.5f);
                }
                else
                {
                    //spawn a circle on the player
                    //int proj = Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<Boss.Aura>(), 5, 0);
                    //Projectile circle = Main.projectile[proj];
                    //circle.localAI[0] = 120;
                    //circle.localAI[1] = 50;

                    for (int k = 0; k <= Vector2.Distance(npc.Center, target.Center); k += 5)
                    {
                        Vector2 check = Vector2.Lerp(npc.Center, target.Center, (k / Vector2.Distance(npc.Center, target.Center)));
                        Dust.NewDust(check, 8, 8, ModContent.DustType<Dusts.Air4>());
                    }
                }
            }

            npc.localAI[2] = 0;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneRockLayerHeight && spawnInfo.player.GetModPlayer<BiomeHandler>().ZoneGlass) ? 0.4f : 0f;
        }
    }
}
