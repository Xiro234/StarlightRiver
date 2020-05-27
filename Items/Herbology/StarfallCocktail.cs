using StarlightRiver.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    class StarfallCocktail : QuickPotion
    {
        public StarfallCocktail() : base("Starfall Cocktail", "Increases the chance for fallen stars to fall", 36000, ModContent.BuffType<StarfallCocktailBuff>(), 3) { }
    }
}
