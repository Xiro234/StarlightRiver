using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

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
            void jumpSide(int side)
            {
                float velSide = player.velocity.X * side;
                if (velSide > 0 && velSide < maxSpeed)
                {
                    player.velocity.X += (((maxSpeed * side) - player.velocity.X) / 2);
                }
                else if (velSide < 0)
                {
                    player.velocity.X = (6 * side);
                }

                for (int y = 0; y < 10; y++)//placeholder dash dust
                {
                    Dust.NewDust(player.BottomLeft + player.velocity, player.width, (int)player.velocity.Y, 6, 3 * -side, 0, 0, default, 2);
                }
            }

            if (!player.controlJump && player.velocity.Y != 0)
            {
                releaseJump = true;
            }
            if (player.controlJump && player.velocity.Y != 0 && releaseJump && !doubleJumped)
            {
                doubleJumped = true;
                player.velocity.Y = -8; //base upward jump
                Main.PlaySound(SoundID.Item61, player.Center);

                if ((player.controlLeft && player.controlRight) || (!player.controlLeft && !player.controlRight))
                {
                    player.velocity.Y += -2;//if neither or both, then slightly higher jump

                    for (int y = 0; y < 8; y++)//placeholder dash dust
                    {
                        Dust.NewDust(player.BottomLeft + player.velocity, player.width, (int)player.velocity.Y, 6, 0, 0, 0, default, 2);
                    }
                }
                else if (player.controlLeft)//-1
                {
                    jumpSide(-1);
                }
                else if (player.controlRight)//1
                {
                    jumpSide(1);
                }
            }
            if (player.velocity.Y == 0)
            {
                releaseJump = false;
                doubleJumped = false;
            }
        }
    }
}