using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Projectiles //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class VitricFragment : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Fragment");
        }
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.timeLeft = 1200;
            projectile.scale *= 0.9f;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = false;
            projectile.thrown = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D glowmask = Main.projectileTexture[projectile.type];
            int glownum = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y7 = glownum * projectile.frame;
            Main.spriteBatch.Draw(glowmask, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, glowmask.Width, glownum)), projectile.GetAlpha(Color.White), projectile.rotation, new Vector2((float)glowmask.Width / 2f, (float)glownum / 2f), projectile.scale, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //If collide with tile, reduce the penetrate.
            //So the projectile can reflect at most 5 times
            projectile.velocity.Y += 2f;
            projectile.netUpdate = true;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            Main.PlaySound(SoundID.Item10, projectile.position);

            return false;
        }
        public float projAI0 = 0f;
        public float projAI1 = 0f;
        public override void AI()
        {
            int num;
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = 8;
                Main.PlaySound(SoundID.Item7, projectile.position);
            }

            if (projAI0 == 0f)
            {
                Player player = Main.player[projectile.owner];
                MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
                projAI1 += 1f;
                if (projAI1 >= 35f)
                {
                    if (!player.channel) //change this to right click
                    {
                        int fire;
                        for (int num731 = 0; num731 < 15; num731 = fire + 1)
                        {
                            int num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
                            Main.dust[num732].noGravity = true;
                            Dust dust = Main.dust[num732];
                            dust.velocity *= 3f;
                            num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                            dust = Main.dust[num732];
                            dust.velocity *= 1f;
                            fire = num731;
                        }
                        projAI0 = 1f;
                        projAI1 = 0f;
                        projectile.netUpdate = true;
                    }
                    else
                    {
                        projectile.velocity.X *= 0.95f;
                        projectile.velocity.Y *= 0.95f;
                    }
                }
                else
                {
                    float num166 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
                    float num167 = projectile.localAI[0];
                    if (num167 == 0f)
                    {
                        projectile.localAI[0] = num166;
                        num167 = num166;
                    }
                    float num168 = projectile.position.X;
                    float num169 = projectile.position.Y;
                    float num170 = 600f;
                    bool flag4 = false;
                    int num171 = 0;
                    if (projectile.ai[1] == 0f)
                    {
                        for (int num172 = 0; num172 < 200; num172 = num + 1)
                        {
                            if (Main.npc[num172].CanBeChasedBy(projectile, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num172 + 1)))
                            {
                                float num173 = Main.npc[num172].position.X + (float)(Main.npc[num172].width / 2);
                                float num174 = Main.npc[num172].position.Y + (float)(Main.npc[num172].height / 2);
                                float num175 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num173) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num174);
                                if (num175 < num170 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num172].position, Main.npc[num172].width, Main.npc[num172].height))
                                {
                                    num170 = num175;
                                    num168 = num173;
                                    num169 = num174;
                                    flag4 = true;
                                    num171 = num172;
                                }
                            }
                            num = num172;
                        }
                        if (flag4)
                        {
                            projectile.ai[1] = (float)(num171 + 1);
                        }
                        flag4 = false;
                    }
                    if (projectile.ai[1] > 0f)
                    {
                        int num176 = (int)(projectile.ai[1] - 1f);
                        if (Main.npc[num176].active && Main.npc[num176].CanBeChasedBy(projectile, true) && !Main.npc[num176].dontTakeDamage)
                        {
                            float num177 = Main.npc[num176].position.X + (float)(Main.npc[num176].width / 2);
                            float num178 = Main.npc[num176].position.Y + (float)(Main.npc[num176].height / 2);
                            float num179 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num177) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num178);
                            if (num179 < 1000f)
                            {
                                flag4 = true;
                                num168 = Main.npc[num176].position.X + (float)(Main.npc[num176].width / 2);
                                num169 = Main.npc[num176].position.Y + (float)(Main.npc[num176].height / 2);
                            }
                        }
                        else
                        {
                            projectile.ai[1] = 0f;
                        }
                    }
                    if (!projectile.friendly)
                    {
                        flag4 = false;
                    }
                    if (flag4)
                    {
                        float num180 = num167;
                        Vector2 vector19 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                        float num181 = num168 - vector19.X;
                        float num182 = num169 - vector19.Y;
                        float num183 = (float)Math.Sqrt((double)(num181 * num181 + num182 * num182));
                        num183 = num180 / num183;
                        num181 *= num183;
                        num182 *= num183;
                        int num184 = 12;
                        projectile.velocity.X = (projectile.velocity.X * (float)(num184 - 1) + num181) / (float)num184;
                        projectile.velocity.Y = (projectile.velocity.Y * (float)(num184 - 1) + num182) / (float)num184;
                    }
                }
            }
            else
            {
                projectile.tileCollide = false;
                float num42 = 19f;
                float num43 = 0.6f;

                Vector2 vector2 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num44 = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - vector2.X;
                float num45 = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - vector2.Y;
                float num46 = (float)Math.Sqrt((double)(num44 * num44 + num45 * num45));
                if (num46 > 3000f)
                {
                    projectile.Kill();
                }
                num46 = num42 / num46;
                num44 *= num46;
                num45 *= num46;
                if (projectile.velocity.X < num44)
                {
                    projectile.velocity.X = projectile.velocity.X + num43;
                    if (projectile.velocity.X < 0f && num44 > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num43;
                    }
                }
                else if (projectile.velocity.X > num44)
                {
                    projectile.velocity.X = projectile.velocity.X - num43;
                    if (projectile.velocity.X > 0f && num44 < 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X - num43;
                    }
                }
                if (projectile.velocity.Y < num45)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num43;
                    if (projectile.velocity.Y < 0f && num45 > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num43;
                    }
                }
                else if (projectile.velocity.Y > num45)
                {
                    projectile.velocity.Y = projectile.velocity.Y - num43;
                    if (projectile.velocity.Y > 0f && num45 < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num43;
                    }
                }

                if (Main.myPlayer == projectile.owner)
                {
                    Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    Rectangle value2 = new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height);
                    if (rectangle.Intersects(value2))
                    {
                        projectile.Kill();
                    }
                }
            }
            projectile.rotation += 0.3f * (float)projectile.direction;
            return;

            if (projAI0 == 0f)
            {
                Vector2 velocity = projectile.velocity;
                velocity.Normalize();
                projectile.rotation = (float)Math.Atan2((double)velocity.Y, (double)velocity.X) + 1.57f;
                return;
            }
            Vector2 vector4 = projectile.Center - Main.player[projectile.owner].Center;
            vector4.Normalize();
            projectile.rotation = (float)Math.Atan2((double)vector4.Y, (double)vector4.X) + 1.57f;
        }
    }
}