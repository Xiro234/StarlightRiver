using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Temple
{
    class TempleRune : ModItem
    {
        private int RuneTimer;
        public override bool Autoload(ref string name)
        {
            StarlightPlayer.PreHurtEvent += PreHurtRune;
            return true;
        }

        private bool PreHurtRune(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            Main.NewText("Fuck my pussy daddy!");
            return true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune of Warding");
            Tooltip.SetDefault("Periodically gain +5 Defense");
        }
        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Blue;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            RuneTimer++;
            if(RuneTimer < 300)
            {
                Lighting.AddLight(player.Center, new Vector3(1, 0.5f, 0.2f) * 0.2f);
                player.statDefense += 5;
                for(float k = (RuneTimer % 5) * 0.1f; k < 6.28f; k+= 0.5f)
                {
                    Vector2 off = new Vector2((float)Math.Cos(k + RuneTimer/100f), (float)Math.Sin(k + RuneTimer / 100f) * 1.5f);
                    Dust d = Dust.NewDustPerfect(player.Center, ModContent.DustType<Dusts.PlayerFollowOrange>(), off * 24);
                    d.customData = player.whoAmI;
                }
            }
            if (RuneTimer > 600) RuneTimer = 0;
        }
    }
}
