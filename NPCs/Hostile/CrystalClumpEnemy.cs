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
                    npc.velocity += target.velocity * 0.5f;
                }

                mp.dashcd = 1;
            }
            //0 = going to player
            //1 = pulling player in
            //2 = player is close enough, stops the player for a while and then throws them away
            //3 = dashed through, stunned
            if (npc.ai[3] == 0)
            {
                npc.aiStyle = 14;
                if (npc.ai[1] >= 0)
                {
                    for (int k = 0; k <= Main.ActivePlayersCount; k += 1)
                    {
                        float minDistance = 180f;
                        Player player = Main.player[k];
                        Vector2 vectorToPlayer = npc.Center - player.Center;
                        float distanceToPlayer = vectorToPlayer.Length();
                        if (distanceToPlayer <= minDistance)
                        {
                            npc.ai[3] = 1;
                        }
                    }
                }
                else
                {
                    npc.ai[1] += 1;
                }
            }
            if (npc.ai[3] == 1)
            {
                npc.aiStyle = -1;
                npc.velocity *= 0.5f;
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
                        player.velocity = -(Direction * 4f);
                    }
                    if (distanceToPlayer <= minDistance)
                    {
                        npc.ai[3] = 2;
                        pulledPlayer = player;
                    }
                }
            }
            if (npc.ai[3] == 2)
            {
                npc.aiStyle = -1;
                pulledPlayer.velocity = new Vector2(0f, 0f);
                pulledPlayer.gravity = 0f;
                pulledPlayer.wingTime = 0;
                npc.ai[1] += 1;
                if (npc.ai[1] >= 60f)
                {
                    Vector2 vectorToPlayer = npc.Center - pulledPlayer.Center;
                    float distanceToPlayer = vectorToPlayer.Length();
                    Vector2 Direction = new Vector2(pulledPlayer.Center.X - npc.Center.X, pulledPlayer.Center.Y - npc.Center.Y);
                    Direction.Normalize();
                    pulledPlayer.velocity = (Direction * 18f);
                    npc.ai[1] = -300;
                    npc.ai[3] = 0;
                    pulledPlayer = null;
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
