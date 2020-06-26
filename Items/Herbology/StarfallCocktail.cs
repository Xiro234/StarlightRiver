using static Terraria.ModLoader.ModContent;
using StarlightRiver.Buffs;

namespace StarlightRiver.Items.Herbology
{
    internal class StarfallCocktail : QuickPotion
    {
        public StarfallCocktail() : base("Starfall Cocktail", "Increases the chance for fallen stars to fall", 36000, BuffType<StarfallCocktailBuff>(), 3)
        {
        }
    }
}