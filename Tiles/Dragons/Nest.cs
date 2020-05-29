using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using StarlightRiver.Dragons;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Dragons
{
    internal class Nest : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<NestEntity>().Hook_AfterPlacement, -1, 0, false);
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Dragon Nest");//Map name
            AddMapEntry(new Color(255, 255, 100), name);
            dustType = ModContent.DustType<Dusts.Gold2>();
            disableSmartCursor = true;
        }

        public override bool NewRightClick(int i, int j)
        {
            NestEntity entity = GetTE(i, j);
            if (entity == null) return false;

            Item item = Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem];

            if (entity.owner == null && item.type == ModContent.ItemType<Items.Dragons.Egg>())
            {
                entity.owner = Main.LocalPlayer; //Sets the ID if the nest has not yet been claimed
                entity.dragon.data.stage = GrowthStage.baby;
                int index = NPC.NewNPC(entity.Position.X * 16 + 27, entity.Position.Y * 16, ModContent.NPCType<DragonEgg>());
                (Main.npc[index].modNPC as DragonEgg).nest = entity;
                item.TurnToAir(); //Absorbs the egg
            }

            return true;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            NestEntity entity = GetTE(i, j);
            if (entity == null || entity.owner == null) return;

            if (tile.frameX == 0 && tile.frameY == 0)
            {
                Vector2 center = (new Vector2(i + 1, j - 1) + Helper.TileAdj) * 16 + new Vector2(8, 0);
                spriteBatch.DrawString(Main.fontItemStack, entity.owner.name + "'s dragon\n" + entity.dragon.data.name, center + new Vector2(0, -64) - Main.screenPosition - Main.fontItemStack.MeasureString(entity.dragon.data.name) / 2, Color.White);
            }
        }

        private NestEntity GetTE(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int left = i - (tile.frameX / 18);
            int top = j - (tile.frameY / 18);
            int index = ModContent.GetInstance<NestEntity>().Find(left, top);

            return index == -1 ? null : (NestEntity)TileEntity.ByID[index];
        }
    }

    internal class NestEntity : ModTileEntity
    {
        public Player owner;
        public DragonHandler dragon => owner.GetModPlayer<DragonHandler>();
        public override bool ValidTile(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.active() && tile.type == ModContent.TileType<Nest>() && tile.frameX == 0 && tile.frameY == 0;
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
        public override void Update()
        {
            owner.GetModPlayer<DragonHandler>().data = dragon.data;
        }
    }

}
