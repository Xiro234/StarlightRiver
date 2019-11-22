using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss
{
    class VitricBoss : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glass Tax Returns");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 3500;
            npc.damage = 30;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 140;
            npc.height = 143;
            npc.value = Item.buyPrice(0, 20, 0, 0);
            npc.npcSlots = 15f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GlassBoss");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }

        public override bool CheckDead()
        {
            if (!LegendWorld.AnyBossDowned)
            {
                LegendWorld.ForceStarfall = true;
            }

            if (!LegendWorld.GlassBossDowned)
            {
                LegendWorld.GlassBossDowned = true;
                LegendWorld.AnyBossDowned = true;
            }

            return true;
        }

        public override void AI()
        {
            Main.NewText(npc.ai[0] + "/" + npc.ai[1]);
            Player player = Main.player[npc.target];
            npc.ai[1]++; //keeps the timer ticking

            switch (npc.ai[0])
            {
                case 0: CrystalShield(npc, 500, 3, Main.expertMode ? 300 : 150); break;
                case 1: PassiveMovement(0.2f, 2.5f, 180); break;
                case 2: Dash(player.Center); break;
                case 3: FallingRockAttack(); break;
            }
        }

        private void CrystalShield(NPC npc, int time, int count, int maxShield)
        {
            if (npc.ai[1] == 1)
            {
                for (float rot = 0; rot < 6.28f; rot += 6.28f / count)
                {
                    int ward = NPC.NewNPC((int)npc.Center.X + 8, (int)npc.Center.Y + 12, ModContent.NPCType<Ward>());
                    Main.npc[ward].velocity = (new Vector2(6, 0)).RotatedBy(rot);
                }
            }
            if(npc.ai[1] >= time || !Main.npc.Any(T => T.type == ModContent.NPCType<Ward>() && T.active))
            {
                foreach(NPC npc2 in Main.npc.Where(npc2 => npc2.type == ModContent.NPCType<Ward>() && npc2.active))
                {
                    npc.GetGlobalNPC<ShieldHandler>().MaxShield += maxShield / count;
                    npc.GetGlobalNPC<ShieldHandler>().Shield += maxShield / count;
                    Helper.Kill(npc2);
                    for(int k = 0; k <= Vector2.Distance(npc.Center, npc2.Center); k++)
                    {
                        Dust.NewDustPerfect(Vector2.Lerp(npc.Center, npc2.Center, k / Vector2.Distance(npc.Center, npc2.Center)), ModContent.DustType<Dusts.Air>());
                    }
                }
                npc.ai[1] = 0;
                npc.ai[0] = 1;
            }
        }

        private void PassiveMovement(float acceleration, float maxspeed, float attackspeed)
        {
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            npc.velocity += -Vector2.Normalize(npc.Center - player.Center) * acceleration;
            if (npc.velocity.Length() >= maxspeed) npc.velocity = Vector2.Normalize(npc.velocity) * maxspeed;
            npc.ai[1]++;

            if(npc.ai[1] >= attackspeed)
            {
                npc.velocity = Vector2.Zero;
                if ((npc.Center - player.Center).Length() >= 300 && npc.ai[2] < 3)
                {                   
                    npc.ai[0] = 2;
                }
                else
                {
                    npc.ai[0] = 3;
                    npc.ai[2] = 0;
                }
                npc.ai[1] = 0;
            }
        }

        private void Dash(Vector2 target)
        {
            npc.ai[1]++;         
            npc.rotation += (0.05f + npc.velocity.Length() * 0.02f) * npc.direction;

            if (npc.ai[1] == 2)
            {
                for (int k = 0; k <= 100; k++)
                {
                    Dust.NewDustPerfect(npc.Center - Vector2.Normalize(npc.Center - target) * k * 20, ModContent.DustType<Dusts.VitricBossTell>(),
                        Vector2.Normalize(npc.Center - target).RotatedBy((Main.rand.Next(2) == 0 ? 1.58f : -1.58f)) * Main.rand.NextFloat(20f), 0, Color.LightCyan * 0.5f, 3.5f);
                }
            }

            if (npc.ai[1] >= 60 && npc.ai[1] < 90)
            {
                npc.velocity -= Vector2.Normalize(npc.Center - target) * 1.1f;
            }
            if(npc.ai[1] >= 120)
            {
                npc.velocity += Vector2.Normalize(npc.Center - target) * 0.3f;
            }
            if(npc.ai[1] >= 210)
            {
                npc.ai[0] = 1;
                npc.ai[1] = 0;
                npc.ai[2]++;// counts dashes so the boss wont just keep dashing
                npc.velocity = Vector2.Zero;
            }   
            
            if(npc.ai[1] >= 60)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Dusts.Air>());
            }
        }

        private void FallingRockAttack()
        {
            Player player = Main.player[npc.target];

            npc.ai[1]++;
            if(npc.ai[1] == 90)
            {
                Main.LocalPlayer.GetModPlayer<StarlightPlayer>().Shake = 40;
            }
            if(npc.ai[1] == 120)
            {
                for(int k = -5; k <= 5; k++)
                {
                    Projectile.NewProjectile(player.position + new Vector2(k * Main.rand.NextFloat(150, 210), -800 + Main.rand.Next(300)), new Vector2(0, 6), ModContent.ProjectileType<Projectiles.GlassSpike>(), 20, 1);
                }
            }
            if(npc.ai[1] >= 180)
            {
                npc.ai[0] = 1;
                npc.ai[1] = 0;
            }
        }
    }

    class Ward : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Resonant Crystal");
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 48;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 1;
            npc.HitSound = SoundID.Item90;
            npc.DeathSound = SoundID.Item101;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.immortal = true;
            npc.noGravity = true;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            if (AbilityHelper.CheckDash(player, npc.Hitbox))
            {
                player.velocity *= -1.25f;
                mp.dash.Active = false;
                mp.dash.OnExit();
                Helper.Kill(npc);

                for (int k = 0; k <= 20; k++)
                {
                    Dust.NewDust(npc.position, 32, 46, ModContent.DustType<Dusts.Air>());
                    Dust.NewDust(npc.position, 32, 46, ModContent.DustType<Dusts.Glass2>());
                    Main.PlaySound(SoundID.Shatter, npc.Center);
                }
                for (int k = 0; k <= 3; k++)
                {
                    Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/Ward" + k), 1f);
                }
            }

            npc.velocity *= 0.95f;
        }

        public static Texture2D glow = ModContent.GetTexture("StarlightRiver/NPCs/Boss/CrystalGlow");

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(glow, npc.position - Main.screenPosition + new Vector2(0, 3), new Color(255, 255, 255) * (float)Math.Sin(LegendWorld.rottime));
        }
    }

    class HealGem : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.hostile = true;
            projectile.timeLeft = 2;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.timeLeft = 2;
            projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2;
            projectile.velocity *= 1.004f;
            Player player = Main.LocalPlayer; //TODO change to nearest pplayer
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            if (AbilityHelper.CheckDash(player, projectile.Hitbox))
            {
                projectile.timeLeft = 0;
                for (int k = 0; k <= 20; k++)
                {
                    Dust.NewDust(projectile.position, 46, 46, ModContent.DustType<Dusts.Air>());
                    Dust.NewDust(projectile.position, 46, 46, ModContent.DustType<Dusts.Glass2>(),0,0,0,default,1.8f);
                    Main.PlaySound(SoundID.Shatter, projectile.Center);
                }
                projectile.hostile = false;
            }

            if (!Main.npc.Any(npc => npc.type == mod.NPCType("VitricBoss") && npc.active))
            {
                projectile.timeLeft = 0;
            }
        }

        public static Texture2D glow = ModContent.GetTexture("StarlightRiver/NPCs/Boss/CrystalGlow2");

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(glow, projectile.position - Main.screenPosition + new Vector2(23, 23), null, new Color(255, 255, 255) * (float)Math.Sin(LegendWorld.rottime), projectile.rotation, glow.Size() / 2, projectile.scale, 0, 0);
        }
    }

    class Aura : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystaline Magic");
        }
        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.timeLeft = 2;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.timeLeft = 2;
            projectile.localAI[0]--;
            for (float k = 0; k <= 6.28; k += 0.1f)
            {
                if (Main.rand.Next(20) == 0)
                {
                    Dust.NewDustPerfect(projectile.Center + new Vector2((float)Math.Cos(k), (float)Math.Sin(k)) * projectile.localAI[1], ModContent.DustType<Dusts.Air4>(), Vector2.Zero);
                }
            }

            for (int k = 0; k <= projectile.localAI[1] * 1.3f; k += 10)
            {
                float offset = 0.707f * projectile.localAI[1];
                if (Main.rand.Next(20) == 0) { Dust.NewDustPerfect(projectile.Center + new Vector2(-offset + k, -offset), ModContent.DustType<Dusts.Air4>(), Vector2.Zero); }
                if (Main.rand.Next(20) == 0) { Dust.NewDustPerfect(projectile.Center + new Vector2(-offset, -offset + k), ModContent.DustType<Dusts.Air4>(), Vector2.Zero); }
                if (Main.rand.Next(20) == 0) { Dust.NewDustPerfect(projectile.Center + new Vector2(offset - k, offset), ModContent.DustType<Dusts.Air4>(), Vector2.Zero); }
                if (Main.rand.Next(20) == 0) { Dust.NewDustPerfect(projectile.Center + new Vector2(offset , offset - k), ModContent.DustType<Dusts.Air4>(), Vector2.Zero); }

                float p = k * 1.6f;
                if (Main.rand.Next(20) == 0) { Dust.NewDustPerfect(projectile.Center + new Vector2(-projectile.localAI[1] + p/2,  -p/2), ModContent.DustType<Dusts.Air4>(), Vector2.Zero); }
                if (Main.rand.Next(20) == 0) { Dust.NewDustPerfect(projectile.Center + new Vector2(-projectile.localAI[1] + p/2,   p/2), ModContent.DustType<Dusts.Air4>(), Vector2.Zero); }
                if (Main.rand.Next(20) == 0) { Dust.NewDustPerfect(projectile.Center + new Vector2(projectile.localAI[1] - p/2,   -p/2), ModContent.DustType<Dusts.Air4>(), Vector2.Zero); }
                if (Main.rand.Next(20) == 0) { Dust.NewDustPerfect(projectile.Center + new Vector2(projectile.localAI[1] - p/2,    p/2), ModContent.DustType<Dusts.Air4>(), Vector2.Zero); }
            }

            if (projectile.localAI[0] <= 0)
            {
                for (float k = 0; k <= 6.28; k += 0.1f)
                {
                    int slow = Main.rand.Next(26, 30);
                    Dust.NewDust(projectile.Center + new Vector2((float)Math.Cos(k), (float)Math.Sin(k)) * 20, 1, 1, ModContent.DustType<Dusts.Air>(), (float)Math.Cos(k) * projectile.localAI[1] / slow, (float)Math.Sin(k) * projectile.localAI[1] / slow);
                }

                foreach (Player player in Main.player.Where(player => Vector2.Distance(player.Center, projectile.Center) <= projectile.localAI[1]))
                {
                    Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<Pulse>(), projectile.damage, 2);
                }

                foreach(Dust dust in Main.dust.Where(dust => dust.type == ModContent.DustType<Dusts.Air4>()))
                {
                    dust.active = false;
                }
                projectile.timeLeft = 0;
            }
        }
    }

    class Pulse : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystaline Magic");
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.timeLeft = 2;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {

        }
    }
}
