﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using StarlightRiver.Core;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    class StarwoodStaffProjectile : ModProjectile, IDrawAdditive
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starshot");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }

        //These stats get scaled when empowered
        private int counterScore = 1;
        private Vector3 lightColor = new Vector3(0.2f, 0.1f, 0.05f);
        private int dustType = ModContent.DustType<Dusts.Stamina>();
        private bool empowered;


        public override void SetDefaults()
        {
            projectile.timeLeft = 60;
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.rotation = Main.rand.NextFloat(4f);
        }


        public override void AI()
        {
            if (projectile.timeLeft == 60)
            {
                StarlightPlayer mp = Main.player[projectile.owner].GetModPlayer<StarlightPlayer>();
                if (mp.Empowered)
                {
                    projectile.frame = 1;
                    lightColor = new Vector3(0.05f, 0.1f, 0.2f);
                    counterScore = 2;
                    dustType = ModContent.DustType<Dusts.BlueStamina>();
                    empowered = true;
                }
            }

            projectile.rotation += 0.3f;
            Lighting.AddLight(projectile.Center, lightColor);
            projectile.velocity = projectile.velocity.RotatedBy(Math.Sin(projectile.timeLeft * 0.2f) * projectile.ai[0]);
        }

        public override void ModifyHitNPC(NPC target,ref int damage,ref float knockback,ref bool crit,ref int hitDirection)
        {
            target.GetGlobalNPC<StarwoodScoreCounter>().AddScore(counterScore, projectile.owner, damage);
            //Main.NewText(knockback);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item4, projectile.Center);
            for (int k = 0; k < 15; k++)
            {
                Dust.NewDustPerfect(projectile.Center, dustType, ((projectile.velocity * 0.1f) * Main.rand.NextFloat(0.8f, 0.12f)).RotatedBy(Main.rand.NextFloat(-0.15f, 0.15f)), 0, default, 1.5f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.Draw(Main.projectileTexture[projectile.type],
                projectile.Center - Main.screenPosition,
                new Rectangle(0, (Main.projectileTexture[projectile.type].Height / 2) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 2),
                Color.White,
                projectile.rotation,
                new Vector2(Main.projectileTexture[projectile.type].Width / 2, Main.projectileTexture[projectile.type].Height / 4),
                1f, default, default);

            return false;
        }

        public void DrawAdditive(SpriteBatch spriteBatch)
        {
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Color color = (empowered ? new Color(200, 220, 255) * 0.35f : new Color(255, 255, 200) * 0.3f) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                if (k <= 4) color *= 1.2f;
                float scale = (projectile.scale * (float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length * 0.8f) * 0.5f;
                Texture2D tex = ModContent.GetTexture("StarlightRiver/Keys/Glow");

                spriteBatch.Draw(tex, (((projectile.oldPos[k] + projectile.Size / 2) + projectile.Center) * 0.5f) - Main.screenPosition, null, color, 0, tex.Size() / 2, scale, default, default);
            }
        }
    }

    class StarwoodStaffFallingStar : ModProjectile, IDrawAdditive
    {
        public override string Texture => "StarlightRiver/Projectiles/WeaponProjectiles/StarwoodSlingshotGlowTrail";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Falling Star");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }

        //These stats get scaled when empowered
        private float ScaleMult = 1;
        private Vector3 lightColor = new Vector3(0.2f, 0.1f, 0.05f);
        private int dustType = ModContent.DustType<Dusts.Stamina>();
        private bool empowered;


        public override void SetDefaults()
        {
            projectile.timeLeft = 600;

            projectile.width = 22;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.rotation = Main.rand.NextFloat(4f);
        }


        public override void AI()
        {
            if (projectile.timeLeft == 600)
            {
                StarlightPlayer mp = Main.player[projectile.owner].GetModPlayer<StarlightPlayer>();
                if (mp.Empowered)
                {
                    projectile.frame = 1;
                    lightColor = new Vector3(0.05f, 0.1f, 0.2f);
                    ScaleMult = 1.5f;
                    dustType = ModContent.DustType<Dusts.BlueStamina>();
                    empowered = true;
                }
            }

            Main.NewText("Dir: " + (Main.npc[(int)projectile.ai[0]].Center - projectile.Center).ToRotation());
            Main.NewText("vel: " + projectile.velocity.ToRotation());

            projectile.velocity = projectile.velocity.RotatedBy(((Main.npc[(int)projectile.ai[0]].Center - projectile.Center).ToRotation() - projectile.velocity.ToRotation()) * 0.05f);

            projectile.rotation += 0.3f;

            Lighting.AddLight(projectile.Center, lightColor);
        }

        public override void Kill(int timeLeft)
        {
            DustHelper.DrawStar(projectile.Center, dustType, pointAmount: 5, mainSize: 2f * ScaleMult, dustDensity: 1f, pointDepthMult: 0.3f);
            Main.PlaySound(SoundID.Item4, projectile.Center);
            for (int k = 0; k < 50; k++)
            {
                Dust.NewDustPerfect(projectile.Center, dustType, Vector2.One.RotatedByRandom(6.28f) * (Main.rand.NextFloat(0.25f, 1.7f) * ScaleMult), 0, default, 1.5f);
            }

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = ModContent.GetTexture(Texture);
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, empowered ? 24 : 0, 22, 24), Color.White, projectile.rotation, new Vector2(11, 12), projectile.scale, default, default);

            return false;
        }

        public void DrawAdditive(SpriteBatch spriteBatch)
        {
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Color color = (empowered ? new Color(200, 220, 255) * 0.35f : new Color(255, 255, 200) * 0.3f) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                if (k <= 4) color *= 1.2f;
                float scale = projectile.scale * (float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length * 0.8f;
                Texture2D tex = ModContent.GetTexture("StarlightRiver/Keys/Glow");

                spriteBatch.Draw(tex, (((projectile.oldPos[k] + projectile.Size / 2) + projectile.Center) * 0.50f) - Main.screenPosition, null, color, 0, tex.Size() / 2, scale, default, default);
            }
        }
    }

    internal class StarwoodScoreCounter : GlobalNPC
    {
        private int score = 0;
        private int resetCounter = 0;
        private int lasthitPlayer = 255;
        private int lasthitDamage = 0;
        public void AddScore(int scoreAmount, int playerIndex, int damage)
        {
            score += scoreAmount;
            resetCounter = 0;
            lasthitPlayer = playerIndex;
            lasthitDamage = damage;
        }
        public override bool InstancePerEntity => true;
        public override void PostAI(NPC npc)
        {
            if(score > 0)
            {
                resetCounter++;
                if(score >= 3)
                {
                    float rotationAmount = Main.rand.NextFloat(-0.3f, 0.3f);

                    StarlightPlayer mp = Main.player[lasthitPlayer].GetModPlayer<StarlightPlayer>();
                    float speed = (mp.Empowered ? 16 : 14) * Main.rand.NextFloat(0.9f, 1.1f);

                    Vector2 position = new Vector2(npc.Center.X, npc.Center.Y - 700).RotatedBy(rotationAmount, npc.Center);
                    Vector2 velocity = ((Vector2.Normalize((npc.Center + new Vector2(0, -20)) - position) * speed) + ((npc.velocity / (speed / 1.5f)) * 10f)) * (Math.Abs(rotationAmount) + 1f);
                    
                    Projectile.NewProjectile(position, velocity, ModContent.ProjectileType<StarwoodStaffFallingStar>(), lasthitDamage * 3, 1, lasthitPlayer, npc.whoAmI);
                    
                    score = 0;
                    resetCounter = 0;
                    //Main.NewText("reset spawn");
                }
                else if(resetCounter > 60)
                {
                    score = 0;
                    resetCounter = 0;
                    //Main.NewText("reset time");
                }
            }
        }
    }
}
