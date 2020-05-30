﻿using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.RiftCrafting.Tier1
{
    internal class TestRecipe1 : RiftRecipe
    {
        private static readonly List<RiftIngredient> ingredients = new List<RiftIngredient>()
        {
            new RiftIngredient(ModContent.ItemType<Items.Vitric.VitricOre>(), 4 ),
            new RiftIngredient(ItemID.IronHammer, 1 )
        };

        private static readonly List<int> pool = new List<int>()
        {
            NPCID.Mummy,
            NPCID.Unicorn
        };

        public TestRecipe1() : base(ingredients, pool, 1, ModContent.ItemType<Items.Vitric.VitricHammer>())
        {
        }
    }

    internal class TestRecipe2 : RiftRecipe
    {
        private static readonly List<RiftIngredient> ingredients = new List<RiftIngredient>()
        {
            new RiftIngredient(ModContent.ItemType<Items.Vitric.VitricOre>(), 3 ),
            new RiftIngredient(ItemID.IronPickaxe, 1 )
        };

        private static readonly List<int> pool = new List<int>()
        {
            NPCID.Mummy,
            NPCID.Unicorn
        };

        public TestRecipe2() : base(ingredients, pool, 1, ModContent.ItemType<Items.Vitric.VitricPick>())
        {
        }
    }

    internal class TestRecipe3 : RiftRecipe
    {
        private static readonly List<RiftIngredient> ingredients = new List<RiftIngredient>()
        {
            new RiftIngredient(ModContent.ItemType<Items.Vitric.VitricOre>(), 100 ),
            new RiftIngredient(ItemID.IronAxe, 1 ),
        };

        private static readonly List<int> pool = new List<int>()
        {
            NPCID.Mummy,
            NPCID.Unicorn
        };

        public TestRecipe3() : base(ingredients, pool, 1, ModContent.ItemType<Items.Vitric.VitricAxe>())
        {
        }
    }
}