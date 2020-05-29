﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace StarlightRiver.Items.CursedAccessories
{
    internal class InfectedAccessory : ModItem
    {
        public override bool Autoload(ref string name)
        {
            return false;
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            Main.NewText("Slot: " + slot, 255, 255, 0);
            if (slot == 3) return false;
            if (!player.armor[slot - 1].IsAir) return false;
            if (slot > 7 + player.extraAccessorySlots) return false;

            Item blocker = new Item
            {
                type = ModContent.ItemType<Blocker>()
            };
            blocker.SetDefaults(ModContent.ItemType<Blocker>());
            (blocker.modItem as Blocker).Parent = item;
            player.armor[slot - 1] = blocker;
            return true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, "StarlightRiverInfectedWarning", "Infected! Requires 2 accessory slots")
            {
                overrideColor = new Color(100, 160, 120)
            };
            tooltips.Add(line);
        }
        public virtual bool SafePreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) { return true; }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (Main.LocalPlayer.armor.Any(n => n == item))
            {
                Texture2D tex = ModContent.GetTexture("StarlightRiver/GUI/InfectedGoop");
                spriteBatch.Draw(tex, position + new Vector2(-10, -35), tex.Frame(), Color.White);
            }

            return SafePreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
    }

    internal class Blocker : ModItem
    {
        public override string Texture => "StarlightRiver/Invisible";
        public Item Parent { get; set; }
        public override void SetDefaults()
        {
            item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Clear();
        }
        public override void UpdateEquip(Player player)
        {
            if (!player.armor.Any(n => n.type == Parent.type)) item.TurnToAir();
        }
        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(Parent)] = Parent
            };
        }
        public override void Load(TagCompound tag)
        {
            Parent = tag.Get<Item>(nameof(Parent));
        }
    }
}
