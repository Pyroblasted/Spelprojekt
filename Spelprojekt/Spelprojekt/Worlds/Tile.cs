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
using set = Spelprojekt.GameProperties.GlobalProperties;

namespace SpelProjekt.Worlds
{
    public class Tile
    {
        public enum TileType
        {
            tile1,
            tile2,
            tile3,
            tile4,
            tile5,
            tile6,
            tile7,
            tile8,
        }

        public static Texture2D texture;


        public Vector2 pos;

        public Rectangle srcRect;

        public TileType tileType;

        public Room currentRoom;

        public Rectangle box;

        public bool isSolid
        {
            get
            {
                return (tileType != TileType.tile1);
            }
        }

        public Tile()
        {
            tileType = TileType.tile1;
        }

        public void Init(Vector2 pos)
        {
            this.pos = pos;

            box.Width = set.TILE_SIZE;
            box.Height = set.TILE_SIZE;
            box.X = (int)pos.X;
            box.Y = (int)pos.Y;
        }

        public void Update()
        {
            srcRect = new Rectangle((int)tileType * set.TILE_SIZE, 0, set.TILE_SIZE, set.TILE_SIZE);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, srcRect, Color.White);
        }
    }
}
