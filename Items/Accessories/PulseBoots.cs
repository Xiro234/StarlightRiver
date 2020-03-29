using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Accessories
{
	[AutoloadEquip(EquipType.Shoes)]
    public class PulseBoots : ModItem //WIP, this item does not work fluently with vanilla rocket boots and double jumps
    { //needs sound / particles and number tweaking
        private bool doubleJumped = false;
        private bool releaseJump = false;
        static int maxSpeed = 15;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pulse Boots");
            Tooltip.SetDefault("Rocket Power!");
        }

        public override void SetDefaults()
        {
            item.width = 21;
            item.height = 19;
            item.accessory = true;
        }

        public override void PostUpdate()
        {
            base.PostUpdate();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            //Main.NewText(player.velocity.X);
        }
        public override void UpdateEquip(Player player)
        {
            if (!player.controlJump && player.velocity.Y != 0)
            {
                releaseJump = true;
            }
            if (player.controlJump && player.velocity.Y != 0 && releaseJump && !doubleJumped)
            {
                doubleJumped = true;
                player.velocity.Y = -8; //base upward jump

                if ((player.controlLeft && player.controlRight) || (!player.controlLeft && !player.controlRight))
                {
                    player.velocity.Y += -2;//if neither or both, then slightly higher jump
                }
                else if (player.controlLeft)
                {
                    if (player.velocity.X < 0 && player.velocity.X > -maxSpeed)
                    {
                        player.velocity.X += ((-maxSpeed - player.velocity.X) / 2); //adds about 6 vel to player when starting at 2 (walking speed), for a total of 8
                    }
                    else if (player.velocity.X > 0)
                    {
                        player.velocity.X = -6; //u turn
                    }
                }
                else if (player.controlRight)
                {
                    if (player.velocity.X > 0 && player.velocity.X < maxSpeed)
                    {
                        //Main.NewText(player.velocity.X + "<" + maxSpeed);

                        //Main.NewText("before" + player.velocity.X);
                        player.velocity.X += ((maxSpeed - player.velocity.X) / 2);
                        //Main.NewText("after" + player.velocity.X);
                    }
                    else if (player.velocity.X < 0)
                    {
                        //Main.NewText("u-turn right");
                        player.velocity.X = 6;
                    }
                }

                /*if (player.velocity.X != 0)
                {
                    player.velocity.X = (10 * player.direction);
                }*/
            }
            if (player.velocity.Y == 0)
            {
                releaseJump = false;
                doubleJumped = false;
            }
        }
    }
}