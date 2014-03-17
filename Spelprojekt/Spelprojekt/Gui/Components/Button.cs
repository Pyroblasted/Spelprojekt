using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt.Gui.Components
{
    class Button : BasicComponent
    {
        #region Delegates
        public delegate void OnHover(Button sender);
        public delegate void OnClick(Button sender);
        public delegate void OnLeave(Button sender);
        #endregion
        #region Events
        public event OnHover OnHoverEvent;
        public event OnClick OnClickEvent;
        public event OnLeave OnLeaveEvent;
        #endregion
        public string texture, text, font;
        public Color textColor, buttonColor = Color.White;
        /// <summary>
        /// Padding of the text when drawing it.
        /// </summary>
        public Vector2 padding = new Vector2(0, 0);
        /// <summary>
        /// Creates a new Button.
        /// </summary>
        /// <param name="name">Unique identifier used to find the control in the ControlManager</param>
        /// <param name="texture">Name of the texture in the ResourceCollection</param>
        /// <param name="font"></param>
        public Button(string name, string texture, string font)
            : base(name)
        {
            this.texture = texture;
            this.font = font;
            text = "";
        }
        /// <summary>
        /// Creates a new Button. Contains additional parameter for the text.
        /// </summary>
        /// <param name="name">Unique identifier used to find the control in the ControlManager</param>
        /// <param name="texture">Name of the texture in the ResourceCollection</param>
        /// <param name="font"></param>
        public Button(string name, string texture, string font, string text)
            : base(name)
        {
            this.texture = texture;
            this.font = font;
            this.text = text;
        }
        public Button SetTextColor(Color color)
        {
            this.textColor = color;
            return this;
        }
        public Button SetButtonColor(Color color)
        {
            this.buttonColor = color;
            return this;
        }
        public Button SetTextPadding(Vector2 padding)
        {
            this.padding = padding;
            return this;
        }
        public override void KeyboardUpdate(GameTime gametime)
        {
            if (selected && currentKeyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
            {
                if (OnClickEvent != null)
                {
                    OnClickEvent.Invoke(this);
                }
            }
        }
        public override void MouseUpdate(GameTime gameTime)
        {
            if (currentMouseState.LeftButton == ButtonState.Released
                && previousMouseState.LeftButton == ButtonState.Pressed
                && CurrentHovering())
            {
                if (OnClickEvent != null)
                {
                    OnClickEvent.Invoke(this);
                }
            }
            if (!CurrentHovering() && PreviousHovering())
            {
                if (OnLeaveEvent != null)
                {
                    OnLeaveEvent.Invoke(this);
                }
            }
            if (CurrentHovering())
            {
                if (OnHoverEvent != null)
                {
                    OnHoverEvent.Invoke(this);
                }
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ResourceCollection.textures[texture], location, buttonColor);
            spriteBatch.DrawString(ResourceCollection.fonts[font], text, location + padding, textColor);
        }
    }
}
