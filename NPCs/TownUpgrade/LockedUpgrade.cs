﻿using Microsoft.Xna.Framework;
using Terraria;

namespace StarlightRiver.NPCs.TownUpgrade
{
    class LockedUpgrade : TownUpgrade
    {
        public LockedUpgrade() : base("", "", "", "Locked", "") { }

        public override void ClickButton()
        {
            Main.NewText("Seek the voidsmith in hell for more information...", Color.Red);
        }
    }
}
