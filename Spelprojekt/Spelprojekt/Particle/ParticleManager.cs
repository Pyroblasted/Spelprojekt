using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Spelprojekt.Graphics;
namespace Spelprojekt.Particle
{
    public class ParticleManager
    {
        public List<Particle> particles = new List<Particle>();
        SpriteBatch spriteBatch;

        public ParticleManager(Game game, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }
        public void Draw(GameTime gameTime)
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                spriteBatch.Draw(
                    ResourceCollection.textures[particles[i].texture], //texture
                    particles[i].position,      //position
                    null,                       //textureDestination
                    particles[i].color,         //color
                    particles[i].rotation,      //rotation
                    new Vector2(ResourceCollection.textures[particles[i].texture].Width * particles[i].scale / 2,        //textureOrigin - X
                        ResourceCollection.textures[particles[i].texture].Height * particles[i].scale / 2),               //textureOrigin - Y
                    particles[i].scale,         //float scale
                    SpriteEffects.None,         //effects
                    .85f);                        //layer
            }
        }
        public void Update(GameTime gameTime)
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                if (particles[i].alive)
                {
                    particles[i].Update(gameTime);
                }
                else
                {
                    particles.RemoveAt(i);
                }
            }
        }
        public void AddParticle(int iterations, Func<Particle> particle)
        {
            for (int i = 0; i < iterations; i++)
            {
                if(particles.Count < 35000) 
                {
                    particles.Add(particle.Invoke());
                }
            }

        }
    }
}
