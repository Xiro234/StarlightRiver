using StarlightRiver.Buffs;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    internal class StarfallCocktail : QuickPotion
    {
        public StarfallCocktail() : base("Starfall Cocktail", "Increases the chance for fallen stars to fall", 36000, ModContent.BuffType<StarfallCocktailBuff>(), 3) { }
    }
}
