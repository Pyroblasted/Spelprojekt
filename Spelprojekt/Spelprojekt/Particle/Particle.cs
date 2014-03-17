using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Spelprojekt.Particle
{
    public class Particle
    {
        public bool alive = true;
        public Vector2 speed;
        public Vector2 position;
        public Color color;
        public float scale;
        public string texture;
        public int lifeTime;
        public bool infinite;
        public bool rotating;
        public float rotation;
        #region Constructors
        public Particle(int lifeTime, Vector2 speed, Vector2 position, Color color)
        {
            alive = true;
            this.lifeTime = lifeTime;
            this.speed = speed;
            this.position = position;
            this.color = color;
            scale = 1f;
            texture = "basicParticle";
            rotating = false;
            if (lifeTime == -1)
            {
                infinite = true;
            } 
        }
        public Particle(int lifeTime, Vector2 speed, Vector2 position, Color color, string texture)
        {
            alive = true;
            this.lifeTime = lifeTime;
            this.speed = speed;
            this.position = position;
            this.color = color;
            scale = 1f;
            this.texture = texture;
            rotating = false;
            if (lifeTime == -1)
            {
                infinite = true;
            } 
        }
        public Particle(int lifeTime, Vector2 speed, Vector2 position, Color color, string texture, float scale)
        {
            alive = true;
            this.lifeTime = lifeTime;
            this.speed = speed;
            this.position = position;
            this.color = color;
            this.scale = scale;
            this.texture = texture;
            rotating = false;
            if (lifeTime == -1)
            {
                infinite = true;
            } 
        }
        public Particle(int lifeTime, Vector2 speed, Vector2 position, Color color, string texture, float scale, bool rotating)
        {
            alive = true;
            this.lifeTime = lifeTime;
            this.speed = speed;
            this.position = position;
            this.color = color;
            this.scale = scale;
            this.texture = texture;
            this.rotating = rotating;
            if (lifeTime == -1)
            {
                infinite = true;
            }
        }
        #endregion
        public virtual void Update(GameTime gameTime)
        {
            position += speed;
            if (rotating) rotation += 0.05f;
            if (!infinite)
            {
                if (lifeTime < 1)
                {
                    alive = false;
                }
                lifeTime--;
            }
        }
    }
}
