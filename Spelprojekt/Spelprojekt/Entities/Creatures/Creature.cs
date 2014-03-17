using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Spelprojekt.Worlds;
using Spelprojekt.Graphics;

namespace Spelprojekt.Entities
{
    public class Creature : Entity
    {
        public int health, armor, resistance, maxHealth, maxArmor, maxResistance;
        public Creature(bool affectedByGravity, bool doesCollide, Vector2 size, Vector2 location, int health, int armor, int resistance) : base(affectedByGravity, doesCollide, size, location)
        {
            this.health = health;
            this.armor = armor;
            this.resistance = resistance;
            this.maxArmor = armor;
            this.maxResistance = resistance;
            this.maxHealth = health;
        }
        public override void Update(GameTime gameTime, World world, Camera2D camera)
        {
            base.Update(gameTime, world, camera);
            if (health <= 0)
            {
                isAlive = false;
            }
        }
    }
}
