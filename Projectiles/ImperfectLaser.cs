using System.Math;
using Terraria;
using Terraria.ModLoader;
using Terraria.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.Projectiles
{
	public class ImperfectLaser : ModProjectile
	{
		public const ushort LaserFocusDist = 80;
		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = true;
			projectile.magic = true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			projectile.position.x = player.Center.x + (40 * player.direction); // 40 should be replaced with the width of the weapon texture
			projectile.timeLeft = 2;
		}
		
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Player player = Main.player[projectile.owner];
			Vector2 laserPos2 = new Vector2(projectile.position.x + player.direction * LaserFocusDist, projectile.position.y);
			
			return TriangleCollision(player.direction * LaserFocusDist, (float)(Math.PI / 6), projectile.position, targetHitbox, projectile.rotation)
				| TriangleCollision(player.direction * -720, (float)(Math.PI / 6), laserPos2, targetHitbox, projectile.rotation);
		}
		
		// using a short here because it is more than enough for what we're doing, but is smaller than an int
		// theta would be the angle adjacent to the focal point of the laser, in radians.
		// pos is the upper-leftmost position of the triangle hitbox
		// making w negative will make the hitbox point left, and make pos the upper-rightmost position
		
		public bool? TriangleCollision(short w, float theta, Vector2 pos, Rectangle hitbox, float rotation)
		{
			float h = Math.Abs(w) * Math.Tan(theta/2);
			Vector2 vertex1 = RotatePoint(new Vector2(pos.x + w, pos.y + h), pos, rotation);
			Vector2 vertex2 = RotatePoint(new Vector2(pos.x, pos.y + 2*h), pos, rotation);
			
			Vector2 centroid = GetCentroid(h, vertex1, vertex2);
			
			Vector2 hitboxCenter = new Vector2((float)(hitbox.y + hitbox.height / 2), (float)(hitbox.x + hitbox.width / 2));
			Vector2 intersectionPoint = GetIntersectionPoint(centroid, hitboxCenter, pos, vertex1);
			
			float angleToHitbox = Math.Atan2(hitbox.x - hitboxCenter.x, hitbox.y - hitboxCenter.y);
			
			float inHitbox = Math.Min((float)((hitbox.width/2)/Math.Sin(angleToHitbox)), (float)((hitbox.height/2)/Math.Cos(angleToHitbox)));
			
			if (GetDistance(centroid, intersectionPoint) <= GetDistance(centroid, hitboxCenter) - inHitbox)
			{
				return true;
			}
			
			return false;
		}
		
		public Vector2 GetIntersectionPoint(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
		{
			float A1 = p2.y - p1.y;
			float B1 = p2.x - p1.x;
			float C1 = A1*p1.x + B1*p1.y;
			
			float A2 = q2.y - q1.y;
			float B2 = q2.x - q1.x;
			float C2 = A2*q1.x + B2*q1.y;
			
			float delta = A1 * B2 - A2 * B1;

			float x = (B2 * C1 - B1 * C2) / delta;
			float y = (A1 * C2 - A2 * C1) / delta;
			return new Vector2(x, y);
		}
		
		public Vector2 GetCentroid(Vector2 p1, Vector2 p2, Vector2 p3)
		{
			return new Vector2((p1.x + p2.x + p3.x) / 3f, (p1.y + p2.y + p3.y) / 3f);
		}
		
		public float GetDistance(Vector2 p1, Vector2 p2)
		{
			return (float)(Math.Sqrt(Math.pow(p2.x-p1.x, 2) + Math.pow(p2.y-p1.y, 2)));
		}
		
		public Vector2 RotatePoint(Vector2 p1, Vector2 p2, float theta)
		{
			float s = Math.Sin(theta);
			float c = Math.Cos(theta);
			
			p1.x -= p2.x;
			p1.y -= p2.y;
			
			float xn = p1.x * c - p1.y * s; // goes counterclockwise
			float yn = p1.x * s + p1.y * c;
			
			return new Vector2(xn + p2.x, yn + p2.y);
		}
	}
}
