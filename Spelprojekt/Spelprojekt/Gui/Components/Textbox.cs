using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spelprojekt.Graphics;

namespace Spelprojekt.Gui.Components
{
    class Textbox : BasicComponent
    {
        static Texture2D caretTexture;
        Color caretColor, textColor;
        string font;
        public string 
            text = "",
            passwordKey = "*";
        int currentIndex, startRenderIndex, maxIndex;
        double timer;
        bool 
            secondChar = false,
            passwordBox = false;
        /// <summary>
        /// Creates a new TextBox.
        /// </summary>
        /// <param name="name">Unique identifier used to find the control in the ControlManager</param>
        /// <param name="font">The name of the font in the ResourceCollection</param>
        /// <param name="caretColor">Color of the caret</param>
        /// <param name="textColor">Color of the text</param>
        public Textbox(string name, string font, Color caretColor, Color textColor, int width)
            : base(name)
        {
            maxIndex = width / (int)ResourceCollection.fonts[font].MeasureString("_").X;
            this.height = (int)ResourceCollection.fonts[font].MeasureString("|").Y;
            this.width = width;
            this.caretColor = caretColor;
            this.textColor = textColor;
            this.font = font;
            caretTexture = TextureCreator.CreateSolidTexture(Color.White, 1, (int)ResourceCollection.fonts[font].MeasureString("|").Y);
        }
        /// <summary>
        /// Creates a new TextBox. Contains an additional parameter to specify if the TextBox should be a password box.
        /// </summary>
        /// <param name="name">Unique identifier used to find the control in the ControlManager</param>
        /// <param name="font">The name of the font in the ResourceCollection</param>
        /// <param name="caretColor">Color of the caret</param>
        /// <param name="textColor">Color of the text</param>
        /// <param name="passwordBox">If true the TextBox will act as a password box instead</param>
        public Textbox(string name, string font, Color caretColor, Color textColor, int width, bool passwordBox)
            : base(name)
        {
            maxIndex = width / (int)ResourceCollection.fonts[font].MeasureString("_").X;
            this.height = (int)ResourceCollection.fonts[font].MeasureString("|").Y;
            this.caretColor = caretColor;
            this.textColor = textColor;
            this.font = font;
            caretTexture = TextureCreator.CreateSolidTexture(Color.White, 1, (int)ResourceCollection.fonts[font].MeasureString("|").Y);
            this.passwordBox = passwordBox;
            this.width = width;
        }
        /// <summary>
        /// Creates a new TextBox. Contains an additional parameter to specify if the TextBox should be a password box.
        /// </summary>
        /// <param name="name">Unique identifier used to find the control in the ControlManager</param>
        /// <param name="font">The name of the font in the ResourceCollection</param>
        /// <param name="caretColor">Color of the caret</param>
        /// <param name="textColor">Color of the text</param>
        /// <param name="passwordBox">If true the TextBox will act as a password box instead</param>
        /// <param name="passwordKey">The character that will be rendered instead of the text entered in a password box</param>
        public Textbox(string name, string font, Color caretColor, Color textColor, int width, bool passwordBox, string passwordKey)
            : base(name)
        {
            maxIndex = width / (int)ResourceCollection.fonts[font].MeasureString("_").X;
            this.height = (int)ResourceCollection.fonts[font].MeasureString("|").Y;
            this.caretColor = caretColor;
            this.textColor = textColor;
            this.font = font;
            caretTexture = TextureCreator.CreateSolidTexture(Color.White, 1, (int)ResourceCollection.fonts[font].MeasureString("|").Y);
            this.passwordBox = passwordBox;
            this.passwordKey = passwordKey;
            this.width = width;
        }
        public override void KeyboardUpdate(GameTime gameTime)
        {
            Keys[] pressedKeys = currentKeyState.GetPressedKeys();
            Keys[] lastKeys = previousKeyState.GetPressedKeys();
            if (selected)
            {
                foreach (Keys k in pressedKeys)
                {
                    Console.WriteLine(k.ToString());
                    if (lastKeys.Contains(k))
                    {
                        if (gameTime.TotalGameTime.TotalMilliseconds - timer > (secondChar ? 30 : 400))
                        {
                            AddKey(k, pressedKeys);
                            timer = gameTime.TotalGameTime.TotalMilliseconds;
                            secondChar = true;
                        }
                    }
                    else
                    {
                        AddKey(k, pressedKeys);
                        timer = gameTime.TotalGameTime.TotalMilliseconds;
                        secondChar = false;
                    }
                }
            }
            //if (currentIndex - startRenderIndex > maxIndex)
            //{
            //    startRenderIndex++;
            //}
            if (currentIndex - startRenderIndex > 0)
            {
                if (ResourceCollection.fonts[font].MeasureString(text.Substring(startRenderIndex, currentIndex - startRenderIndex)).X > width)
                {
                    startRenderIndex++;
                }
            }
            if (currentIndex - startRenderIndex < 0)
            {
                startRenderIndex--;
            }
        }
        private int GetEndRenderIndex(bool password)
        {
            SpriteFont sfont = ResourceCollection.fonts[font];
            int indexTotal = 0;
            int widthTotal = 0;
            for (int i = startRenderIndex; i < text.Length; i++)
            {
                if(!password)widthTotal += (int)sfont.MeasureString(text[i].ToString()).X;
                else widthTotal += (int)sfont.MeasureString(passwordKey).X;
                if (widthTotal >= width + 1)
                {
                    indexTotal++;
                    break;
                }
                else indexTotal++;
            }
            return indexTotal;
        }
        private void AddKey(Keys k, Keys[] pressedKeys)
        {
            #region Add Characters
            if (k.ToString().Length == 1)
            {
                AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                    || pressedKeys.Contains(Keys.RightShift)
                    || System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock)
                    ? k.ToString()
                    : k.ToString().ToLower());
                currentIndex++;
            }
            if (k == Keys.OemPeriod)
            {
                AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                    || pressedKeys.Contains(Keys.RightShift)
                    ? ":"
                    : ".");
                currentIndex++;
            }
            if (k == Keys.OemMinus)
            {
                AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                    || pressedKeys.Contains(Keys.RightShift)
                    ? "_"
                    : "-");
                currentIndex++;
            }
            if (k == Keys.OemComma)
            {
                AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                    || pressedKeys.Contains(Keys.RightShift)
                    ? ";"
                    : ",");
                currentIndex++;
            }
            if (k == Keys.OemTilde)
            {
                AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                    || pressedKeys.Contains(Keys.RightShift)
                    ? "Ö"
                    : "ö");
                currentIndex++;
            }
            if (k == Keys.OemQuotes)
            {
                AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                    || pressedKeys.Contains(Keys.RightShift)
                    ? "Ä"
                    : "ä");
                currentIndex++;
            }
            if (k == Keys.OemCloseBrackets)
            {
                AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                    || pressedKeys.Contains(Keys.RightShift)
                    ? "Å"
                    : "å");
                currentIndex++;
            }
            if (k == Keys.OemQuestion)
            {
                AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                    || pressedKeys.Contains(Keys.RightShift)
                    ? "*"
                    : "'");
                currentIndex++;
            }
            if (k == Keys.OemSemicolon)
            {
                if (pressedKeys.Contains(Keys.LeftAlt) || pressedKeys.Contains(Keys.RightAlt))
                {
                    AddTextAt(currentIndex, "~");
                }
                else
                {
                    AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                        || pressedKeys.Contains(Keys.RightShift)
                        ? "^"
                        : "¨");
                }
                currentIndex++;
            }
            if (k == Keys.OemBackslash)
            {
                if (pressedKeys.Contains(Keys.LeftAlt) || pressedKeys.Contains(Keys.RightAlt))
                {
                    AddTextAt(currentIndex, "|");
                }
                else
                {
                    AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                        || pressedKeys.Contains(Keys.RightShift)
                        ? ">"
                        : "<");
                }
                currentIndex++;
            }
            if (k == Keys.Space)
            {
                AddTextAt(currentIndex, " ");
                currentIndex++;
            }
            if (k == Keys.Back && currentIndex > 0)
            {
                ChangeTextAt(currentIndex - 1, "");
                currentIndex--;
            }
            if (k == Keys.Delete && currentIndex < text.Length)
            {
                ChangeTextAt(currentIndex, "");
            }
            if (k == Keys.Left && currentIndex > 0)
            {
                currentIndex--;
            }
            if (k == Keys.Right && currentIndex < text.Length)
            {
                currentIndex++;
            }
            #region numbers
            if (k == Keys.D1)
            {
                AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                    || pressedKeys.Contains(Keys.RightShift)
                    ? "!"
                    : "1");
                currentIndex++;
            }
            if (k == Keys.D2)
            {
                if (pressedKeys.Contains(Keys.LeftAlt) || pressedKeys.Contains(Keys.RightAlt))
                {
                    AddTextAt(currentIndex, "@");
                }
                else
                {
                    AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                        || pressedKeys.Contains(Keys.RightShift)
                        ? "\""
                        : "2");
                }
                currentIndex++;
            }
            if (k == Keys.D3)
            {
                if (pressedKeys.Contains(Keys.LeftAlt) || pressedKeys.Contains(Keys.RightAlt))
                {
                    AddTextAt(currentIndex, "£");
                }
                else
                {
                    AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                        || pressedKeys.Contains(Keys.RightShift)
                        ? "#"
                        : "3");
                }
                currentIndex++;
            }
            if (k == Keys.D4)
            {
                if (pressedKeys.Contains(Keys.LeftAlt) || pressedKeys.Contains(Keys.RightAlt))
                {
                    AddTextAt(currentIndex, "$");
                }
                else
                {
                    AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                        || pressedKeys.Contains(Keys.RightShift)
                        ? "¤"
                        : "4");
                }
                currentIndex++;
            }
            if (k == Keys.D5)
            {
                AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                    || pressedKeys.Contains(Keys.RightShift)
                    ? "%"
                    : "5");
                currentIndex++;
            }
            if (k == Keys.D6)
            {
                AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                    || pressedKeys.Contains(Keys.RightShift)
                    ? "&"
                    : "6");
                currentIndex++;
            }
            if (k == Keys.D7)
            {
                if (pressedKeys.Contains(Keys.LeftAlt) || pressedKeys.Contains(Keys.RightAlt))
                {
                    AddTextAt(currentIndex, "{");
                }
                else
                {
                    AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                        || pressedKeys.Contains(Keys.RightShift)
                        ? "/"
                        : "7");
                }
                currentIndex++;
            }
            if (k == Keys.D8)
            {
                if (pressedKeys.Contains(Keys.LeftAlt) || pressedKeys.Contains(Keys.RightAlt))
                {
                    AddTextAt(currentIndex, "[");
                }
                else
                {
                    AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                        || pressedKeys.Contains(Keys.RightShift)
                        ? "("
                        : "8");
                }
                currentIndex++;
            }
            if (k == Keys.D9)
            {
                if (pressedKeys.Contains(Keys.LeftAlt) || pressedKeys.Contains(Keys.RightAlt))
                {
                    AddTextAt(currentIndex, "]");
                }
                else
                {
                    AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                        || pressedKeys.Contains(Keys.RightShift)
                        ? ")"
                        : "9");
                }
                currentIndex++;
            }
            if (k == Keys.D0)
            {
                if (pressedKeys.Contains(Keys.LeftAlt) || pressedKeys.Contains(Keys.RightAlt))
                {
                    AddTextAt(currentIndex, "}");
                }
                else
                {
                    AddTextAt(currentIndex, pressedKeys.Contains(Keys.LeftShift)
                        || pressedKeys.Contains(Keys.RightShift)
                        ? "="
                        : "0");
                }
                currentIndex++;
            }
            #endregion
            #endregion
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!passwordBox)
            {
                spriteBatch.DrawString(ResourceCollection.fonts[font], text.Substring(startRenderIndex, GetEndRenderIndex(false)), new Vector2(location.X, location.Y), textColor);
                if(selected) spriteBatch.Draw(caretTexture, new Vector2(location.X 
                    + GetEndRenderIndex(false) - (text.Length - currentIndex) > 0 ? ResourceCollection.fonts[font].MeasureString(text.Substring(startRenderIndex, currentIndex - startRenderIndex)).X : 0
                    , location.Y), caretColor);

            }
            else
            {
                string passBoxText = "";
                for (int i = 0; i < text.Length; i++)
                {
                    passBoxText += passwordKey;
                }
                spriteBatch.DrawString(ResourceCollection.fonts[font], passBoxText.Substring(startRenderIndex, GetEndRenderIndex(true)), new Vector2(location.X, location.Y), textColor);
                if (selected) spriteBatch.Draw(caretTexture, new Vector2(location.X
                     + GetEndRenderIndex(true) - (text.Length - currentIndex) > 0 ? ResourceCollection.fonts[font].MeasureString(passBoxText.Substring(startRenderIndex, currentIndex - startRenderIndex)).X : 0
                     , location.Y), caretColor);
            }
        }
        private bool CurrentHovering()
        {
            return currentMouseState.X >= location.X && currentMouseState.X <= location.X + width
                && currentMouseState.Y >= location.Y && currentMouseState.Y <= location.Y + height;
        }
        private bool PreviousHovering()
        {
            return previousMouseState.X >= location.X && previousMouseState.X <= location.X + width
                && previousMouseState.Y >= location.Y && previousMouseState.Y <= location.Y + height;
        }
        private void ChangeTextAt(int index, string change)
        {
            List<char> textList = text.ToArray().ToList();
            if (change.Length > 1) return;
            if (change == "")
            {
                textList.RemoveAt(index);
            }
            else
            {
                textList[index] = change[0];
            }
            text = new string(textList.ToArray());
        }
        private void AddTextAt(int index, string change)
        {
            List<char> textList = text.ToArray().ToList();
            if (change.Length > 1) return;
            if (change == "")
            {
                textList.RemoveAt(index);
            }
            else
            {
                textList.Insert(index, change[0]);
            }
            text = new string(textList.ToArray());
        }
    }
}
    