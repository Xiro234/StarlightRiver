﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Items;
using StarlightRiver.Projectiles.Dummies;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Overgrow
{
    class WindowSmall : DummyTile
    {
        public override int DummyType => ProjectileType<WindowSmallDummy>();

        public override void SetDefaults() => QuickBlock.QuickSetFurniture(this, 4, 6, DustType<Dusts.Stone>(), SoundID.Tink, false, new Color(255, 220, 0));

        public override bool SpawnConditions(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            return tile.frameX == 0 && tile.frameY == 0;
        }
    }

    class WindowSmallItem : QuickTileItem
    {
        public override string Texture => "StarlightRiver/MarioCumming";

        public WindowSmallItem() : base("Smol Window", "Titties", TileType<WindowSmall>(), 1) { }
    }

    class WindowSmallDummy : Dummy, IDrawAdditive
    {
        public WindowSmallDummy() : base(TileType<WindowSmall>(), 4 * 16, 6 * 16) { }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = GetTexture("StarlightRiver/Tiles/Overgrow/Window2");
            Texture2D tex2 = GetTexture("StarlightRiver/Tiles/Overgrow/WindowSmall");

            Rectangle target = new Rectangle((int)(projectile.position.X - Main.screenPosition.X), (int)(projectile.position.Y - Main.screenPosition.Y), 4 * 16, 6 * 16);

            float offX = (Main.screenPosition.X + Main.screenWidth / 2 - projectile.Center.X) * -0.14f;
            float offY = (Main.screenPosition.Y + Main.screenHeight / 2 - projectile.Center.Y) * -0.14f;
            Rectangle source = new Rectangle(tex.Width / 2 + (int)offX, 16 + (int)offY, 4 * 16, 6 * 16);

            spriteBatch.Draw(tex, target, source, Color.White);
            spriteBatch.Draw(tex2, target, tex2.Frame(), lightColor);
        }

        public override void Update()
        {
            projectile.ai[0] += 0.02f;
            Lighting.AddLight(projectile.Center + new Vector2(0, 32), new Vector3(1, 1f, 0.6f));

            if (Main.rand.Next(20) == 0)
            {
                Vector2 off = Vector2.UnitY.RotatedByRandom(0.8f);
                Dust.NewDustPerfect(projectile.Center + off * 20, DustType<Dusts.Gold4>(), off * 0.15f, 0, default, 0.35f);
            }
        }

        public void DrawAdditive(SpriteBatch spriteBatch)
        {
            Texture2D tex = GetTexture("StarlightRiver/Tiles/Overgrow/PitGlow");

            float off = (float)Math.Sin(projectile.ai[0]) * 0.05f;

            for (int k = -1; k < 2; k++)
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0, 100), tex.Frame(), new Color(1, 0.9f, 0.6f) * (0.4f + off), (float)Math.PI + k * (0.9f + off), new Vector2(tex.Width / 2, 0), 0.7f, 0, 0);
        }
    }
}
