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
            npc.damage = 15;
            Main.npcFrameCount[npc.type] = 3;
            npc.defense = 5;
            npc.lifeMax = 60;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 10f;
            npc.knockBackResist = 0.2f;
            npc.aiStyle = 14;
        }

        int sucktime = 0;
        bool cansuck = true;
        public override void AI()
        {
            npc.TargetClosest(true);
            Player target = Main.player[npc.target];
            AbilityHandler mp = target.GetModPlayer<AbilityHandler>();
            Vector2 distance = target.Center - npc.Center;

            if (cansuck && Main.rand.Next(2) == 0)
            {
                float rand = Main.rand.NextFloat(0, (float)Math.PI * 2);
                Vector2 dustPos = npc.Center + new Vector2((float)Math.Sin(rand), (float)Math.Cos(rand)) * 180;
                Dust.NewDustPerfect(dustPos, mod.DustType("Air"), Vector2.Normalize(dustPos - npc.Center) * -2, 0, default, 0.6f);
            }

            if (distance.Length() <= 180 && cansuck)
            {
                npc.velocity = Vector2.Zero;
                target.velocity = Vector2.Normalize(distance) * -5;
                if (sucktime % 20 == 0)
                {
                    for (float k = 0; k <= Math.PI * 2; k += (float)Math.PI / 40)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            Vector2 dustPos2 = npc.Center + new Vector2((float)Math.Sin(k), (float)Math.Cos(k)) * 180;
                            Dust.NewDustPerfect(dustPos2, mod.DustType("Air"), Vector2.Normalize(dustPos2 - npc.Center) * -2, 0, default, 0.8f);
                        }
                    }
                }
                sucktime++;
            }
            else if (sucktime > 0)
            {
                sucktime--;
            }

            if(sucktime >= 180)
            {
                cansuck = false;
            }

            if (sucktime <= 0 && !cansuck)
            {
                cansuck = true;
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
