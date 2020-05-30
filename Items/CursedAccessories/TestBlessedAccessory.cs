using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.CursedAccessories
{
    internal class TestBlessedAccessory : BlessedAccessory
    {
        public TestBlessedAccessory() : base(ModContent.GetTexture("StarlightRiver/Items/CursedAccessories/TestBlessedAccessoryGlow"))
        {
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Blessed");
            DisplayName.SetDefault("ExampleBlessedAccessory");
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

        public override bool TestCondition()
        {
            return Main.player[item.owner].statLife <= 100;
        }
    }
}