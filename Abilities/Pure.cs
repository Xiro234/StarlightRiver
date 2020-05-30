using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Projectiles.Ability;
using System.Linq;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Abilities
{
    public class Pure : Ability
    {

        public Pure(Player player) : base(4, player)
        {

        }
        public override Texture2D Texture => ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Purity1");
        public override bool CanUse => !Main.projectile.Any(proj => proj.owner == player.whoAmI && proj.active && (proj.type == ModContent.ProjectileType<Purifier>() || proj.type == ModContent.ProjectileType<PurifierReturn>()));

        public override void OnCast()
        {
            Active = true;
            Main.PlaySound(SoundID.Item37);
            Cooldown = 600;
            //Filters.Scene.Activate("PurityFilter", player.Center + new Vector2(0, -40)).GetShader();
        }

        public override void InUse()
        {
            Projectile.NewProjectile(player.Center + new Vector2(16, -24), Vector2.Zero, ModContent.ProjectileType<Purifier>(), 0, 0, player.whoAmI);
            LegendWorld.PureTiles.Add((player.Center + new Vector2(16, -24)) / 16);

            Active = false;
            OnExit();

        }

        public override void OnExit()
        {

        }
    }
}
