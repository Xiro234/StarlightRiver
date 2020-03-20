using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.IO;

namespace StarlightRiver.Dragons
{
    public enum Roar
    {
        normal = 0,
        scalie = 1
    };
    public class DragonHandler : ModPlayer
    {
        public Color hornColor = new Color(180, 140, 60);
        public Color scaleColor = new Color(255, 50, 50);
        public Color bellyColor = new Color(250, 220, 130);
        public Color eyeColor = new Color(100, 100, 220);

        public string name = "[PH]DefaultDragonName";

        public Roar roar = Roar.normal;

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            if (!mediumcoreDeath)
            {
                Item egg = new Item();
                egg.SetDefaults(ModContent.ItemType<Items.Dragons.Egg>());
                items.Add(egg);
            }
        }
        public override bool ShiftClickSlot(Item[] inventory, int context, int slot)
        {
            if (inventory[slot].modItem != null && inventory[slot].modItem is Items.SoulboundItem) return false;
            else return default;
        }
        public override TagCompound Save()
        {
            return new TagCompound()
            {
                [nameof(hornColor)] = hornColor,
                [nameof(scaleColor)] = scaleColor,
                [nameof(bellyColor)] = bellyColor,
                [nameof(eyeColor)] = eyeColor,

                [nameof(name)] = name,

                [nameof(roar)] = (int)roar
            };
        }
        public override void Load(TagCompound tag)
        {
            hornColor = tag.Get<Color>(nameof(hornColor));
            scaleColor = tag.Get<Color>(nameof(scaleColor));
            bellyColor = tag.Get<Color>(nameof(bellyColor));
            eyeColor = tag.Get<Color>(nameof(eyeColor));

            name = tag.GetString(nameof(name));

            roar = (Roar)tag.GetInt(nameof(roar));
        }
    }
}
