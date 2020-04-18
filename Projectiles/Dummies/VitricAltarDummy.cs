using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace StarlightRiver.Projectiles.Dummies
{
    class VitricAltarDummy : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 96;
            projectile.aiStyle = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            projectile.timeLeft = 2;
            Point16 parentPos = new Point16((int)projectile.position.X / 16, (int)projectile.position.Y / 16);
            Tile parent = Framing.GetTileSafely(parentPos.X, parentPos.Y);
            if (!parent.active()) projectile.timeLeft = 0;

            if(parent.frameX == 0 && Main.player.Any(n => Abilities.AbilityHelper.CheckDash(n, projectile.Hitbox)))
            {
                LegendWorld.GlassBossOpen = true;
                if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneGlass)
                {
                    Main.LocalPlayer.GetModPlayer<StarlightPlayer>().ScreenMoveTarget = projectile.Center;
                    Main.LocalPlayer.GetModPlayer<StarlightPlayer>().ScreenMovePan = projectile.Center + new Vector2(0, -600);
                    Main.LocalPlayer.GetModPlayer<StarlightPlayer>().ScreenMoveTime = NPCs.Boss.VitricBoss.VitricBackdropLeft.Risetime + 120;
                }
                for (int x = parentPos.X; x < parentPos.X + 5; x++) 
                {
                    for (int y = parentPos.Y; y < parentPos.Y + 7; y++) 
                    {
                        Framing.GetTileSafely(x, y).frameX += 90;
                    }
                }
            }
        }
    }
}
