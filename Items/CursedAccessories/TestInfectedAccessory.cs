using Terraria;

namespace StarlightRiver.Items.CursedAccessories
{
    class TestInfectedAccessory : InfectedAccessory
    {
        public override string Texture => "StarlightRiver/Items/CursedAccessories/TestBlessedAccessory";
        public override bool Autoload(ref string name) { return true; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Ejaculation");
        }
        public override void SetDefaults()
        {
            item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
        }
    }
}
