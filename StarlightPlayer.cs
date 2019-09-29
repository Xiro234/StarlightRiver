using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System;
using StarlightRiver.GUI;
using StarlightRiver.Ability;

namespace StarlightRiver
{
    public class StarlightPlayer : ModPlayer
    {
        public bool DarkSlow = false;

        public bool VitricSet = false;
        public override void PreUpdate()
        {
            Stamina.visible = false;
            for (int k = 0; k < player.GetModPlayer<AbilityHandler>().unlock.Length; k++)
            {
                if (player.GetModPlayer<AbilityHandler>().unlock[k] == 1)
                {
                    Stamina.visible = true;
                }
            }

            if (DarkSlow)
            {
                player.velocity.X *= 0.8f;
            }
            DarkSlow = false;

            //Set Bonuses-----------------------------------------------------------------------------
            
            if(VitricSet)
            {

            }
        }       
    }
}
