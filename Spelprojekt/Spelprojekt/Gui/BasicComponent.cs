using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spelprojekt.Gui
{
    public class BasicComponent
    {
        public delegate void OnSelected();
        public delegate void OnDeselected();
        public event OnSelected OnSelectedEvent;
        public event OnDeselected OnDeselectedEvent;

        protected MouseState currentMouseState, previousMouseState;
        protected KeyboardState currentKeyState, previousKeyState;
        public string name
        {
            get; 
            internal set;
        }
        public int width, height;
        public Vector2 location = new Vector2();
        public float scale = 1f;
        public bool visible = true;
        public bool selected;
        public BasicComponent(string name)
        {
            this.name = name;
        }
        public virtual T SetLocation<T>(Vector2 location) where T : BasicComponent
        {
            this.location = location;
            return (T)this;
        }
        public virtual T SetSize<T>(int width, int height) where T : BasicComponent
        {
            this.width = width;
            this.height = height;
            return (T)this;
        }
        public virtual T SetVisible<T>(bool visible) where T : BasicComponent
        {
            this.visible = visible;
            return (T)this;
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
        public void Update(GameTime gameTime) 
        {
            OtherUpdate(gameTime);
            currentMouseState = Mouse.GetState();
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (currentMouseState.X >= location.X && currentMouseState.X <= location.X + width
                    && currentMouseState.Y >= location.Y && currentMouseState.Y <= location.Y + height)
                {
                    selected = true;
                }
                else
                {
                    selected = false;
                }
            }
            MouseUpdate(gameTime);
            previousMouseState = Mouse.GetState();
            currentKeyState = Keyboard.GetState();
            KeyboardUpdate(gameTime);
            previousKeyState = Keyboard.GetState();
        }
        public virtual void MouseUpdate(GameTime gameTime)
        {

        }
        public virtual void KeyboardUpdate(GameTime gametime)
        {

        }
        public virtual void OtherUpdate(GameTime gameTime)
        {

        }
        public bool CurrentHovering()
        {
            return currentMouseState.X >= location.X && currentMouseState.X <= location.X + width
                && currentMouseState.Y >= location.Y && currentMouseState.Y <= location.Y + height;
        }
        public bool PreviousHovering()
        {
            return previousMouseState.X >= location.X && previousMouseState.X <= location.X + width
                && previousMouseState.Y >= location.Y && previousMouseState.Y <= location.Y + height;
        }

    }
}
