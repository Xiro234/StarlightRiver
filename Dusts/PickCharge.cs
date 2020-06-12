﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Dusts
{
    public class PickCharge : ModDust
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Dusts/FireDust";
            return true;
        }
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            if (dust.customData is int && Main.player[(int)dust.customData].active)
            {
                Player player = Main.player[(int)dust.customData];
                dust.position = player.Center + new Vector2(0, player.gfxOffY) + dust.velocity;
                if (!Main.mouseRight || !(player.HeldItem.modItem is Items.Temple.TemplePick)) dust.active = false; //RIP multiplayer
            }
            else dust.active = false;

            dust.rotation += 0.1f;
            return false;
        }
    }
}