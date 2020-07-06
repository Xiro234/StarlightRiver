using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.NPCs.Miniboss.Glassweaver
{
    class SwordSlash : ModProjectile
    {
        public NPC parent;

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.hostile = true;
            projectile.aiStyle = -1;
            projectile.timeLeft = 30;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            float duration = 30;
            float progress = projectile.timeLeft > (duration / 2f) ? (duration - projectile.timeLeft) / (duration / 2f) : projectile.timeLeft / (duration / 2f);
            projectile.Center = parent.Center + Vector2.SmoothStep(Vector2.Zero, Vector2.Normalize(projectile.velocity) * 128, progress);

            for (int k = 0; k < Main.maxPlayers; k++)
            {
                Player player = Main.player[k];
                if (player.active && player.itemAnimation > 0 && player.HeldItem.GetGlobalItem<StarlightItem>().meleeHitbox.Intersects(projectile.Hitbox))
                {
                    parent.ai[2] = (int)GlassMiniboss.AttackState.Recoil;
                    parent.ai[3] = 2;
                    parent.velocity = Vector2.Normalize(parent.Center - player.Center) * 5;

                    for (int m = 0; m < 100; m++) Dust.NewDustPerfect(projectile.Center, DustType<Dusts.Glass3>());
                    Main.PlaySound(SoundID.Shatter);
                    projectile.timeLeft = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) => false;

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = GetTexture(Texture);
            Rectangle frame = new Rectangle(0, (int)(projectile.timeLeft / 30f * 8) * tex.Height / 8, tex.Width, tex.Height / 8);
            spriteBatch.Draw(tex, parent.Center - Main.screenPosition, frame, Color.White, projectile.velocity.ToRotation(), new Vector2(0, 32), 1, 0, 0);
        }
    }
}
