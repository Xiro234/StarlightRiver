using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using StarlightRiver.Projectiles.Dummies;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Vitric
{
    internal class VitricOre : DummyTile
    {
        public override int DummyType => ModContent.ProjectileType<VitricOreDummy>();
        public override void SetDefaults()
        {
            QuickBlock.QuickSetFurniture(this, 2, 3, ModContent.DustType<Dusts.Air>(), SoundID.Shatter, false, ModContent.ItemType<Items.Vitric.VitricOre>(), new Color(200, 255, 230));
            minPick = int.MaxValue;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {                      
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                Texture2D glow = ModContent.GetTexture("StarlightRiver/Tiles/Vitric/VitricOreGlow");
                Color color = Color.White * (float)Math.Sin(StarlightWorld.rottime);

                spriteBatch.Draw(glow, (new Vector2(i ,j) + Helper.TileAdj) * 16 - Main.screenPosition, color);
            }
        }
    }

    internal class VitricOreFloat : DummyTile
    {
        public override int DummyType => ModContent.ProjectileType<VitricOreFloatDummy>();
        public override void SetDefaults()
        {
            QuickBlock.QuickSetFurniture(this, 2, 2, ModContent.DustType<Dusts.Air>(), SoundID.Shatter, false, ModContent.ItemType<Items.Vitric.VitricOre>(), new Color(200, 255, 230));
            minPick = int.MaxValue;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                Texture2D glow = ModContent.GetTexture("StarlightRiver/Tiles/Vitric/VitricOreFloatGlow");
                Color color = Color.White * (float)Math.Sin(StarlightWorld.rottime);

                spriteBatch.Draw(glow, (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition, color);
            }
        }
    }

    internal class VitricOreDummy : Dummy
    {
        public VitricOreDummy() : base(ModContent.TileType<VitricOre>(), 32, 48) { }

        public override void Collision(Player player)
        {
            if (AbilityHelper.CheckDash(player, projectile.Hitbox))
            {
                WorldGen.KillTile((int)projectile.position.X / 16, (int)projectile.position.Y / 16);

                for (int k = 0; k <= 20; k++)
                {
                    Dust.NewDust(projectile.position, 32, 32, ModContent.DustType<Dusts.Glass2>(), 0, 0, 0, default, 1.3f);
                    Dust.NewDust(projectile.position, 32, 32, ModContent.DustType<Dusts.Air>(), 0, 0, 0, default, 0.8f);
                }
            }
        }
    }

    internal class VitricOreFloatDummy : Dummy
    {
        public VitricOreFloatDummy() : base(ModContent.TileType<VitricOreFloat>(), 32, 32) { }

        public override void Collision(Player player)
        {
            if (AbilityHelper.CheckDash(player, projectile.Hitbox))
            {
                WorldGen.KillTile((int)projectile.position.X / 16, (int)projectile.position.Y / 16);

                for (int k = 0; k <= 20; k++)
                {
                    Dust.NewDust(projectile.position, 32, 32, ModContent.DustType<Dusts.Glass2>(), 0, 0, 0, default, 1.3f);
                    Dust.NewDust(projectile.position, 32, 32, ModContent.DustType<Dusts.Air>(), 0, 0, 0, default, 0.8f);
                }
            }
        }
    }
}