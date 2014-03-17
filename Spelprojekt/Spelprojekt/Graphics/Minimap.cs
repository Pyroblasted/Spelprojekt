using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using set = Spelprojekt.GameProperties.GlobalProperties;
using Spelprojekt.Worlds;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt.Graphics
{
    public class Minimap
    {
        public Vector2 location;
        public int tilesX = (set.SCREEN_WIDTH / set.TILE_SIZE) * 4,
            tilesY = (set.SCREEN_HEIGHT / set.TILE_SIZE) * 4,
            sizeX,
            sizeY;
        MinimapTile[,] tiles;
        public Minimap(Vector2 location)
        {
            this.location = location;
            tiles = new MinimapTile[tilesX, tilesY];
            sizeX = tilesX * 4;
            sizeY = tilesY * 4;
        }
        public void Update(World world, Camera2D camera)
        {
            float cameraPosX = (int)(((camera.Position.X - camera.Origin.X) / set.TILE_SIZE) - (int)((set.SCREEN_WIDTH / set.TILE_SIZE) * 1.5));
            float cameraPosY = (int)(((camera.Position.Y - camera.Origin.Y) / set.TILE_SIZE) - (int)((set.SCREEN_HEIGHT / set.TILE_SIZE) * 1.5));
            cameraPosX /= camera.Scale;
            cameraPosY /= camera.Scale;
            int x1 = 0;
            int y1 = 0;
            int playerX = 0, playerY = 0;
            for (int x = (int)cameraPosX; x < cameraPosX + tilesX; x++)
            {
                for (int y = (int)cameraPosY; y < cameraPosY + tilesY; y++)
                {
                    if (x > 0 && y > 0 && y < set.WORLD_SIZE && x < set.WORLD_SIZE && world.tiles[x, y] != null)
                    {
                        if (world.tiles[x, y].box.Intersects(world.GetPlayer().hitBox))
                        {
                            playerX = x1;
                            playerY = y1;
                        }
                    }
                    if (x > 0 && y > 0 && y < set.WORLD_SIZE && x < set.WORLD_SIZE && world.tiles[x, y] != null)
                    {
                        tiles[x1, y1] = new MinimapTile(MinimapColors.colors[world.tiles[x, y].tileType]);
                    }
                    else
                    {
                        tiles[x1, y1] = new MinimapTile(Color.Transparent);
                    }
                    y1++;
                }
                y1 = 0;
                x1++;
            }
            tiles[playerX, playerY] = new MinimapTile(Color.Salmon);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ResourceCollection.textures["miniTile"], new Rectangle((int)location.X, (int)location.Y, sizeX, sizeY), Color.Gray * 0.5f);
            for (int x = 0; x < tilesX; x++)
            {   
                for (int y = 0; y < tilesY; y++)
                {
                    if(tiles[x, y] != null) tiles[x, y].Draw(spriteBatch, (int)location.X + x * 4, (int)location.Y + y * 4);
                }
            }
        }
    }
    public class MinimapTile
    {
        private Color color;
        public MinimapTile(Color color)
        {
            this.color = color;
        }
        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            spriteBatch.Draw(ResourceCollection.textures["miniTile"], new Vector2(x, y), color);
        }
    }
}
