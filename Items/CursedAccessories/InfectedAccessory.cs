using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;

namespace StarlightRiver.Items.CursedAccessories
{
    class InfectedAccessory : ModItem
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

            Item blocker = new Item();
            blocker.type = ModContent.ItemType<Blocker>();
            blocker.SetDefaults(ModContent.ItemType<Blocker>());
            (blocker.modItem as Blocker).Parent = item; 
            player.armor[slot - 1] = blocker;
            return true;
        }
    }

    class Blocker : ModItem
    {
        public override string Texture => "StarlightRiver/Invisible";
        public Item Parent { get; set; }
        public override void SetDefaults()
        {
            item.accessory = true;
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
