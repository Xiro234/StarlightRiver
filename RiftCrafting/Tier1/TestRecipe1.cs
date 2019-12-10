using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarlightRiver.RiftCrafting.Tier1
{
    class TestRecipe1 : RiftRecipe
    {
        static List<RiftIngredient> ingredients = new List<RiftIngredient>()
        {
            new RiftIngredient(ModContent.ItemType<Items.Vitric.VitricOre>(), 1 ),
            new RiftIngredient(ItemID.IronHammer, 1 )
        };

        static List<int> pool = new List<int>()
        {
            NPCID.Mummy,
            NPCID.Unicorn
        };
        public TestRecipe1() : base(ingredients, pool, 1, ModContent.ItemType<Items.Vitric.VitricHammer>()) { }
    }

    class TestRecipe2 : RiftRecipe
    {
        static List<RiftIngredient> ingredients = new List<RiftIngredient>()
        {
            new RiftIngredient(ModContent.ItemType<Items.Vitric.VitricOre>(), 1 ),
            new RiftIngredient(ItemID.IronPickaxe, 1 )
        };

        static List<int> pool = new List<int>()
        {
            NPCID.Mummy,
            NPCID.Unicorn
        };
        public TestRecipe2() : base(ingredients, pool, 1, ModContent.ItemType<Items.Vitric.VitricPick>()) { }
    }

    class TestRecipe3 : RiftRecipe
    {
        static List<RiftIngredient> ingredients = new List<RiftIngredient>()
        {
            new RiftIngredient(ModContent.ItemType<Items.Vitric.VitricOre>(), 1 ),
            new RiftIngredient(ItemID.IronAxe, 1 ),
        };

        static List<int> pool = new List<int>()
        {
            NPCID.Mummy,
            NPCID.Unicorn
        };
        public TestRecipe3() : base(ingredients, pool, 1, ModContent.ItemType<Items.Vitric.VitricAxe>()) { }
    }

}
