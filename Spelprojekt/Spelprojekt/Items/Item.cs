using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Spelprojekt.Graphics;
using Microsoft.Xna.Framework;

namespace Spelprojekt.Items
{
    class Item
    {
        public Spritesheet sprite;
        public Point iconIndex;
        public string name;
        public Item(Spritesheet sprite, string name, Point iconIndex)
        {
            this.name = name;
            this.iconIndex = iconIndex;
            this.sprite = sprite;
            sprite.textureIndex = iconIndex;
        }
        public virtual void UseItem(MouseState currentState, MouseState previousState, int mx, int my)
        {

        }
        public virtual void LeftClick(MouseState currentState, MouseState previousState, int mx, int my)
        {

        }
        public virtual void OnSelected()
        {

        }
    }
}
