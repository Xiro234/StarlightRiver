using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Items.Vitric;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Vitric
{
    internal class VitricSand : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            drop = ModContent.ItemType<VitricSandItem>();
            AddMapEntry(new Color(172, 131, 105));

            Main.tileMerge[Type][ModContent.TileType<VitricSpike>()] = true;
            Main.tileMerge[Type][ModContent.TileType<AncientSandstone>()] = true;
            Main.tileMerge[Type][ModContent.TileType<VitricSoftSand>()] = true;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color light = Lighting.GetColor(i, j);
            Color airColor = new Color(190, 255, 245);
            if (Main.rand.Next(200) == 0 && light.R >= 10 && light.B >= 10 && light.G >= 10)
            {
                Dust.NewDustPerfect(new Vector2(i * 16, j * 16), mod.DustType("Air3"), Vector2.Zero, 0, airColor * ((light.R + light.G + light.B) / 765f));
            }
        }
    }

    internal class VitricTile : ModTile
    {
        private new readonly int Drop = 0;
        private readonly int Chance = 0;
        private readonly int Pick = 0;
        public VitricTile(int drop, int chance, int pick)
        {
            Drop = drop;
            Chance = chance;
            Pick = pick;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            drop = Drop;
            minPick = Pick;
            AddMapEntry(new Color(115, 182, 158));
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color light = Lighting.GetColor(i, j);
            Color airColor = new Color(190, 255, 245);
            if (Main.rand.Next(Chance) == 0 && light.R >= 10 && light.B >= 10 && light.G >= 10)
            {
                Dust.NewDustPerfect(new Vector2(i * 16, j * 16), mod.DustType("Air3"), Vector2.Zero, 0, airColor * ((light.R + light.G + light.B) / 765f));
            }
        }
    }

    internal class VitricGlass : VitricTile
    {
        public VitricGlass() : base(ModContent.ItemType<VitricGlassItem>(), 30, 50)
        {

        }
    }

    internal class VitricGlassCrystal : VitricTile
    {
        public VitricGlassCrystal() : base(ModContent.ItemType<VitricGlassCrystalItem>(), 30, 50)
        {

        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            drop = ModContent.ItemType<VitricGlassCrystalItem>();
            minPick = 50;
            TileID.Sets.NotReallySolid[Type] = true;
            AddMapEntry(new Color(100, 170, 170));
        }
    }

    internal class VitricBrick : VitricTile
    {
        public VitricBrick() : base(ModContent.ItemType<VitricBrickItem>(), 60, 65)
        {

        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            drop = ModContent.ItemType<VitricBrickItem>();
            minPick = 65;
            TileID.Sets.NotReallySolid[Type] = true;
            AddMapEntry(new Color(169, 229, 167));
        }
    }

    internal class VitricSoftSand : VitricTile
    {
        public VitricSoftSand() : base(ItemID.SandBlock, 30, 50)
        {

        }

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileSand[Type] = true;
            drop = ModContent.ItemType<VitricSandItem>(); //TBC
            AddMapEntry(new Color(172, 131, 105));

            Main.tileMerge[Type][ModContent.TileType<VitricSpike>()] = true;
            Main.tileMerge[Type][ModContent.TileType<AncientSandstone>()] = true;
            Main.tileMerge[Type][ModContent.TileType<VitricSand>()] = true;
        }
    }

    internal class VitricMoss : VitricTile
    {
        public VitricMoss() : base(ItemID.Eggnog, 30, 50)
        {
            //Should never be killed in-game lol so eggnog
        }

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            drop = ModContent.ItemType<VitricSandItem>(); //TBC
            SetModCactus(new VitricCactus());
            AddMapEntry(new Color(172, 131, 105));

            Main.tileMerge[Type][ModContent.TileType<VitricSpike>()] = true;
            Main.tileMerge[Type][ModContent.TileType<AncientSandstone>()] = true;
            Main.tileMerge[Type][ModContent.TileType<VitricSand>()] = true;
            Main.tileMerge[Type][ModContent.TileType<VitricSoftSand>()] = true;
        }

        public override void RandomUpdate(int i, int j)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int x1 = -1; x1 <= 1; x1++)
                {
                    int tileX = i + x1;
                    int tileY = j + y;
                    if (!WorldGen.InWorld(i, j, 0)) continue;
                    if (Main.tile[tileX, tileY].type == (ushort)ModContent.TileType<VitricSand>() && Main.rand.Next(3) == 0)
                    {
                        Main.tile[tileX, tileY].type = (ushort)ModContent.TileType<VitricMoss>();
                        WorldGen.SquareTileFrame(tileX, tileY, true);
                    }
                }
            }

            if (!Main.tile[i, j + 1].active() && Main.rand.Next(10) == 0)
                WorldGen.PlaceTile(i, j + 1, Type);
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                fail = true;
                Main.tile[i, j].type = (ushort)ModContent.TileType<VitricSand>();
                WorldGen.SquareTileFrame(i, j, true);
                Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, mod.DustType("Air3"), 0f, 0f, 0, new Color(121, 121, 121), 1f);
            }
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Texture2D moss = ModContent.GetTexture("StarlightRiver/Tiles/Vitric/VitricMoss_Glow");
            Tile t = Main.tile[i, j];
            Color col = Lighting.GetColor(i, j);
            Color realCol = new Color(((col.R / 255f) * 1.4f) + 0.1f, ((col.G / 255f) * 1.4f) + 0.1f, ((col.B / 255f) * 1.4f) + 0.1f);
            spriteBatch.Draw(moss, ((new Vector2(i, j) + Helper.TileAdj) * 16) - Main.screenPosition, new Rectangle(t.frameX, t.frameY, 16, 16), realCol, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
        }
    }
}