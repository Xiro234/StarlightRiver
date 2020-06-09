using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using StarlightRiver.Items.Armor.Starwood;

namespace StarlightRiver.Items.Armor.Starwood
{
    [AutoloadEquip(EquipType.Head)]
    public class StarwoodHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starwood Hat");
            Tooltip.SetDefault("5% increased magic damage");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 26;
            item.value = 8000;
            item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 0.05f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class StarwoodChest : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starwood Robes");
            Tooltip.SetDefault("Increases max mana by 20");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 30;
            item.value = 6000;
            item.defense = 3;
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<StarwoodHat>() && legs.type == ModContent.ItemType<StarwoodBoots>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Wip";
            //StarlightPlayer starlightPlayer = player.GetModPlayer<StarlightPlayer>();
            StarlightPlayer mp = player.GetModPlayer<StarlightPlayer>();
            mp.starwoodArmorComplete = true;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class StarwoodBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starwood Boots");
            Tooltip.SetDefault("5% increased magic critial strike change");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = 4000;
            item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 5;
        }
    }

    internal class ManastarPickup : GlobalItem
    {
        public override bool OnPickup(Item item, Player player)
        {
            if (item.type == ItemID.Star)
            {
                StarlightPlayer mp = player.GetModPlayer<StarlightPlayer>();
                mp.StartStarwoodEmpowerment();
            }
            return base.OnPickup(item, player);
        }
    }

    internal class ManastarDrops : GlobalNPC
    {
        public bool DropStar = false;
        public override bool InstancePerEntity => true;
        public override void NPCLoot(NPC npc)
        {
            if (DropStar && Main.rand.Next(2) == 0)
            {
                Item.NewItem(npc.Center, ItemID.Star);
            }
        }
    }
}
namespace StarlightRiver
{
    public partial class StarlightPlayer : ModPlayer
    {
        public bool starwoodArmorComplete = false;
        public bool Empowered = false;
        private int EmpowermentTimer = 0;

        public void StartStarwoodEmpowerment()
        {
            if (starwoodArmorComplete)//checks if complete, not completely needed but is there so empowered isnt true for a brief moment
            {
                if (!Empowered)
                {
                    for (int k = 0; k < 80; k++)//pickup sfx
                    {
                        Dust.NewDustPerfect(player.Center, ModContent.DustType<Dusts.BlueStamina>(), (Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(0.8f, 1.2f)) * new Vector2(1f, 1.5f), 0, default, 1.5f);
                    }
                }
                else
                {
                    for (int k = 0; k < 40; k++)//reduced pickup sfx if its already active
                    {
                        Dust.NewDustPerfect(player.Center, ModContent.DustType<Dusts.BlueStamina>(), (Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(0.5f, 0.8f)) * new Vector2(1f, 1.5f), 0, default, 1.5f);
                    }
                }
                Empowered = true;
                EmpowermentTimer = 0;//resets timer
            }
        }

        private void EmpowermentUpdate()
        {
            if (Empowered)//most stuff should just check if Empowerment is true, checking if the setis completed can be done but shouldn't be needed
            {
                //240 = 4 seconds
                if (EmpowermentTimer <= 240 && starwoodArmorComplete) //checks if complete to disable empowerment if set is removed
                { 
                    for (int k = 0; k < 1; k++)//temp sfx
                    {
                        Dust.NewDustPerfect(player.position + new Vector2(Main.rand.Next(player.width), Main.rand.Next(player.height)), ModContent.DustType<Dusts.BlueStamina>(), new Vector2(0, -1).RotatedByRandom(0.8f) * Main.rand.NextFloat(1.0f, 1.4f), 0, default, 1.2f);
                    }
                    EmpowermentTimer++;
                }
                else { EmpowermentTimer = 0; Empowered = false; }
            }
        }

        private void ResetStarwoodEffects()
        {
            starwoodArmorComplete = false;
        }

        private void OnHitStarwood(NPC target)
        {
            if (starwoodArmorComplete)
            {
                target.GetGlobalNPC<ManastarDrops>().DropStar = true;
            }
        }
    }
}