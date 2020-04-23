using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
{
    class Breakuator : GlobalTile
    {
        public static List<Point16> breakuator = new List<Point16>();
        public override void HitWire(int i, int j, int type)
        {
            if (breakuator.Any(point => point == new Point16(i, j)))
            {
                WorldGen.KillTile(i, j);
            }
        }

        public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            if (breakuator.Any(point => point == new Point16(i, j)))
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/Breakuator"), new Vector2(i + 12, j + 12) * 16 - Main.screenPosition, Color.White);
            }
        }
    }
}
