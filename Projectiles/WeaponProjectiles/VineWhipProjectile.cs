using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    class WhipSegment1 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            //projectile.ai[1] maxLength
            //projectile.localAI[0] length
            //projectile.localAI[1] shrink
            Player player = Main.player[projectile.owner];
            if (projectile.ai[0] == 0)
            {
                projectile.localAI[0] = 1;
                if ((Main.MouseWorld - player.Center).Length() > 400) projectile.ai[1] = 400;
                else projectile.ai[1] = (Main.MouseWorld - player.Center).Length();
                projectile.ai[0] = 1;
            }
            projectile.timeLeft = 2;
            projectile.position = player.Center;
            if (projectile.localAI[0] < projectile.ai[1] && projectile.localAI[1] == 0) projectile.localAI[0] += 30f;
            if (!player.channel) 
            {
                projectile.localAI[1] = 1;
            }
            if (projectile.localAI[1] == 1)
            {
                projectile.localAI[0] -= 15f;
            }
            if (projectile.localAI[0] < 1) projectile.Kill();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D unit = Main.projectileTexture[projectile.type];
            int unitLength = 13;
            int numUnits = (int)Math.Ceiling(projectile.localAI[0] / unitLength);
            float increment = 0f;
            if (numUnits > 1)
            {
                increment = (projectile.localAI[0] - unitLength) / (numUnits - 1);
            }
            Vector2 direction = new Vector2((float)Math.Cos(projectile.rotation), (float)Math.Sin(projectile.rotation));
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipVertically;
            }
            for (int k = 1; k <= numUnits; k++)
            {
                Texture2D image = unit;
                if (k == numUnits)
                {
                    image = mod.GetTexture("Projectiles/WeaponProjectiles/WhipTip");
                }
                Vector2 pos = projectile.position + direction * (increment * (k - 1) + unitLength / 2f);
                Color color = Lighting.GetColor((int)(pos.X / 16f), (int)(pos.Y / 16f));
                spriteBatch.Draw(image, pos - Main.screenPosition, null, projectile.GetAlpha(color), projectile.rotation+MathHelper.ToRadians(90), new Vector2(unit.Width / 2, unit.Height / 2), 1f, effects, 0f);
            }
            return false;

        }
    }
}
