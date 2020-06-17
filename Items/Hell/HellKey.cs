﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria;

namespace StarlightRiver.Items.Hell
{
    class HellKey : QuickMaterial
    {
        public HellKey() : base("Hellscorch Key", "Opens a hellscorch chest", 10, 10000, ItemRarityID.Quest) { }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = GetTexture("StarlightRiver/Items/Hell/HellKeyGlow");
            spriteBatch.Draw(tex, item.position + new Vector2(0, -8) - Main.screenPosition, Color.White);
        }
    }
}
