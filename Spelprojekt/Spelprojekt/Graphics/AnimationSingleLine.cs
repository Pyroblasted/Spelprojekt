using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spelprojekt.Graphics
{
    public class AnimationSingleLine
    {

        public delegate void OnAnimationFinished(AnimationSingleLine sender, int frame, int line);
        public event OnAnimationFinished OnFinishedEvent;

        private Rectangle source;
        private int _frame;
        private string texture;
        public int delay;
        private int timer = 0;
        private bool animate;
        public int Frame
        {
            get
            {
                return _frame;
            }
            set
            {
                if (value <= Texture.Width / source.Width && value >= 0)
                {
                    _frame = value;
                }
                else
                {
                    if (OnFinishedEvent != null)
                    {
                        OnFinishedEvent.Invoke(this, Frame, Line);
                    }
                }
            }
        }
        public Texture2D Texture
        {
            get
            {
                return ResourceCollection.textures[texture];
            }
        }
        public int Line
        {
            get
            {
                return source.Y / source.Height;
            }
            set
            {
                if (value <= Texture.Height / source.Height && value >= 0)
                {
                    source.Y = value * source.Height;
                }
            }
        }

        public AnimationSingleLine(int tw, int th, int line, string texture, int delay)
        {
            this.texture = texture;
            source.X = 0;
            source.Y = line * th;
            source.Width = tw;
            source.Height = th;
            Line = line;
        }
        public void Update()
        {
            if (timer >= delay && animate)
            {
                timer = 0;
                Frame++;
            }
            timer++;
            source.X = Frame % (Texture.Width / source.Width) * source.Width;

        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            spriteBatch.Draw(Texture, location, source, color);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effect, float layerDepth)
        {
            spriteBatch.Draw(Texture, location, source, color, rotation, origin, scale, effect, layerDepth);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle location, Color color)
        {
            spriteBatch.Draw(Texture, location, source, color);
        }
        public void Stop()
        {
            animate = false;
            Frame = 0;
        }
        public void Start()
        {
            animate = true;
        }
        public void Pause()
        {
            animate = false;
        }
    }
}
