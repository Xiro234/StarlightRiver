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
            npc.defense = 15;
            npc.lifeMax = 90;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 10f;
            npc.knockBackResist = 0.2f;
            npc.aiStyle = 14;
        }
        int sucktime = 0;
        bool cansuck = true;
        public override void HitEffect(int hitDirection, double damage)
        {
            Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y, 41); //granite golem
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 30); //ice materialize
            if (npc.life <= 0)
            {
                Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y, 5); //ice
            }
            else
            {

                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 50); //ice mine
            }
            base.HitEffect(hitDirection, damage);
        }
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
            for (int k = 0; k <= 200; k += 1)
            {
                NPC wisp = Main.npc[k];
                Vector2 wispDistance = wisp.Center - npc.Center;
                if (wisp.type == mod.NPCType("DesertWisp") || wisp.type == mod.NPCType("DesertWisp2"))
                {
                    if (wispDistance.Length() <= 240 && cansuck)
                    {
                        wisp.velocity = Vector2.Normalize(distance) * -7;
                    }
                }
            }
            if (distance.Length() <= 180 && cansuck)
            {
                npc.velocity = Vector2.Zero;
                npc.defense = 0;
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
                npc.defense = 15;
            }
            else
            {
                npc.defense = 15;
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
        public override void FindFrame(int frameHeight)
        {
            int frame = 1;
            if (cansuck)
            {
                frame = 0;
            }
            npc.frame.Y = frame * frameHeight;
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
