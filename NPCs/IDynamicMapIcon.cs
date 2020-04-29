using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;

namespace StarlightRiver.NPCs
{
    interface IDynamicMapIcon
    {
        void DrawOnMap(SpriteBatch spriteBatch, Vector2 center, float scale);
    }
}
