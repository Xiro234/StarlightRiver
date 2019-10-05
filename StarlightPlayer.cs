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
using StarlightRiver.Abilities;

namespace StarlightRiver
{
    public class StarlightPlayer : ModPlayer
    {
        public bool DarkSlow = false;

        public bool VitricSet = false;
		
		public int InvertGrav = 0;
		public override void PreUpdateBuffs()
        {

			if (InvertGrav > 0)
			{
                //Main.NewText("Invert: true");
				player.gravControl = true;
				player.gravDir = -1f;
			}
            else
            {
                //Main.NewText("Invert: false");
            }
        }
		
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

		public override void ResetEffects()
        {	
			//InvertGrav = false;
		}

        public override void PostUpdate()
        {
            //Main.NewText(player.velocity);
            if (InvertGrav > 0)
            {
                if (InvertGrav == 1 && player.velocity.Y < 5 && player.velocity.Y > -5)
                {
                    player.velocity.Y = 0;
                }
                    --InvertGrav;
            }
        }
    }
}
