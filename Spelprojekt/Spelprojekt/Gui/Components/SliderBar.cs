using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt.Gui.Components
{
    class SliderBar : ProgressBar
    {
        bool canChange;
        int tabIndex = 1;
        public SliderBar(string name, string barTexture, string containerTexture)
            : base(name, barTexture, containerTexture)
        {

        }
        public override void MouseUpdate(GameTime gameTime)
        {
            width = (int)(ResourceCollection.textures[this.barTexture].Width * this.scale);
            height = (int)(ResourceCollection.textures[this.barTexture].Height * this.scale);
            float mousePos = Mouse.GetState().X - (int)location.X;
            Console.WriteLine(width + " | " + height + " | " + progress);
            if (previousMouseState != null)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed
                    && Mouse.GetState().X > this.location.X
                    && Mouse.GetState().X < this.location.X + width
                    && Mouse.GetState().Y > this.location.Y
                    && Mouse.GetState().Y < this.location.Y + height
                    && previousMouseState.LeftButton == ButtonState.Released)
                {
                    canChange = true;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    canChange = false;
                }
            }
            if (canChange) this.SetProgress((int)width, (int)mousePos);
        }
        public override void KeyboardUpdate(GameTime gametime)
        {
            if (selected)
            {
                if (currentKeyState.IsKeyDown(Keys.Left))
                {
                    if (value > 0) value -= tabIndex;
                }
                if (currentKeyState.IsKeyDown(Keys.Right))
                {
                    if (value < max) value += tabIndex;
                }
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
