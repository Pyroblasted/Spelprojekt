using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt.Gui.Components
{
    class ProgressBar : BasicComponent
    {
        protected int _progress;
        public int progress
        {
            get
            {
                return _progress / 100;
            }
            internal set
            {
                _progress = value;
            }
        }
        public string
            barTexture,
            containerTexture;
        public int max
        {
            get;
            internal set;
        }
        public int value;
        Rectangle sourceRectangle = new Rectangle();
        public ProgressBar(string name, string barTexture, string containerTexture)
            : base(name)
        {
            this.barTexture = barTexture;
            this.containerTexture = containerTexture;
        }
        /// <summary>
        /// Sets the percentage based on a max value and the current value.
        /// </summary>
        /// <param name="max">The maximum value for the progress</param>
        /// <param name="value">The current value of the progress</param>
        public ProgressBar SetProgress(int max, int value)
        {
            this.value = value;
            this.max = max;
            return this;

        }
        public override void OtherUpdate(GameTime gameTime)
        {
            _progress = (int)((double)value / (double)max * 10000.0d);
            if (_progress > 10000) _progress = 10000;
            else if (_progress < 0) _progress = 0;
            progress = _progress;
            int width = ResourceCollection.textures[barTexture].Width;
            int height = ResourceCollection.textures[barTexture].Height;
            sourceRectangle.X = (int)location.X;
            sourceRectangle.Y = (int)location.Y;
            sourceRectangle.Width = (int)(width * (float)_progress / 10000.0f);
            sourceRectangle.Height = height;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ResourceCollection.textures[barTexture], location, sourceRectangle, Color.White);
        }
    }
}
