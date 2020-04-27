using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    public class ThoriumRodProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 420;
            projectile.ignoreWater = true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, -1, -1, 107, 1);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thorium Rod");
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D auraTexture = mod.GetTexture("Projectiles/WeaponProjectiles/ThoriumAura");
            int frameHeight = auraTexture.Height / Main.projFrames[projectile.type];

            int frame = frameHeight * projectile.frame;

            Main.spriteBatch.Draw(auraTexture,
                projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),  //position
                new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, frame, auraTexture.Width, frameHeight)), //source
                projectile.GetAlpha(Color.White), //color
                (float)projectile.timeLeft / 10, //rotation
                new Vector2(auraTexture.Width / 2f, frameHeight / 2f), //origin
                projectile.ai[0], //scale
                SpriteEffects.None, 0f);
            base.PostDraw(spriteBatch, lightColor);
        }
        public override void AI()
        {
            Texture2D auraTexture = mod.GetTexture("Projectiles/WeaponProjectiles/ThoriumAura");
            if (projectile.timeLeft % 10 == 0)
            {
                for (int k = 0; k < Main.npc.Length; k++)
                {
                    if (Main.npc[k].active && !Main.npc[k].friendly)
                    {
                        if (Helper.CheckCircularCollision(projectile.Center, auraTexture.Height / 2, Main.npc[k].Hitbox))
                        {
                            Main.npc[k].StrikeNPC(projectile.damage, projectile.knockBack, projectile.direction);

                        }
                    }
                }
            }
            float scale = 1.2f;
            if (projectile.timeLeft > 410)
            {
                projectile.rotation = projectile.velocity.ToRotation() * (float)Math.PI / 2;
                projectile.ai[0] += scale / 10;
            }
            else
            {
                projectile.velocity *= 0.995f;
                projectile.rotation += (float)Math.PI / 40;
            }
            for (float f = 336; f >= 0; f -= 84) //keep this 84
            {
                if (projectile.timeLeft <= f)
                {
                    projectile.ai[0] -= 0.00125f;
                }
            }
            if (projectile.timeLeft <= 32)
            {
                if (projectile.ai[0] > 0)
                {
                    projectile.ai[0] -= 0.0125f;
                }
                if (projectile.scale > 0f)
                {
                    projectile.scale -= 0.05f;
                }
            }
            base.AI();
        }
    }
}