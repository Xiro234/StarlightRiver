using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Core
{
    internal partial class StarlightItem : GlobalItem
    {
        public Rectangle meleeHitbox;

        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => true;

        public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox) => meleeHitbox = hitbox;
    }
}
