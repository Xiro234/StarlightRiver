using Microsoft.Xna.Framework;
using StarlightRiver.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver
{
    public class VerletChainInstance
    {
        public bool ChainActive = true;
        public bool init = false;

        public List<RopeSegment> ropeSegments = new List<RopeSegment>();

        public bool customDistances = false;
        public int segmentDistance = 5;
        public List<float> segmentDistanceList = new List<float>();//length must match the segment count

        public int segmentCount = 10;
        public int constraintRepetitions = 50;

        //public float time = 1f;
        //public float lineWidth = 0.1f;

        private void Start(Vector2 targetPosition)
        {
            Vector2 ropeStartPoint = targetPosition;

            for (int i = 0; i < segmentCount; i++)
            {
                ropeSegments.Add(new RopeSegment(ropeStartPoint));
                ropeStartPoint.Y += (customDistances ? segmentDistanceList[i] : segmentDistance);
            }
        }


        public void UpdateChain(Vector2 targetPosition)
        {
            if (ChainActive)
            {
                if (init == false)
                {
                    Start(targetPosition); //run once
                    init = true;
                }

                //Main.NewText("updated" + targetPosition);
                //DrawRope();
                Simulate(targetPosition);
            }
            else if (!ChainActive && init == true)//if in-active and initalized 
            {
                //Reset here
            }
        }

        private void Simulate(Vector2 targetPosition)
        {
            // SIMULATION
            Vector2 forceGravity = new Vector2(0f, 1f); //GRAVITY

            for (int i = 1; i < segmentCount; i++)
            {
                RopeSegment firstSegment = ropeSegments[i];
                Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
                firstSegment.posOld = firstSegment.posNow;
                firstSegment.posNow += velocity;
                firstSegment.posNow += forceGravity; //firstSegment.posNow += forceGravity * time;
                this.ropeSegments[i] = firstSegment;

            }

            //CONSTRAINTS
            for (int i = 0; i < constraintRepetitions; i++)//the amount of times Constraints are applied per update
            {
                ApplyConstraint(targetPosition);
            }
        }

        private void ApplyConstraint(Vector2 targetPosition)
        {
            RopeSegment firstSegment = ropeSegments[0];
            firstSegment.posNow = targetPosition;
            ropeSegments[0] = firstSegment;

            for (int i = 0; i < segmentCount - 1; i++)
            {
                RopeSegment firstSeg = ropeSegments[i];
                RopeSegment secondSeg = ropeSegments[i + 1];

                float segmentDist = (customDistances ? segmentDistanceList[i] : segmentDistance);

                //Vector2 distVect = (firstSeg.posNow - secondSeg.posNow);
                //float dist = (float)Math.Sqrt(distVect.X * (distVect.X + distVect.Y) * distVect.Y);

                float dist = (firstSeg.posNow - secondSeg.posNow).Length();
                float error = Math.Abs(dist - segmentDist);
                Vector2 changeDir = Vector2.Zero;

                if (dist > segmentDist)
                {
                    changeDir = Vector2.Normalize(firstSeg.posNow - secondSeg.posNow);
                }
                else if (dist < segmentDist)
                {
                    changeDir = Vector2.Normalize(secondSeg.posNow - firstSeg.posNow);
                }

                Vector2 changeAmount = changeDir * error;
                if (i != 0)
                {
                    firstSeg.posNow -= changeAmount * 0.5f;
                    ropeSegments[i] = firstSeg;
                    secondSeg.posNow += changeAmount * 0.5f;
                    ropeSegments[i + 1] = secondSeg;
                }
                else
                {
                    secondSeg.posNow += changeAmount;
                    ropeSegments[i + 1] = secondSeg;
                }
            }
        }

        public void DrawRope(SpriteBatch spritebatch, Action<SpriteBatch, Vector2> drawMethod)
        {
            //Vector2[] ropePositions = new Vector2[segmentCount];

            for (int i = 0; i < segmentCount; i++)
            {
                //ropePositions[i] = this.ropeSegments[i].posNow; //original did this for an unknown reason

                drawMethod(spritebatch, ropeSegments[i].posNow);
            }
        }

        public struct RopeSegment
        {
            public Vector2 posNow;
            public Vector2 posOld;

            public RopeSegment(Vector2 pos)
            {
                this.posNow = pos;
                this.posOld = pos;
            }
        }
    }
}