using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Reflection;
using Terraria.ModLoader.IO;

namespace StarlightRiver.Items.Debug
{
    class StructureSaver : ModItem
    {
        public bool SecondPoint { get; set; }
        public Point16 TopLeft { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTime = 20;
            item.useAnimation = 20;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2 && !SecondPoint && TopLeft != null)
            {
                SaveStructure(new Rectangle(TopLeft.X, TopLeft.Y, Width, Height));
            }

            else if (!SecondPoint)
            {
                TopLeft = (Main.MouseWorld / 16).ToPoint16();
                Width = 0;
                Height = 0;
                Main.NewText("Select Second Point");
                SecondPoint = true;
            }

            else
            {
                Point16 bottomRight = (Main.MouseWorld / 16).ToPoint16();
                Width = bottomRight.X - TopLeft.X;
                Height = bottomRight.Y - TopLeft.Y;
                Main.NewText("Ready to save! right click to save this structure...");
                SecondPoint = false;
            }

            return true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/GUI/Stamina2");
            Texture2D tex2 = ModContent.GetTexture("StarlightRiver/GUI/Fire");
            if (Width != 0 && TopLeft != null)
            {
                spriteBatch.Draw(tex2, new Rectangle((int)(TopLeft.X * 16 - Main.screenPosition.X), (int)(TopLeft.Y * 16 - Main.screenPosition.Y), Width * 16 + 16, Height * 16 + 16), tex2.Frame(), Color.White * 0.25f);
                spriteBatch.Draw(tex, (TopLeft.ToVector2() + new Vector2(Width + 1, Height + 1)) * 16 - Main.screenPosition, tex.Frame(), Color.Red, 0, tex.Frame().Size() / 2, 1, 0, 0);
            }
            if (TopLeft != null) spriteBatch.Draw(tex, TopLeft.ToVector2() * 16 - Main.screenPosition, tex.Frame(), Color.Cyan, 0, tex.Frame().Size() / 2, 1, 0, 0);
        }
        private void SaveStructure(Rectangle target)
        {
            string path = ModLoader.ModPath.Replace("Mods", "SavedStructures");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string thisPath = path + "/" + "SavedStructure_" + DateTime.Now.ToString("d-M-y----H-m-s-f");
            Main.NewText("Structure saved as " + thisPath);
            FileStream stream = File.Create(thisPath);
            stream.Close();

            TagCompound tag = new TagCompound();
            tag.Add("Width", Width);
            tag.Add("Height", Height);

            List<TileSaveData> data = new List<TileSaveData>();
            for (int x = target.X; x <= target.X + target.Width; x++)
            {
                for (int y = target.Y; y <= target.Y + target.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    string tileName;
                    string wallName;
                    if (tile.type > Main.maxTileSets) tileName = ModContent.GetModTile(tile.type).GetType().AssemblyQualifiedName;
                    else tileName = tile.type.ToString();
                    if (tile.wall > Main.maxWallTypes) wallName = ModContent.GetModWall(tile.wall).GetType().AssemblyQualifiedName;
                    else wallName = tile.wall.ToString();

                    byte[] wireArray = new byte[]
                    {
                        (byte)tile.wire().ToInt(),
                        (byte)tile.wire2().ToInt(),
                        (byte)tile.wire3().ToInt(),
                        (byte)tile.wire4().ToInt()
                    };
                    data.Add(new TileSaveData(tile.active(), tileName, wallName, tile.frameX, tile.frameY, (short)tile.wallFrameX(), (short)tile.wallFrameY(), tile.slope(), tile.liquid, tile.color(), tile.wallColor(), wireArray));
                }
            }
            tag.Add("TileData", data);

            TagIO.ToFile(tag, thisPath);
        }
        public static void GenerateStructure(string path, Point16 pos, Mod mod)
        {
            TagCompound tag = TagIO.FromStream(mod.GetFileStream(path));
            List<TileSaveData> data = (List<TileSaveData>)tag.GetList<TileSaveData>("TileData");
            for(int x = 0; x <= tag.GetInt("Width"); x++)
            {
                for (int y = 0; y <= tag.GetInt("Height"); y++)
                {
                    int index = y + x * (tag.GetInt("Height") + 1);
                    TileSaveData d = data[index];
                    Tile tile = Framing.GetTileSafely(pos.X + x, pos.Y + y);

                    tile.ClearEverything();            
                    if (!int.TryParse(d.Tile, out int type))
                    {
                        //try
                        //{
                            Type tileType = Type.GetType(d.Tile);
                            var getType = typeof(ModContent).GetMethod("TileType", BindingFlags.Static | BindingFlags.Public);
                            type = (int)getType.MakeGenericMethod(tileType).Invoke(null, null);
                        //}
                        //catch { type = 0; }
                    }
                    if (!int.TryParse(d.Wall, out int wallType))
                    {
                        try
                        {
                            Type wallTypeType = Type.GetType(d.Wall); //I am so sorry for this name
                            var getWallType = typeof(ModContent).GetMethod("WallType", BindingFlags.Static | BindingFlags.Public);
                            type = (int)getWallType.MakeGenericMethod(wallTypeType).Invoke(null, null);
                        }
                        catch { type = 0; }
                    }
                    tile.type = (ushort)type;
                    tile.wall = (ushort)wallType;
                    tile.frameX = d.FrameX;
                    tile.frameY = d.FrameY;
                    tile.wallFrameX(d.WFrameX);
                    tile.wallFrameY(d.WFrameY);
                    tile.slope(d.Slope);
                    tile.liquid = d.Liquid;
                    tile.color(d.Color);
                    tile.wallColor(d.WallColor);
                    tile.wire(d.Wire[0] > 0);
                    tile.wire2(d.Wire[1] > 0);
                    tile.wire3(d.Wire[2] > 0);
                    tile.wire4(d.Wire[3] > 0);
                    tile.active(d.Active);
                }
            }
        }
    }
    public struct TileSaveData : TagSerializable
    {
        public bool Active;
        public string Tile;
        public string Wall;
        public short FrameX;
        public short FrameY;
        public short WFrameX;
        public short WFrameY;
        public byte Slope;
        public byte Liquid;
        public byte Color;
        public byte WallColor;
        public byte[] Wire;
        public TileSaveData(bool active, string tile, string wall, short frameX, short frameY, short wFrameX, short wFrameY, byte slope, byte liquid, byte color, byte wallColor, byte[] wire)
        {
            Active = active;
            Tile = tile;
            Wall = wall;
            FrameX = frameX;
            FrameY = frameY;
            WFrameX = wFrameX;
            WFrameY = wFrameY;
            Slope = slope;
            Liquid = liquid;
            Color = color;
            WallColor = wallColor;
            Wire = wire;
        }
        public static Func<TagCompound, TileSaveData> DESERIALIZER = s => DeserializeData(s);
        public static TileSaveData DeserializeData(TagCompound tag)
        {
            return new TileSaveData(
            tag.GetBool("Active"),
            tag.GetString("Tile"),
            tag.GetString("Wall"),
            tag.GetShort("FrameX"),
            tag.GetShort("FrameY"),
            tag.GetShort("WFrameX"),
            tag.GetShort("WFrameY"),
            tag.GetByte("Slope"),
            tag.GetByte("Liquid"),
            tag.GetByte("Color"),
            tag.GetByte("WallColor"),
            tag.GetByteArray("Wire")
            );
        }

        public TagCompound SerializeData()
        {
            return new TagCompound()
            {
                ["Active"] = Active,
                ["Tile"] = Tile,
                ["Wall"] = Wall,
                ["FrameX"] = FrameX,
                ["FrameY"] = FrameY,
                ["WFrameX"] = WFrameX,
                ["WFrameY"] = WFrameY,
                ["Slope"] = Slope,
                ["Liquid"] = Liquid,
                ["Color"] = Color,
                ["WallColor"] = WallColor,
                ["Wire"] = Wire
            };
        }
    }
}
