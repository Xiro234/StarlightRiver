using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Core
{
    public partial class StarlightProjectile : GlobalProjectile
    {
        public delegate void PostAIDelegate(Projectile projectile);
        public static event PostAIDelegate PostAIEvent;
        public override void PostAI(Projectile projectile)
        {
            PostAIEvent?.Invoke(projectile);
        }

        public delegate void KillDelegate(Projectile projectile, int timeLeft);
        public static event KillDelegate KillEvent;
        public override void Kill(Projectile projectile, int timeLeft)
        {
            KillEvent?.Invoke(projectile, timeLeft);
        }
    }
}
