using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spelprojekt.Graphics
{
    public class Animation
    {
        public enum EndBehavior
        {
            repeating, pauseOnEnd, pauseReturnToStart
        }

        public delegate void OnAnimationFinished(Animation sender, int frames);
        public event OnAnimationFinished OnFinishedEvent;



        public bool animate;
        public Rectangle target;
        int progress;
        public int frame, frames;
        public Texture2D spritesheet;
        public int delay;
        public int textureWidth { get; internal set; }
        public int textureHeight { get; internal set; }
        public EndBehavior endBehavior = EndBehavior.pauseOnEnd;
        public Animation(Texture2D spritesheet, int textureWidth, int textureHeight, int delay, int frames)
        {
            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;
            this.spritesheet = spritesheet;
            target = new Rectangle(0, 0, textureWidth, textureHeight);
            this.delay = delay;
            this.frames = frames;
            frame = 0;
        }
        public Animation(Texture2D spritesheet, int textureWidth, int textureHeight, int delay, int frames, EndBehavior endBehavior)
        {
            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;
            this.spritesheet = spritesheet;
            target = new Rectangle(0, 0, textureWidth, textureHeight);
            this.delay = delay;
            this.frames = frames;
            this.endBehavior = endBehavior;
            frame = 0;
        }
        public void Pause()
        {
            animate = false;
        }
        public void Start()
        {
            animate = true;
        }
        public void Stop()
        {
            {
                SetFrame(0);
                animate = false;
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(spritesheet, position, target, color);
        }
        public void SetFrame(int frame)
        {
            this.frame = frame;
        }
        public void Update(GameTime gameTime)
        {
            progress++;
            if (frame >= frames)
            {
                if (OnFinishedEvent != null)
                {
                    OnFinishedEvent.Invoke(this, frame);
                }
                if (endBehavior == EndBehavior.repeating) frame = 0;
                else if (endBehavior == EndBehavior.pauseOnEnd) Pause();
                else if (endBehavior == EndBehavior.pauseReturnToStart) Stop();
            }
            if (progress > delay && animate)
            {
                frame++;
                progress = 0;
            }
            target.X = frame % (spritesheet.Width / textureWidth) * textureWidth;
            target.Y = frame / (spritesheet.Height / textureHeight) * textureHeight;
        }
    }
}


