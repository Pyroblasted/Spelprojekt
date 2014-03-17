using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Spelprojekt.Gui;

namespace Spelprojekt.enemiestest
{
    class Enemy
    {
        Vector2 pos;
        Texture2D image;
        Rectangle srcRect = new Rectangle(0,0,0,0);
        Color color = Color.White;
        float rot = 0.0f;
        Vector2 origin = new Vector2(0,0);
        float scale = 1.0f;
        SpriteEffects spriteEffects = SpriteEffects.None;
        float layerDepth = 1.0f;

        public int health = 0;
        public int damage = 0;
        public int speed = 0;

        public Enemy(Vector2 pos)
        {
            this.pos = pos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, pos, srcRect, color, rot, origin, scale, spriteEffects, layerDepth);
        }
    }
}
