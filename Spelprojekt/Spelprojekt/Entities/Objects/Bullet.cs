using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spelprojekt.Graphics;
using set = Spelprojekt.GameProperties.GlobalProperties;
using Spelprojekt.Util;
using Spelprojekt.Particle;
using SpelProjekt.Worlds;
using Spelprojekt.Worlds;
using Spelprojekt.Entities.Creatures.Enemies;

namespace Spelprojekt.Entities
{
    class Bullet : Entity
    {
        public int damage;
        public Entity owner;
        public DamageType damageType;
        public bool canHit = true;
        public string texture;
        public float rotation;
        private List<Tile> collisionArea = new List<Tile>();
        private int timer;
        public enum DamageType
        {
            physical, other, ignore 
        }
        public Bullet(bool affectedByGravity, bool doesCollide, Vector2 location, string texture, Entity owner, int damage, DamageType damageType, Vector2 velocity) : base(affectedByGravity, doesCollide, Vector2.Zero, location)
        {
            size = new Vector2(ResourceCollection.textures[texture].Width, ResourceCollection.textures[texture].Height);
            hitBox = new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y);
            this.damage = damage;
            this.owner = owner;
            this.damageType = damageType;
            this.texture = texture;
            this.velocity = velocity;
        }
        public override void OtherUpdate(GameTime gameTime, Worlds.World world, Camera2D camera)
        {
            timer++;
            if (timer >= 300) isAlive = false;
            if (velocity.X < 0)
            {
                hitBox.X = (int)location.X - -(int)velocity.X;
                hitBox.Width = (int)this.size.X + -(int)velocity.X;
            }
            else
            {
                hitBox.X = (int)location.X - (int)velocity.X;
                hitBox.Width = (int)this.size.X + (int)velocity.X;
            }
            if (velocity.Y < 0)
            {
                hitBox.Y = (int)location.Y - -(int)velocity.Y;
                hitBox.Height = (int)this.size.Y + -(int)velocity.Y;
            }
            else
            {
                hitBox.Y = (int)location.Y - (int)velocity.Y;
                hitBox.Height = (int)this.size.Y + (int)velocity.Y;
            }
            rotation = -VectorUtil.GetRotation(location, location + velocity);
            collisionArea = world.FindTilesClose(this, 5 + (int)(velocity / set.TILE_SIZE).Length());
            foreach (Tile t in collisionArea)
            {
                if (t.isSolid && this.hitBox.Intersects(t.box))
                {
                    isAlive = false;
                    Game1.particleManager.AddParticle(20, () =>
                    {
                        Vector2 mod = velocity / 10;
                        return new ParticleResizing(mod - new Vector2((float)Game1.rnd.NextDouble(), (float)Game1.rnd.NextDouble()), location + new Vector2((Game1.rnd.Next(20) - 10),
                            (Game1.rnd.Next(20) - 10)),
                            MinimapColors.colors[t.tileType],
                            "frame",
                            (float)Game1.rnd.NextDouble() * 5,
                            false,
                            0.1f,
                            false);
                    });
                    break;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Camera2D camera)
        {
            spriteBatch.Draw(ResourceCollection.textures[texture], location, null, Color.White, rotation, new Vector2
                (ResourceCollection.textures[texture].Width / 2, ResourceCollection.textures[texture].Height / 2), Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(ResourceCollection.textures["frame"], hitBox, Color.Gray * 0.9f);
            foreach (Tile t in collisionArea)
            {
                if (this.hitBox.Intersects(t.box))
                {
                    spriteBatch.Draw(ResourceCollection.textures["frame"], t.box, Color.Gray * 0.3f);
                }
                else
                {
                    spriteBatch.Draw(ResourceCollection.textures["frame"], t.box, Color.Gray * 0.1f);
                }
            }
        }
        public override void OnHitEntity(Entity entity)
        {
            if (entity != owner && entity.GetType().IsSubclassOf(typeof(Creature)) && canHit)
            {
                Creature creature = (Creature)entity;
                if (damageType == DamageType.physical)
                {
                    creature.health -= (damage - creature.armor);
                }
                if (damageType == DamageType.other)
                { 
                    creature.health -= (damage - creature.resistance);
                }
                if (damageType == DamageType.ignore)
                {
                    creature.health -= damage;
                }
                canHit = false;
                isAlive = false;
            }
        }
    }
}
