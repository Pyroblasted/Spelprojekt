using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spelprojekt.Graphics
{
    class Spritesheet
    {
        private int tw, th;
        private string texture;
        public Texture2D Texture
        {
            get
            {
                return ResourceCollection.textures[texture];
            }
        }
        public Point textureIndex = new Point(0, 0);
        public Spritesheet(string texture, int tw, int th)
        {
            this.th = th;
            this.tw = tw;
            this.texture = texture;
        }
        public void Draw(SpriteBatch spriteBatch, int x, int y, Color color)
        {
            spriteBatch.Draw(Texture, new Vector2(x, y), new Rectangle(textureIndex.X * tw, textureIndex.Y * th, tw, th), color);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            spriteBatch.Draw(Texture, location, new Rectangle(textureIndex.X * tw, textureIndex.Y * th, tw, th), color);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle location, Color color)
        {
            spriteBatch.Draw(Texture, location, new Rectangle(textureIndex.X * tw, textureIndex.Y * th, tw, th), color);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle location, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(Texture, location, new Rectangle(textureIndex.X * tw, textureIndex.Y * th, tw, th), color, rotation, origin, effects, layerDepth);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth, Vector2 scale)
        {
            spriteBatch.Draw(Texture, location, new Rectangle(textureIndex.X * tw, textureIndex.Y * th, tw, th), color, rotation, origin, scale, effects, layerDepth);
        }
        public void Draw(SpriteBatch spriteBatch, int x, int y, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth, Vector2 scale)
        {
            spriteBatch.Draw(Texture, new Vector2(x, y), new Rectangle(textureIndex.X * tw, textureIndex.Y * th, tw, th), color, rotation, origin, scale, effects, layerDepth);
        }
    }
}
