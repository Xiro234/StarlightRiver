using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Dusts
{
    public class Glass : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
        }

        public override bool Update(Dust dust)
        {
            Player player = Main.LocalPlayer;
            dust.position += dust.velocity;
            dust.position += Vector2.Normalize(player.Center - dust.position) * 6;
            dust.velocity *= 0.94f;
            dust.rotation = (player.Center - dust.position).Length() * 0.1f;
            dust.scale *= 0.99f;
            if(dust.scale <= 0.4)
            {
                dust.active = false;
            }
            return false;
        }
    }
}
