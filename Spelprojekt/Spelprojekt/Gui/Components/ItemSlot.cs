using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spelprojekt.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spelprojekt.Gui.Components
{
    class ItemSlot : BasicComponent
    {
        public static Item mouseItem;
        public string frameTexture;
        public Item item = null;
        public bool canMoveItem = true;
        public ItemSlot(string name, string frameTexture)
            : base(name)
        {
            this.frameTexture = frameTexture;
        }
        public override void OtherUpdate(GameTime gameTime)
        {
            if (item != null && item.GetType().IsSubclassOf(typeof(ItemHoldable)))
            {
                ItemHoldable h = (ItemHoldable)item;
                h.Update();
            }
        }
        public override void MouseUpdate(GameTime gameTime)
        {
            if (currentMouseState.X < location.X + width && currentMouseState.X > location.X
                && currentMouseState.Y < location.Y + height && currentMouseState.Y > location.Y
                && currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                if (ItemSlot.mouseItem == null)
                {
                    if (item != null && canMoveItem)
                    {
                        ItemSlot.mouseItem = item;
                        item = null;
                    }
                }
                else if (ItemSlot.mouseItem != null && canMoveItem)
                {
                    if (item == null)
                    {
                        item = ItemSlot.mouseItem;
                        ItemSlot.mouseItem = null;
                    }
                    else if (item != null && canMoveItem)
                    {
                        //stacking logic
                        Item temp = ItemSlot.mouseItem;
                        ItemSlot.mouseItem = item;
                        item = temp;
                    }
                }
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ResourceCollection.textures[frameTexture], location, Color.White);
            if(item != null) item.sprite.Draw(spriteBatch, location, Color.White);
        }
        public ItemSlot SetItem(Item item)
        {
            this.item = item;
            return this;
        }
    }
}
