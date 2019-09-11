using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spritersguildwip.Ability;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.NPCs.Boss
{
    class VitricBoss : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PH Vitric Boss");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 3500;
            npc.damage = 30;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 100;
            npc.height = 100;
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
        Vector2 direction = Vector2.Zero;
        Vector2[] spawns = new Vector2[6];
        public override void AI()
        {
            Main.NewText(npc.localAI[0] +"/"+ npc.localAI[1] + "/" + npc.localAI[2] + "/" + npc.localAI[3]);
            switch (npc.localAI[0])
            {
                case 0:
                    npc.immortal = true;
                    npc.localAI[1]++;
                    if(npc.localAI[1] <= 20 + 10 * Main.ActivePlayersCount && npc.localAI[1] % 10 == 0)
                    {
                        float r = (float)(Math.PI * 2) / (2 + Main.ActivePlayersCount) * (npc.localAI[1] / 10);
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Ward"),0,(float)Math.Cos(r) * 20,(float)Math.Sin(r) * 20);
                    }
                    if (npc.localAI[1] >= 960)
                    {
                        npc.localAI[1] = 0;
                        npc.localAI[0] = 1;
                    }
                    break;

                case 1:
                    for (int k = 0; k <= Main.npc.Length - 1; k++)
                    {
                        if (Main.npc[k].type == mod.NPCType("Ward") && Main.npc[k].active == true)
                        {
                            npc.localAI[2]++;
                            Helper.Kill(Main.npc[k]);
                        }
                    }
                    if (npc.localAI[2] > 0)
                    {
                        for (int k = 0; k <= Main.player.Length - 1; k++)
                        {
                            if (Vector2.Distance(Main.player[k].Center, npc.Center) <= 1000)
                            {
                                Main.player[k].immune = false;
                                Main.player[k].immuneTime = 0;
                                Projectile.NewProjectile(Main.player[k].Center, Vector2.Zero, mod.ProjectileType("Pulse"),
                                (int)(npc.localAI[2] / (Main.ActivePlayersCount + 2) * 200), 1);
                            }
                        }
                    }
                    npc.localAI[2] = 0;
                    npc.localAI[3] = 0;
                    npc.localAI[0] = 2;
                    break;

                case 2:

                    npc.TargetClosest(true);
                    npc.immortal = false;

                    npc.localAI[3]++;
                    if (npc.localAI[3] == 180)
                    {
                        npc.localAI[2] = Main.rand.Next(2);
                    }
                    if (npc.localAI[3] > 180)
                    {
                        switch (npc.localAI[2])
                        {
                            case 0:
                                if (npc.localAI[3] == 181)
                                {
                                    npc.netUpdate = true;
                                    direction = Vector2.Normalize(Main.player[npc.target].Center - npc.Center);
                                }
                                if(npc.localAI[3] >= 181 && npc.localAI[3] <= 240)
                                {
                                    Dust.NewDust(npc.Center + (direction.RotatedBy(1.57) * 0.25f * (npc.localAI[3] - 180)), 1, 1, mod.DustType("Air"),direction.X * 15, direction.Y * 15, 0, default, (60-(npc.localAI[3] - 180)) / 30 );
                                    Dust.NewDust(npc.Center - (direction.RotatedBy(1.57) * 0.25f * (npc.localAI[3] - 180)), 1, 1, mod.DustType("Air"), direction.X * 15, direction.Y * 15, 0, default, (60-(npc.localAI[3] - 180)) / 30);
                                }
                                if(npc.localAI[3] >= 261 && npc.localAI[3] <= 320)
                                {
                                    npc.velocity += direction * 0.5f;
                                }
                                if (npc.localAI[3] >= 321 && npc.localAI[3] <= 330)
                                {
                                    npc.velocity = Vector2.Zero;
                                }
                                if (npc.localAI[3] >= 331 && npc.localAI[3] <= 350)
                                {
                                    npc.velocity += direction * -.38f;
                                }
                                if (npc.localAI[3] >= 351 && npc.localAI[3] <= 450)
                                {
                                    npc.velocity = direction * -8.34f;
                                }
                                if(npc.localAI[3] == 451)
                                {
                                    npc.velocity = Vector2.Zero;
                                    direction = Vector2.Zero;
                                    npc.localAI[3] = 0;
                                    npc.localAI[2] = 0;
                                }
                                break;

                            case 1:
                                if (npc.localAI[3] == 181)
                                {
                                    npc.netUpdate = true;
                                    spawns[0] = new Vector2(Main.player[npc.target].Center.X, npc.Center.Y - 500);
                                    for (int k = 1; k <= 5; k++)
                                    {
                                        spawns[k] = new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 500);
                                    }                                    
                                }
                                if (npc.localAI[3] >= 181 && npc.localAI[3] <= 240)
                                {
                                    foreach(Vector2 spawn in spawns)
                                    {
                                        Dust.NewDustPerfect(spawn + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-32, 32)), mod.DustType("Air"), new Vector2(0 , 8), 0, default, (60 - (npc.localAI[3] - 180)) / 15);
                                    }                                  
                                }
                                if (npc.localAI[3] == 261)
                                {
                                    foreach (Vector2 spawn in spawns)
                                    {
                                        Projectile.NewProjectile(spawn, new Vector2(0, Main.rand.Next(6,12)), 2/*mod.ProjectileType("Crystal1")*/, 20, 0f);
                                    }
                                    spawns = new Vector2[6];
                                    npc.localAI[3] = 0;
                                    npc.localAI[2] = 0;
                                }
                                break;
                            case 2:
                                npc.localAI[3] = 0;
                                npc.localAI[2] = 0;
                                break;
                        }
                    }
                    

                    

                    if(npc.life <= npc.lifeMax / 2)
                    {
                        npc.localAI[1] = 0;
                        npc.localAI[2] = 0;
                        npc.localAI[3] = 0;
                        npc.localAI[0] = 3;
                    }
                    break;

                case 3:
                    npc.immortal = true;
                    npc.velocity *= 0;
                    npc.localAI[1]++;
                    if (npc.localAI[1] <= 20 + 10 * Main.ActivePlayersCount && npc.localAI[1] % 10 == 0)
                    {
                        float r = (float)(Math.PI * 2) / (2 + Main.ActivePlayersCount) * (npc.localAI[1] / 10);
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Ward"), 0, (float)Math.Cos(r) * 20, (float)Math.Sin(r) * 20);
                    }
                    if (npc.localAI[1] >= 600)
                    {
                        npc.localAI[1] = 0;
                        npc.localAI[0] = 4;
                    }
                    break;

                case 4:
                    for (int k = 0; k <= Main.npc.Length - 1; k++)
                    {
                        if (Main.npc[k].type == mod.NPCType("Ward") && Main.npc[k].active == true)
                        {
                            npc.localAI[2]++;
                            Helper.Kill(Main.npc[k]);
                        }
                    }
                    if (npc.localAI[2] > 0)
                    {
                        for(int k = 0; k <= Main.player.Length - 1; k++)
                        {
                            if (Vector2.Distance(Main.player[k].Center, npc.Center) <= 1000)
                            {
                                Main.player[k].immune = false;
                                Main.player[k].immuneTime = 0;
                                Projectile.NewProjectile(Main.player[k].Center, Vector2.Zero, mod.ProjectileType("Pulse"),
                                (int)(npc.localAI[2] / (Main.ActivePlayersCount + 2) * 200), 1);
                            }
                        }

                    }
                    npc.localAI[2] = 0;
                    npc.localAI[0] = 5;
                    break;

                case 5:
                    //normal AI 2
                    npc.immortal = false;

                    npc.TargetClosest(true);
                    npc.velocity = Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * 5;

                    if (npc.life <= npc.lifeMax / 10)
                    {
                        npc.localAI[0] = 6;
                    }
                    break;

                case 6:
                    npc.immortal = true;
                    npc.velocity *= 0;
                    npc.localAI[1]++;
                    if (npc.localAI[1] == 180)
                    {
                        float p = Main.rand.NextFloat(0, (float)Math.PI * 2);
                        Vector2 start = npc.Center + new Vector2((float)Math.Cos(p), (float)Math.Sin(p)) * 800;
                        Projectile.NewProjectile(start, Vector2.Normalize(npc.Center - start) * 0.6f, mod.ProjectileType("HealGem"), 10, 0.2f);
                        npc.localAI[1] = 0;
                    }
                    for (int k = 0; k <= Main.projectile.Length - 1; k++)
                    {
                        if (Main.projectile[k].type == mod.ProjectileType("HealGem") && Main.projectile[k].Hitbox.Intersects(npc.Hitbox) && Main.projectile[k].active)
                        {
                            if (Main.projectile[k].localAI[0] == 1)
                            {
                                npc.life -= 30;
                            }
                            else
                            {
                                npc.life += 10;
                            }
                            Main.projectile[k].active = false;
                        }
                    }
                    if(npc.life <= 0)
                    {
                        Helper.Kill(npc);
                    }

                    if(npc.life >= npc.lifeMax / 5)
                    {
                        npc.localAI[0] = 5;
                        npc.immortal = false;
                    }
                    break;

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
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 10f;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.immortal = true;
            npc.noGravity = true;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            if (npc.Hitbox.Intersects(player.Hitbox) && player.GetModPlayer<AbilityHandler>().ability is Dash && player.GetModPlayer<AbilityHandler>().ability.Active)
            {
                player.GetModPlayer<AbilityHandler>().ability.Active = false;
                player.GetModPlayer<AbilityHandler>().ability.OnExit();
                Helper.Kill(npc);
            }

            npc.velocity = new Vector2(npc.ai[0], npc.ai[1]);
            npc.ai[0] *= 0.95f;
            npc.ai[1] *= 0.95f;
        }

        public static Texture2D glow = ModContent.GetTexture("spritersguildwip/NPCs/Boss/CrystalGlow");

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(glow, npc.position - Main.screenPosition + new Vector2(0, 3), new Color(255, 255, 255) * (float)Math.Sin(LegendWorld.rottime));
        }
    }

    class HealGem : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
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
            Player player = Main.LocalPlayer;

            if (projectile.Hitbox.Intersects(player.Hitbox) && player.GetModPlayer<AbilityHandler>().ability is Dash && player.GetModPlayer<AbilityHandler>().ability.Active)
            {
                projectile.localAI[0] = 1;
                projectile.hostile = false;
            }

            if (!Main.npc.Any(npc => npc.type == mod.NPCType("VitricBoss") && npc.active))
            {
                projectile.timeLeft = 0;
            }
        }

        public static Texture2D glow = ModContent.GetTexture("spritersguildwip/NPCs/Boss/CrystalGlow2");
        public static Texture2D red = ModContent.GetTexture("spritersguildwip/NPCs/Boss/HealGem2");

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (projectile.localAI[0] == 0)
            {
                spriteBatch.Draw(glow, projectile.position - Main.screenPosition + new Vector2(16, 16), null, new Color(255, 255, 255) * (float)Math.Sin(LegendWorld.rottime), projectile.rotation, glow.Size() / 2, projectile.scale, 0, 0);
            }
            else
            {
                spriteBatch.Draw(red, projectile.position - Main.screenPosition + new Vector2(16,16), null, Lighting.GetColor((int)projectile.position.X / 16, (int)projectile.position.Y / 16), projectile.rotation, glow.Size() / 2, projectile.scale, 0, 0);
            }
        }
    }

    class Pulse : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Their own Recklessness");
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
