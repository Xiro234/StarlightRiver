using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Tiles
{
    internal abstract class DummyTile : ModTile
    {
        private readonly int DummyType;

        public DummyTile(int dummyType)
        {
            DummyType = dummyType;
        }
        public virtual bool SpawnConditions(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.frameX == 0 && tile.frameY == 0;
        }
        public virtual void SafeNearbyEffects(int i, int j, bool closer) { }
        public sealed override void NearbyEffects(int i, int j, bool closer)
        {
            if (!Main.tileFrameImportant[Type] || SpawnConditions(i, j))
            {
                if (!Main.projectile.Any(n => n.active && n.type == DummyType && n.position == new Vector2(i, j) * 16))
                    Projectile.NewProjectile(new Vector2(i, j) * 16, Vector2.Zero, DummyType, 0, 0);
            }
            SafeNearbyEffects(i, j, closer);
        }
    }
}
