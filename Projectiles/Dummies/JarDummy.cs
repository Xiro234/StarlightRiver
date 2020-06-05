using Microsoft.Xna.Framework;
using StarlightRiver.Abilities;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Dummies
{
    internal class JarDummy : Dummy
    {
        public JarDummy() : base(ModContent.TileType<Tiles.Temple.JarTall>(), 32, 32) { }
        public override void Collision(Player player)
        {
            if (AbilityHelper.CheckDash(player, projectile.Hitbox))
            {
                WorldGen.KillTile(ParentX, ParentY);
            }
        }
    }
}