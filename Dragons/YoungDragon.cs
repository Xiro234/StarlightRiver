using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Dragons
{
    class YoungDragon : ModMountData
    {
        public override void SetDefaults()
        {
            mountData.spawnDust = DustID.Grass;
            mountData.buff = mod.BuffType("CarMount");
            mountData.heightBoost = 20;
            mountData.fallDamage = 0.1f;
            mountData.runSpeed = 4f;
            mountData.dashSpeed = 8f;
            mountData.flightTimeMax = 600;
            mountData.fatigueMax = 0;
            mountData.jumpHeight = 5;
            mountData.acceleration = 0.1f;
            mountData.jumpSpeed = 4f;
            mountData.blockExtraJumps = false;
            mountData.totalFrames = 4;
            mountData.constantJump = true;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 40;
            }
            mountData.playerYOffsets = array;
            mountData.xOffset = 13;
            mountData.bodyFrame = 3;
            mountData.yOffset = -12;
            mountData.playerHeadOffset = 22;
            mountData.standingFrameCount = 4;
            mountData.standingFrameDelay = 12;
            mountData.standingFrameStart = 0;
            mountData.runningFrameCount = 4;
            mountData.runningFrameDelay = 12;
            mountData.runningFrameStart = 0;
            mountData.flyingFrameCount = 0;
            mountData.flyingFrameDelay = 0;
            mountData.flyingFrameStart = 0;
            mountData.inAirFrameCount = 1;
            mountData.inAirFrameDelay = 12;
            mountData.inAirFrameStart = 0;
            mountData.idleFrameCount = 4;
            mountData.idleFrameDelay = 12;
            mountData.idleFrameStart = 0;
            mountData.idleFrameLoop = true;
            mountData.swimFrameCount = mountData.inAirFrameCount;
            mountData.swimFrameDelay = mountData.inAirFrameDelay;
            mountData.swimFrameStart = mountData.inAirFrameStart;
            if (Main.netMode != 2)
            {
                mountData.textureWidth = mountData.backTexture.Width + 20;
                mountData.textureHeight = mountData.backTexture.Height;
            }
        }
        public override void UpdateEffects(Player player)
        {
            SetDefaults();
            player.noItems = true;
            if (player.controlUseItem)
            {
                Dust.NewDustPerfect(player.Center + new Vector2(player.direction * -14, 8), ModContent.DustType<Dusts.Piss>(), new Vector2(player.direction * 2, 0), 180, new Color(255, 255, 150));
            }
        }
        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
            texture = ModContent.GetTexture("StarlightRiver/Invisible");
            DragonData data = drawPlayer.GetModPlayer<DragonHandler>().data;
            int offX = drawPlayer.direction == -1 ? 25 : -25;
            Rectangle target = new Rectangle((int)drawPosition.X + offX, (int)drawPosition.Y, 132, 92);
            Rectangle source = ModContent.GetTexture("StarlightRiver/Dragons/DragonScale").Frame();
            SpriteEffects flip = drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            playerDrawData.Add(new DrawData(ModContent.GetTexture("StarlightRiver/Dragons/DragonScale"), target, source, data.scaleColor.MultiplyRGB(drawColor), 0, source.Size() / 2, flip, 0));
            playerDrawData.Add(new DrawData(ModContent.GetTexture("StarlightRiver/Dragons/DragonBelly"), target, source, data.bellyColor.MultiplyRGB(drawColor), 0, source.Size() / 2, flip, 0));
            playerDrawData.Add(new DrawData(ModContent.GetTexture("StarlightRiver/Dragons/DragonHorn"), target, source, data.hornColor.MultiplyRGB(drawColor), 0, source.Size() / 2, flip, 0));
            playerDrawData.Add(new DrawData(ModContent.GetTexture("StarlightRiver/Dragons/DragonEye"), target, source, data.eyeColor.MultiplyRGB(drawColor), 0, source.Size() / 2, flip, 0));
            return true;
        }
    }
    public class CarMount : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("YoungDragon");
            Description.SetDefault("Wheeeeee");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<YoungDragon>(), player);
            player.buffTime[buffIndex] = 11;
        }
    }
}
