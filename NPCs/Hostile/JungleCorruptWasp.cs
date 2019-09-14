using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Ability;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Hostile
{
    class JungleCorruptWasp : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Rotter");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.knockBackResist = 0f;
            npc.height = 32;
            npc.lifeMax = 10;
            npc.HitSound = SoundID.NPCHit8;
            npc.DeathSound = SoundID.NPCDeath12;
            npc.noGravity = true;
            npc.damage = 55;
            npc.aiStyle = -1;
        }

        public override void AI()
        {
            npc.TargetClosest(true);

            if(npc.localAI[1] == 0)
            {
                float r = npc.localAI[0] / 60 * 6.28f;
                if (Vector2.Distance(npc.Center, Main.player[npc.target].Center) >= 80)
                {
                    npc.velocity = Vector2.Normalize(npc.Center - Main.player[npc.target].Center + new Vector2(0, 30)) * -3 + new Vector2((float)Math.Cos(r), (float)Math.Sin(r + 3.14));
                }
                else
                {
                    npc.velocity = new Vector2((float)Math.Cos(r), (float)Math.Sin(r + 3.14));
                }
            }

            npc.localAI[0]++;
            if (npc.localAI[0] >= 180 && Vector2.Distance(npc.Center, Main.player[npc.target].Center) <= 120 && npc.localAI[1] == 0)
            {
                npc.localAI[1] = 1;
                npc.localAI[0] = 180;
            }
            else if(npc.localAI[0] >= 240 && npc.localAI[1] == 0)
            {
                npc.localAI[1] = 0;
            }

            if(npc.localAI[1] == 1)
            {
                npc.velocity *= 0;
                if (npc.localAI[0] % 10 == 0)
                {
                    Projectile.NewProjectile(npc.Center, new Vector2(npc.direction * 3, -1.5f), mod.ProjectileType("GasCurse"), 20, 0.2f);
                }

                if (npc.localAI[0] >= 240)
                {
                    npc.localAI[0] = 0;
                    npc.localAI[1] = 0;
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneRockLayerHeight && !Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].active() && spawnInfo.player.GetModPlayer<BiomeHandler>(mod).ZoneJungleCorrupt) ? 1f : 0f;
        }

        public int Framecounter = 0;
        public int Gameframecounter = 0;
        public override void FindFrame(int frameHeight)
        {
            if (Gameframecounter++ == 6)
            {
                Framecounter++;
                Gameframecounter = 0;
            }
            npc.frame.Y = 36 * Framecounter;
            if (Framecounter >= 3)
            {
                Framecounter = 0;
            }
        }
    }
    public class GasCurse : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 8;
            projectile.height = 8;
            projectile.penetrate = -1;
            projectile.timeLeft = 180;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.damage = 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Spray");
        }
        public override void AI()
        {
            Dust.NewDust(projectile.position, (int)(projectile.width), (int)(projectile.height), 75);
            projectile.localAI[1]++;
            projectile.Hitbox = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, (int)(8 * projectile.localAI[0]), (int)(8 * projectile.localAI[0]));

            if (projectile.localAI[1] >= 10)
            {
                projectile.localAI[0]++;
                projectile.localAI[1]=0;
            }
            projectile.velocity.Y += 0.05f;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 300);
        }
    }
}
