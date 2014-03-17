using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spelprojekt.Gui.Components
{
    class ItemSlotCollection : BasicComponent
    {
        public enum SlotOrder
        {
            vertical, horizontal
        }
        public ItemSlot[] slotCollection;
        public SlotOrder slotOrder;
        public int lineBreak;
        public int selectedSlot;
        private int lastScroll;
        public bool useScroll = true;
        public ItemSlotCollection(string name, SlotOrder slotOrder, int lineBreak, params ItemSlot[] slots) 
            : base(name)
        {
            this.slotOrder = slotOrder;
            this.lineBreak = lineBreak;
            slotCollection = slots;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for(int i = 0; i < slotCollection.Length; i++)
            {
                slotCollection[i].Draw(gameTime, spriteBatch);
            }
            for (int i = 0; i < slotCollection.Length; i++)
            {
                if (i == selectedSlot)
                {
                    ItemSlot s = slotCollection[i];
                    int x = (int)s.location.X;
                    int y = (int)s.location.Y;
                    spriteBatch.Draw(ResourceCollection.textures["frame"], new Rectangle(x, y, 1, s.height), Color.Black);
                    spriteBatch.Draw(ResourceCollection.textures["frame"], new Rectangle(x, y + s.height, s.width, 1), Color.Black);
                    spriteBatch.Draw(ResourceCollection.textures["frame"], new Rectangle(x + s.width, y, 1, s.height), Color.Black);
                    spriteBatch.Draw(ResourceCollection.textures["frame"], new Rectangle(x, y, s.width, 1), Color.Black);
                }
            }
        }
        public override void OtherUpdate(GameTime gameTime)
        {
            for (int i = slotCollection.Length - 1; i >= 0; i--)
            {
                slotCollection[i].Update(gameTime);
            }
            if (slotOrder == SlotOrder.vertical)
            {
                int index = 0;
                for (int x = 0; x < lineBreak; x++ )
                {
                    for (int y = 0; y < slotCollection.Length / lineBreak; y++)
                    {
                        slotCollection[index].SetLocation<ItemSlot>(new Vector2(location.Y + x * slotCollection[index].width, location.Y + y * slotCollection[index].height));
                        index++;
                    }
                    //index++;
                }
            }
            else if (slotOrder == SlotOrder.horizontal)
            {
                int index = 0;
                for (int x = 0; x < slotCollection.Length / lineBreak; x++)
                {
                    for (int y = 0; y < lineBreak; y++)
                    {
                        slotCollection[index].SetLocation<ItemSlot>(new Vector2(location.Y + x * slotCollection[index].width, location.Y + y * slotCollection[index].height));
                        index++;
                    }
                    //index++;
                }
            }
        }
        public override void MouseUpdate(GameTime gameTime)
        {
            if (useScroll)
            {
                if (currentMouseState.ScrollWheelValue > lastScroll)
                {
                    selectedSlot++;
                    if (selectedSlot >= slotCollection.Length) selectedSlot = 0;
                } if (currentMouseState.ScrollWheelValue < lastScroll)
                {
                    selectedSlot--;
                    if (selectedSlot < 0) selectedSlot = slotCollection.Length - 1;
                }
                lastScroll = currentMouseState.ScrollWheelValue;
            }
        }
        public override T SetVisible<T>(bool visible)
        {
            foreach (ItemSlot i in slotCollection)
            {
                i.visible = visible;
            }
            return base.SetVisible<T>(visible);
        }
        public ItemSlotCollection SetCanMove(bool canMove)
        {
            foreach (ItemSlot i in slotCollection)
            {
                i.canMoveItem = canMove;
            }
            return this;
        }
    }
}
