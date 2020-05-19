using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Terraria.ModLoader.IO;

namespace StarlightRiver.Tiles.Decoration
{
    class VineBannerItem : Items.QuickTileItem
    {
        bool SecondPoint;
        VineBannerEntity target;
        public VineBannerItem() : base("Vine Banner", "Places a vine banner", ModContent.TileType<VineBanner>(), 0){}
        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
        }
        public override bool UseItem(Player player)
        {
            if (Vector2.Distance(player.Center, Main.MouseWorld) < 160)
            {
                if (SecondPoint)
                {
                    target.Endpoint = (Main.MouseWorld / 16).ToPoint16() - target.Position;
                    target.Set = true;
                    item.createTile = ModContent.TileType<VineBanner>();
                    SecondPoint = false;
                }
                else
                {
                    TileEntity.PlaceEntityNet((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), ModContent.TileEntityType<VineBannerEntity>());
                    if(TileEntity.ByPosition.ContainsKey((Main.MouseWorld / 16).ToPoint16())) target = (VineBannerEntity)TileEntity.ByPosition[(Main.MouseWorld / 16).ToPoint16()];
                    item.createTile = 0;
                    SecondPoint = true;
                }
            }
            return true;
        }
    }
    class VineBanner : ModTile
    {
        public override void SetDefaults()
        {

            Main.tileBlockLight[Type] = false;
            Main.tileSolid[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<VineBannerEntity>().Hook_AfterPlacement, -1, 0, false);
            TileObjectData.addTile(Type);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (TileEntity.ByPosition.ContainsKey(new Point16(i, j)))
            {
                VineBannerEntity entity = TileEntity.ByPosition[new Point16(i, j)] as VineBannerEntity;

                Texture2D tex2 = ModContent.GetTexture("StarlightRiver/Tiles/Decoration/VineBanner");
                Vector2 end = new Vector2(i, j) * 16 + entity.Endpoint.ToVector2() * 16;
                Vector2 oldPos = new Vector2(i, j) * 16;
                Vector2 olderPos = new Vector2(i, j) * 16;
                float max = Vector2.Distance(end, new Vector2(i, j) * 16) / 5;
                for (int k = 0; k < max; k++)
                {
                    float off = -Math.Abs((i * 16 - end.X) / 6f) + (float)Math.Cos(LegendWorld.rottime + i % 6) * 2;
                    float sin = (float)Math.Sin(LegendWorld.rottime + i % 6);
                    Vector2 pos = Vector2.CatmullRom(new Vector2(i + sin, j + off) * 16, end, new Vector2(i, j) * 16, new Vector2(i - sin, j + off) * 16, k / max);
                    Color color = Lighting.GetColor((int)pos.X / 16, (int)pos.Y / 16);
                    pos += Helper.TileAdj * 16;

                    spriteBatch.Draw(tex2, pos - Main.screenPosition, new Rectangle(k % 3 * 16, 0, 16, 16), color, (pos - oldPos).ToRotation() + 1.57f, tex2.Size() / 2, 1, 0, 0);

                    olderPos = oldPos;
                    oldPos = pos;
                }
            }



        }
    }
    class VineBannerEntity : ModTileEntity
    {
        internal Point16 Endpoint;
        internal bool Set;
        public override void Update()
        {
            if(!Set) Endpoint = (Main.MouseWorld / 16).ToPoint16() - Position;
        }
        public override bool ValidTile(int i, int j)
        {
            return (Main.tile[i, j].active() && Main.tile[i, j].type == ModContent.TileType<VineBanner>());
        }
        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
        {
            if (Main.netMode == 1)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i, j, 3);
                NetMessage.SendData(87, -1, -1, null, i, j, Type, 0f, 0, 0, 0);
                return -1;
            }
            return Place(i, j);
        }
        public override TagCompound Save()
        {
            return new TagCompound()
            {
                ["Set"] = Set,
                ["PosX"] = Endpoint.X,
                ["PosY"] = Endpoint.Y
            };
        }
        public override void Load(TagCompound tag)
        {
            Set = tag.GetBool("Set");
            if (!Set)
            {
                Kill(Position.X, Position.Y);
                WorldGen.KillTile(Position.X, Position.Y);
            }
            Endpoint = new Point16(tag.GetShort("PosX"), tag.GetShort("PosY"));
        }
    }
}
