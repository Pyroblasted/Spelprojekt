using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Spelprojekt.Particle
{
    class ParticleResizing : Particle
    {
        public float factor;
        public bool growing;
        public float maxScale;
        bool reachedMaxSize;
        public ParticleResizing(Vector2 speed, Vector2 position, Color color, string texture, float scale, bool rotating, float factor, bool growing)
            : base(-1, speed, position, color, texture, scale, rotating)
        {
            this.factor = factor;
            this.growing = growing;
            if (growing)
            {
                maxScale = scale;
                this.scale = 0;
                reachedMaxSize = false;
            }
            else
            {
                reachedMaxSize = true;
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (growing)
            {
                if (!reachedMaxSize && scale > maxScale)
                {
                    reachedMaxSize = true;
                }
                if (!reachedMaxSize && scale < maxScale)
                {
                    scale += factor;
                }
                else if (reachedMaxSize)
                {
                    scale -= factor;
                }
            }
            else
            {
                scale -= factor;
            }
            if (speed.X > 0) speed.X -= factor;
            if (speed.Y > 0) speed.Y -= factor;
            if (scale <= 0 && reachedMaxSize)
            {
                alive = false;
            } 
        }
    }
}
