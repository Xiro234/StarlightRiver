using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.Tiles.Overgrow
{
    class BossWindow : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!Main.projectile.Any(proj => proj.active && proj.type == ModContent.ProjectileType<Projectiles.Dummies.OvergrowBossWindowDummy>() &&
             proj.Hitbox.Intersects(new Rectangle(i * 16, j * 16, 16, 16))))
            {
                Projectile.NewProjectile(new Vector2(i, j) * 16, Vector2.Zero, ModContent.ProjectileType<Projectiles.Dummies.OvergrowBossWindowDummy>(), 0, 0);
            }
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2(i + 0.5f, j + 0.5f) * 16;
            Vector2 drawpos = (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition;

            Texture2D frametex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/WindowFrame");
            spriteBatch.Draw(frametex, drawpos, frametex.Frame(), Color.White, 0, frametex.Frame().Size() / 2, 1, 0, 0);
            return false;
        }
    }
}
