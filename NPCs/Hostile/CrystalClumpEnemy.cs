using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spritersguildwip.Ability;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.NPCs.Hostile
{
    class CrystalClumpEnemy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Observer");
        }
        public override void SetDefaults()
        {
            npc.width = 58;
            npc.height = 86;
            npc.damage = 50;
            Main.npcFrameCount[npc.type] = 3;
            npc.defense = 25;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 10f;
            npc.knockBackResist = 0.2f;
            npc.aiStyle = 14;
            npc.noTileCollide = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneOverworldHeight && !Main.dayTime && spawnInfo.player.ZoneDesert) ? 1f : 0f;
        }
        bool shielded = true;
        Player pulledPlayer = null;
        public override void AI()
        {
            npc.TargetClosest(true);
            Player target = Main.player[npc.target];
            AbilityHandler mp = target.GetModPlayer<AbilityHandler>();

            if (npc.Hitbox.Intersects(target.Hitbox) && mp.dashcd > 1)
            {
                if (shielded)
                {
                    shielded = false;
                    npc.ai[3] = 3;
                    npc.ai[1] = 1;
                    npc.ai[2] = 0;
                    npc.velocity += target.velocity * 0.5f;
                }

                mp.dashcd = 1;
            }
            //0 = going to player
            //1 = pulling player in
            //2 = player is close enough, stops the player for a while and then throws them away
            //3 = dashed through, stunned
            npc.ai[0] -= 0.01f;
            if (npc.ai[0] <= 0.2f && npc.ai[0] > 0f)
            {
                npc.ai[0] = 1f;
            }
            if (npc.ai[0] <= -1.2f)
            {
                npc.ai[0] = 1f;
            }
            if (npc.ai[3] != 0)
            {
                npc.ai[0] -= 0.02f;
            }
            if (npc.ai[3] < 2)
            {
                if (npc.ai[0] > 0f && npc.ai[2] >= 0)
                {
                    for (int num1 = 0; num1 <= 20; num1++)
                    {
                        int dustType = mod.DustType("Air");
                        Vector2 dustPos = npc.Center + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((240f * npc.ai[0]) - (num1 * 2));
                        int dust = Dust.NewDust(dustPos - Vector2.One * 8f, 16, 16, dustType, 0f, 0f, 0, default, 0.6f);
                        Main.dust[dust].velocity = -Vector2.Normalize(npc.Center - dustPos) * 1.5f * (4f - num1 * 2f) / 10f;
                        Main.dust[dust].noGravity = true;
                        if (npc.ai[3] == 0)
                        {
                            Main.dust[dust].scale = 0.8f;
                            Main.dust[dust].position += new Vector2((float)Main.rand.NextFloat(), (float)Main.rand.NextFloat());
                        }
                        else
                        {
                            Main.dust[dust].scale = 1.1f;
                        }
                    }
                }
            }
            if (npc.ai[3] == 0 && npc.ai[0] > 0f)
            {
                npc.aiStyle = 14;
                if (npc.ai[2] >= 0)
                {
                    for (int k = 0; k <= Main.ActivePlayersCount; k += 1)
                    {
                        float minDistance = 240f * npc.ai[0];
                        Player player = Main.player[k];
                        Vector2 vectorToPlayer = npc.Center - player.Center;
                        float distanceToPlayer = vectorToPlayer.Length();
                        if (distanceToPlayer <= minDistance)
                        {
                            npc.ai[3] = 1;
                            npc.ai[2] = 1;
                        }
                    }
                }
                else
                {
                    npc.ai[2] += 1;
                }
            }
            if (npc.ai[3] == 1)
            {
                npc.aiStyle = -1;
                npc.velocity *= 0.5f;
                npc.ai[2] += 1;
                for (int k = 0; k <= Main.ActivePlayersCount; k += 1)
                {
                    float maxDistance = 240f;
                    float minDistance = 80f;
                    Player player = Main.player[k];
                    Vector2 vectorToPlayer = npc.Center - player.Center;
                    float distanceToPlayer = vectorToPlayer.Length();
                    if (distanceToPlayer <= maxDistance)
                    {
                        Vector2 Direction = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                        Direction.Normalize();
                        player.velocity = new Vector2(0f, 0f);
                        player.velocity = -(Direction * 3f);
                    }
                    if (npc.ai[2] >= 60)
                    {
                        if (distanceToPlayer <= minDistance)
                        {
                            npc.ai[3] = 2;
                            pulledPlayer = player;
                        }
                    }
                    if (npc.ai[2] >= 360)
                    {
                        npc.ai[3] = 3;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                    }
                }
            }
            if (npc.ai[3] == 2)
            {
                npc.aiStyle = -1;
                pulledPlayer.velocity = new Vector2(0f, 0f);
                pulledPlayer.gravity = 0f;
                pulledPlayer.wingTime = 0;
                npc.ai[2] += 1;
                if (npc.ai[2] >= 120f)
                {
                    Vector2 vectorToPlayer = npc.Center - pulledPlayer.Center;
                    float distanceToPlayer = vectorToPlayer.Length();
                    Vector2 Direction = new Vector2(pulledPlayer.Center.X - npc.Center.X, pulledPlayer.Center.Y - npc.Center.Y);
                    Direction.Normalize();
                    pulledPlayer.velocity = (Direction * 18f);
                    npc.ai[2] = -300;
                    npc.ai[3] = 0;
                    pulledPlayer = null;
                }
            }
            if (npc.ai[3] == 3)
            {
                npc.ai[2] += 1;
                if (npc.ai[2] == 180)
                {
                    if (npc.ai[1] == 0)
                    {
                        usefulMethods.EndMePls(npc);
                    }
                    else
                    {

                    }
                }
            }
            if (shielded)
            {
                npc.immortal = true;
            }
            else
            {
                npc.immortal = false;
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            AbilityHandler mp = target.GetModPlayer<AbilityHandler>();
            if(mp.dashcd == 1)
            {
                target.immune = true;
                target.immuneTime = 5;
            }
        }
    }
}
