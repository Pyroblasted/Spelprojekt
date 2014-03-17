using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Spelprojekt.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt.Items
{
    class ItemHoldable : Item
    {
        public Vector2 origin = new Vector2(0);
        public Vector2 offset = new Vector2(0);
        public ItemHoldable(Spritesheet sprite, string name, Point iconIndex)
            : base(sprite, name, iconIndex)
        {

        }
        public void Draw(SpriteBatch spriteBatch, int x, int y, float rotation)
        {
            Console.WriteLine(rotation);
            SpriteEffects effect;
            if (rotation >= Math.PI / 2 || rotation <= -Math.PI / 2) effect = SpriteEffects.FlipVertically;
            else effect = SpriteEffects.None;
            sprite.Draw(spriteBatch, x + (int)offset.X, y + (int)offset.Y, Color.White, rotation, origin, effect, 0f, Vector2.One);
        }
        public ItemHoldable SetOffset(Vector2 offset)
        {
            this.offset = offset;
            return this;
        }
        public ItemHoldable SetOrigin(Vector2 origin)
        {
            this.origin = origin;
            return this;
        }
        public virtual void Update()
        {

        }
    }
}
