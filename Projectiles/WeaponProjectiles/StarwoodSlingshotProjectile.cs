using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    internal class StarwoodSlingshotProjectile : ModProjectile, IDrawAdditive
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shooting Star");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        //These stats get scaled when empowered
        private int ScaleMult = 1;

        private Color glowColor = new Color(255, 220, 200, 150);
        private Vector3 lightColor = new Vector3(0.2f, 0.1f, 0.05f);
        private int dustType = DustType<Dusts.Stamina>();

        public override void SetDefaults()
        {
            projectile.timeLeft = 600;

            projectile.width = 22;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.aiStyle = -1;
            projectile.rotation = Main.rand.NextFloat(4f);
        }

        public override void AI()
        {
            Player projOwner = Main.player[projectile.owner];
            StarlightPlayer mp = Main.player[projectile.owner].GetModPlayer<StarlightPlayer>();

            projectile.rotation += 0.2f;

            if (projectile.timeLeft == 600 && mp.Empowered)
            {
                projectile.frame = 1;
                glowColor = new Color(220, 200, 255, 150);
                lightColor = new Vector3(0.05f, 0.1f, 0.2f);
                ScaleMult = 2;
                dustType = DustType<Dusts.BlueStamina>();
                projectile.velocity *= 1.2f;
            }
            Lighting.AddLight(projectile.Center, lightColor);
            if (projectile.velocity.Y < 50)
            {
                projectile.velocity.Y += 0.25f;
            }
            projectile.velocity.X *= 0.995f;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage *= ScaleMult;
        }

        public override void Kill(int timeLeft)
        {
            DustHelper.DrawStar(projectile.Center, dustType, pointAmount: 5, mainSize: 1f * ScaleMult, dustDensity: 0.5f, pointDepthMult: 0.3f);
            Main.PlaySound(SoundID.Item4, projectile.Center);
            for (int k = 0; k < 35; k++)
            {
                Dust.NewDustPerfect(projectile.Center, dustType, Vector2.One.RotatedByRandom(6.28f) * (Main.rand.NextFloat(0.25f, 1.2f) * ScaleMult), 0, default, 1.5f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            StarlightPlayer mp = Main.player[projectile.owner].GetModPlayer<StarlightPlayer>();

            Texture2D tex = GetTexture(Texture);
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, mp.Empowered ? 22 : 0, 22, 24), Color.White, projectile.rotation, new Vector2(11, 12), projectile.scale, default, default);
            return false;
        }

        public void DrawAdditive(SpriteBatch spriteBatch)
        {
            StarlightPlayer mp = Main.player[projectile.owner].GetModPlayer<StarlightPlayer>();

            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Color color = (mp.Empowered ? new Color(200, 220, 255) * 0.35f : new Color(255, 255, 200) * 0.3f) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                if (k <= 4) color *= 1.2f;
                float scale = projectile.scale * (float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length * 0.8f;
                Texture2D tex = GetTexture("StarlightRiver/Keys/Glow");

                spriteBatch.Draw(tex, projectile.oldPos[k] + projectile.Size / 2 - Main.screenPosition, null, color, 0, tex.Size() / 2, scale, default, default);
            }
        }
    }
}