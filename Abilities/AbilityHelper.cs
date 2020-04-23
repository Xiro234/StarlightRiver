using Microsoft.Xna.Framework;
using Terraria;

namespace StarlightRiver.Abilities
{
    //This class serves to simplify implementing ability interactions
    class AbilityHelper
    {
        public static bool CheckDash(Player player, Rectangle hitbox)
        {
            if (Collision.CheckAABBvAABBCollision(player.Hitbox.TopLeft(), player.Hitbox.Size(), hitbox.TopLeft(), hitbox.Size()) && player.GetModPlayer<AbilityHandler>().dash.Active)
            {
                return true;
            }
            return false;
        }

        public static bool CheckWisp(Player player, Rectangle hitbox)
        {
            if (Collision.CheckAABBvAABBCollision(player.Hitbox.TopLeft(), player.Hitbox.Size(), hitbox.TopLeft(), hitbox.Size()) && player.GetModPlayer<AbilityHandler>().wisp.Active)
            {
                return true;
            }
            return false;
        }

        public static bool CheckSmash(Player player, Rectangle hitbox)
        {
            if (Collision.CheckAABBvAABBCollision(player.Hitbox.TopLeft(), player.Hitbox.Size(), hitbox.TopLeft(), hitbox.Size()) && player.GetModPlayer<AbilityHandler>().smash.Active)
            {
                return true;
            }
            return false;
        }
    }
}
