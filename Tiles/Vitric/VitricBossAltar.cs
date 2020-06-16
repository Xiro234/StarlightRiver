using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Projectiles.Dummies;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Vitric
{
    internal class VitricBossAltar : ModTile
    {
        private Projectile Dummy = new Projectile();

        public override void SetDefaults()
        {
            QuickBlock.QuickSetFurniture(this, 5, 7, ModContent.DustType<Dusts.Air>(), SoundID.Tink, false, -1, new Color(200, 113, 113), false, false, "Ceiro's Altar");
            minPick = int.MaxValue;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.frameX % 90 == 0 && tile.frameY == 0)
            {
                if (!(Dummy.modProjectile is VitricAltarDummy && Main.projectile.Any(n => n == Dummy) && Dummy.active && Dummy.Hitbox.Contains(new Point(i * 16 + 30, j * 16 + 30))))
                {
                    Dummy = Main.projectile[Projectile.NewProjectile(new Vector2(i, j) * 16 + new Vector2(40, 56), Vector2.Zero, ModContent.ProjectileType<VitricAltarDummy>(), 0, 0)];
                }
            }
        }

        public override bool NewRightClick(int i, int j)
        {
            if (StarlightWorld.GlassBossOpen && Dummy.modProjectile is VitricAltarDummy) (Dummy.modProjectile as VitricAltarDummy).SpawnBoss();
            return true;
        }
    }
}