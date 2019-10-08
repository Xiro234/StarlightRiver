using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.CursedAccessories
{
    class TestCursedAccessory : CursedAccessory
    {
        public TestCursedAccessory() : base(ModContent.GetTexture("StarlightRiver/Items/CursedAccessories/TestCursedAccessoryGlow"))
        {

        }
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Cursed!");
            DisplayName.SetDefault("ExampleCursedAccessory");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
        }
    }
}
