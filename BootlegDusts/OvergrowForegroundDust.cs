using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarlightRiver.GUI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.BootlegDusts
{
    class OvergrowForegroundDust : BootlegDust
    {
        int offset = 0;
        float co = 0;
        public int fadein = 0;
        public OvergrowForegroundDust(int off, float coefficient, Vector2 velocity, Color color, float scale) : base(ModContent.GetTexture("StarlightRiver/GUI/HolyBig"), new Vector2(0, Main.screenHeight), velocity, color, scale, Main.rand.Next(6000))
        {
            offset = off;
            co = coefficient;
        }

        public override void Update()
        {
            pos.X = Main.dungeonX * 16 + offset + (Main.LocalPlayer.Center.X - Main.dungeonX * 16) * -co - Main.screenPosition.X;
            pos.Y = Main.dungeonY * 16 + 12000 + (Main.LocalPlayer.Center.Y -  Main.dungeonY * 16) * -co - Main.screenPosition.Y + (6000 - time) * vel.Y;
            time--;
            if(fadein < 60 || fadein > 100) fadein++;

            if (time <= 60) col *= 0.999f;
            int factor = co > 1 ? 5 : 3;
            if (fadein < 60 && fadein % factor == 0) { col.R++; col.G++; col.B++; }
            if(fadein > 100) col *= 0.999f;
        }
    }
}
