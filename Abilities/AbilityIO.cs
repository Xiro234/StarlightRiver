using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader.IO;

namespace StarlightRiver.Abilities
{
    class AbilityIO
    {
        public static TagCompound Save(Ability ability)
        {
            var tag = new TagCompound();
            tag.Set("type", ability.GetType().ToString());
            tag.Set("locked", ability.Locked);
            return tag;
        }

        public static Ability Load(Player player, TagCompound tag)
        {
            string typ = tag.GetString("type");
            Ability ability = (Ability)System.Activator.CreateInstance(StarlightRiver.Instance.Code.GetTypes().FirstOrDefault(t => t.GetType().ToString() == typ), player);
            ability.Locked = tag.GetBool("locked");
            return ability;
        }
    }
}
