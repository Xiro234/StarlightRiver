using Microsoft.Xna.Framework;
using Terraria;

namespace StarlightRiver.Items.StarJuice
{
    class StarLamp : StarjuiceStoringItem
    {
        public StarLamp() : base(500) { }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starlight Illuminator");
            Tooltip.SetDefault("Consumes starlight to produce light");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.useStyle = 1;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 2;
        }

        public override void UpdateInventory(Player player)
        {
            if (charge > 0)
            {
                if (Main.time % 30 == 0 && !Main.fastForwardTime) charge--;
                Lighting.AddLight(player.Center, new Vector3(1f, 1.7f, 1.9f) * (charge / (float)maxCharge) * 0.4f);
            }
        }

        public override void PostUpdate()
        {
            if (charge > 0)
            {
                if (Main.time % 30 == 0 && !Main.fastForwardTime) charge--;
                Lighting.AddLight(item.Center, new Vector3(1, 2.2f, 2) * (charge / (float)maxCharge) * 0.4f);
            }
        }

    }
}
