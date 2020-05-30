using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Ultrashark
{
    public class Ultrashark : ModItem, IGlowingItem
    {
        #region values

        public float spinup;

        public bool turretDeployed = false; //true after the player pressed RMB
        public bool turretSetup = false; //true after the turret setup animation is complete, the player can now shoot

        public int turretDirection = 0; //set to player direction upon pressing RMB
        public float sharkRotation = 0; //cached rotation, thats about it
        public int sharkFrame = 1; //self explanitory
        public int sharkFrameCount = 10;

        public int standFrame = 1;
        public int standFrameCount = 9;

        #endregion values

        #region methods

        public void SpawnCasing(Player player, Vector2 velocity) //pos infront of player pretty much
        {
            Gore.NewGore(GetSharkPos(player), (-velocity + new Vector2(0, -1) + new Vector2(Main.rand.NextFloat(3f) - 1.5f, -2)) * 0.25f, mod.GetGoreSlot("Gores/UltrasharkCasing"));
        }

        public Vector2 GetSharkPos(Player player) //pos infront of player pretty much
        {
            return player.Center + new Vector2(turretDirection * player.width, -10);
        }

        public Vector2 GetStandPos(Player player) //pos where we draw the stand
        {
            return GetSharkPos(player) + new Vector2(0, 17);
        }

        public static Vector2 GetMousePos() //player's mouse position
        {
            return new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y);
        }

        public float GetSharkRotation(Player player) //used to set sharkRotation
        {
            float rotation = Vector2.Normalize(GetMousePos() - GetSharkPos(player)).ToRotation();
            float anglediff = ((turretDirection == 1 ? 0 : 3.14f) - rotation + 9.42f) % 6.28f - 3.14f;
            float f = 1.256f;
            return anglediff <= f && anglediff >= -f ? rotation : sharkRotation;
        }

#pragma warning disable IDE0060 // Remove unused parameter
        public void CompleteSetup(Player player)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            //put some completion vfx here idk
            turretSetup = true;
            // TODO- ultrashark
        }

        #endregion methods

        #region drawing

        public void DrawStand(PlayerDrawInfo info) //should all be obivous
        {
            Texture2D standTexture = mod.GetTexture("Items/Ultrashark/StandDelpoyAnimation");
            int frameHeight = standTexture.Height / standFrameCount;
            int frame = frameHeight * standFrame;
            Player player = info.drawPlayer;
            Vector2 standPos = GetStandPos(player) - Main.screenPosition + new Vector2(0f, player.gfxOffY);
            Main.playerDrawData.Add(new DrawData(
                standTexture,
                standPos,  //position
                new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, frame, standTexture.Width, frameHeight)), //source
                Lighting.GetColor((int)GetStandPos(player).X / 16, (int)GetStandPos(player).Y / 16), //color
                0, //rotation
                new Vector2(standTexture.Width / 2f, frameHeight / 2f), //origin
                1f, //scale
                turretDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0));
        }

        public void DrawGun(PlayerDrawInfo info)
        {
            Texture2D sharkTexture = mod.GetTexture("Items/Ultrashark/ShootingAnimation");
            int frameHeight = sharkTexture.Height / sharkFrameCount;
            int frame = frameHeight * sharkFrame;
            Player player = info.drawPlayer;
            Vector2 sharkPos = GetSharkPos(player) - Main.screenPosition + new Vector2(0f, player.gfxOffY);
            sharkRotation = GetSharkRotation(player);
            Main.playerDrawData.Add(new DrawData(
                sharkTexture,
                sharkPos,  //position
                new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, frame, sharkTexture.Width, frameHeight)), //source
                Lighting.GetColor((int)GetSharkPos(player).X / 16, (int)GetSharkPos(player).Y / 16), //color
                sharkRotation, //rotation
                new Vector2(sharkTexture.Width / 2f, frameHeight / 2f), //origin
                1f, //scale
                turretDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically, 0));
        }

        public void DrawGlowmask(PlayerDrawInfo info)
        {
            if (turretDeployed)
            {
                DrawStand(info);
                DrawGun(info);
            }
        }

        #endregion drawing

        #region item

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 24;
            item.useTime = 24;
            item.shootSpeed = 20f;
            item.knockBack = 2f;
            item.UseSound = SoundID.Item11;
            item.width = 64;
            item.height = 24;
            item.damage = 28;
            item.rare = ItemRarityID.LightRed;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.noMelee = true;
            item.autoReuse = true;
            item.useTurn = false;
            item.useAmmo = AmmoID.Bullet;
            item.ranged = true;
            item.shoot = ProjectileID.PurificationPowder;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultrashark");
            Tooltip.SetDefault("Okay so like it goes like pew... pew... pewpew... pewpew... pewpewpew... pewpewpewpew... pewpewpewpewpepwepwpewpepwpewepwepwpewpepwepwepwpewp\nAnd like, its innacurate\nBut like, you can right click it to like mount it and it like a turret\nWhile a turret exist you can like, shoot, way more accurately, faster, and spin up faster too damn");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (turretDeployed && player.altFunctionUse == 2) //only use right click if turret aint deployed
            {
                return false;
            }
            if (turretDeployed && !turretSetup) //cant use right click if turret isnt setup
            {
                return false;
            }

            if (player.altFunctionUse == 2 || turretDeployed) //dont draw the gun
            {
                item.noUseGraphic = true;
            }
            else
            {
                item.noUseGraphic = false;
            }
            return base.CanUseItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override float UseTimeMultiplier(Player player)
        {
            return 1 + spinup;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (spinup < (turretDeployed ? 4f : 3f)) //spinup
            {
                spinup += (turretDeployed ? 0.5f : 0.2f);
            }
            if (turretDeployed) //shoot as stand
            {
                sharkFrame++;
                if (sharkFrame >= sharkFrameCount)
                {
                    sharkFrame = 0;
                }
                Vector2 speed = GetSharkRotation(player).ToRotationVector2() * 20f;
                speedX = speed.X;
                speedY = speed.Y;
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
                speedX = perturbedSpeed.X;
                speedY = perturbedSpeed.Y;
                position = GetSharkPos(player);
                SpawnCasing(player, perturbedSpeed);
                return true;
            }
            else if (player.altFunctionUse == 2) //summon stand
            {
                if (player.velocity.X >= -0.2f || player.velocity.X <= 0.2f || player.velocity.Y >= -0.2f || player.velocity.Y >= 0.2f)
                {
                    turretDeployed = true;
                    turretDirection = player.direction;
                }
                return false;
            }
            Vector2 perturbedSpeedAgain = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8));
            speedX = perturbedSpeedAgain.X;
            speedY = perturbedSpeedAgain.Y;
            SpawnCasing(player, perturbedSpeedAgain);
            return true;
        }

        #endregion item
    }

    public class UltrasharkHandler : ModPlayer
    {
        #region player

        public override void PostUpdate()
        {
            if (player.HeldItem.type == ModContent.ItemType<Ultrashark>())
            {
                Ultrashark item = (Ultrashark)player.HeldItem.modItem;
                if (item.turretDeployed)
                {
                    if (item.turretDirection != 0) //no turn
                    {
                        player.direction = item.turretDirection;
                    }
                }
            }
        }

        public override void PreUpdate()
        {
            if (player.HeldItem.type == ModContent.ItemType<Ultrashark>())
            {
                Ultrashark item = (Ultrashark)player.HeldItem.modItem;
                if (player.releaseUseItem) //reset spinup
                {
                    item.spinup = 0;
                }
                if (item.turretDeployed)
                {
                    if (player.velocity.X <= -0.2f || player.velocity.X >= 0.2f || player.velocity.Y <= -0.2f || player.velocity.Y >= 0.2f) //cancel if moving
                    {
                        item.turretDeployed = false;
                        item.turretSetup = false;
                        item.standFrame = 0;
                        item.sharkFrame = 0;
                        item.spinup = 0;
                    }
                }

                #region animation and setup

                if (item.turretDeployed)
                {
                    if (Main.time % 6 == 0) //animate stand
                    {
                        if (item.standFrame < item.standFrameCount - 1)
                        {
                            item.standFrame++;
                        }
                        else if (!item.turretSetup)
                        {
                            item.CompleteSetup(player);
                        }
                    }
                    if (Main.time % 20 - item.spinup * 6f == 0) //animate gun
                    {
                        if (item.sharkFrame < item.sharkFrameCount - 1)
                        {
                            item.sharkFrame++;
                        }
                    }
                }

                #endregion animation and setup
            }
        }

        #endregion player
    }
}