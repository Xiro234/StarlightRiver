using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Decoration
{
    internal class VineBannerItem : ModItem
    {
        private bool SecondPoint;
        private VineBannerEntity target;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vine Banner");
            Tooltip.SetDefault("Click 2 points to place a swaying vine");
        }

        public override void SetDefaults()
        {
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
        }

        public override bool UseItem(Player player)
        {
            if (SecondPoint && Main.tile[(Main.MouseWorld / 16).ToPoint16().X, (Main.MouseWorld / 16).ToPoint16().Y].active())
            {
                target.Endpoint = (Main.MouseWorld / 16).ToPoint16() - target.Position;
                target.Set = true;
                SecondPoint = false;
                target = null;
            }
            else if (!SecondPoint)
            {
                WorldGen.PlaceTile((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), TileType<VineBanner>());
                TileEntity.PlaceEntityNet((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), TileEntityType<VineBannerEntity>());
                if (TileEntity.ByPosition.ContainsKey((Main.MouseWorld / 16).ToPoint16())) target = (VineBannerEntity)TileEntity.ByPosition[(Main.MouseWorld / 16).ToPoint16()];
                SecondPoint = true;
            }
            return true;
        }
    }

    internal class VineBanner : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileBlockLight[Type] = false;
            Main.tileSolid[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(GetInstance<VineBannerEntity>().Hook_AfterPlacement, -1, 0, false);
            TileObjectData.addTile(Type);
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (i * 16 > Main.screenPosition.X && i * 16 < Main.screenPosition.X + Main.screenWidth && TileEntity.ByPosition.ContainsKey(new Point16(i, j)))
            {
                VineBannerEntity entity = TileEntity.ByPosition[new Point16(i, j)] as VineBannerEntity;

                Texture2D tex2 = GetTexture("StarlightRiver/Tiles/Decoration/VineBanner");
                Vector2 end = new Vector2(i - 0.5f, j) * 16 + entity.Endpoint.ToVector2() * 16;
                Vector2 oldPos = new Vector2(i, j) * 16;
                //Vector2 olderPos = new Vector2(i, j) * 16;
                float max = Vector2.Distance(end, new Vector2(i, j) * 16) / 5;
                for (int k = 0; k < max; k++)
                {
                    float off = -Math.Abs((i * 16 - end.X) / 6f) + (float)Math.Cos(StarlightWorld.rottime + i % 6) * 2;
                    float sin = (float)Math.Sin(StarlightWorld.rottime + i % 6);
                    Vector2 pos = Vector2.CatmullRom(new Vector2(i + sin, j + off) * 16, end, new Vector2(i + 1.5f, j + 1) * 16, new Vector2(i - sin, j + off) * 16, k / max);
                    Color color = Lighting.GetColor((int)pos.X / 16, (int)pos.Y / 16);
                    if (!entity.Set) color *= 0.2f;

                    if (Main.rand.Next(120) == 0)
                    {
                        Dust.NewDustPerfect(pos + new Vector2(0, 16), DustType<Dusts.GreenLeaf>(), new Vector2(1, 0));
                    }

                    pos += Helper.TileAdj * 16;

                    spriteBatch.Draw(tex2, pos - Main.screenPosition, new Rectangle(k % 3 * 16, 0, 16, 16), color, (pos - oldPos).ToRotation() + 1.57f, tex2.Size() / 2, 1, 0, 0);

                    //olderPos = oldPos;
                    oldPos = pos;
                }
            }
        }
    }

    internal class VineBannerEntity : ModTileEntity
    {
        internal Point16 Endpoint;
        internal bool Set;

        public override void Update()
        {
            if (!Set) Endpoint = (Main.MouseWorld / 16).ToPoint16() - Position;
            if (!ValidTile(Position.X, Position.Y)) Kill(Position.X, Position.Y);

            if (Set && (!Main.tile[Position.X + Endpoint.X, Position.Y + Endpoint.Y].active() ||
                (!Main.tile[Position.X, Position.Y + 1].active() && !Main.tile[Position.X, Position.Y - 1].active() && !Main.tile[Position.X + 1, Position.Y].active() && !Main.tile[Position.X - 1, Position.Y].active()) //totally floating
                ))
            {
                Kill(Position.X, Position.Y);
                WorldGen.KillTile(Position.X, Position.Y);
            }
        }

        public override bool ValidTile(int i, int j)
        {
            return (Main.tile[i, j].active() && Main.tile[i, j].type == TileType<VineBanner>());
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i, j, 3);
                NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type, 0f, 0, 0, 0);
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