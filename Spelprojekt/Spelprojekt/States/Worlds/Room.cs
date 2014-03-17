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
using Spelprojekt.Worlds;
using set = Spelprojekt.GameProperties.GlobalProperties;
using System.Text;
using Spelprojekt;

namespace SpelProjekt.Worlds
{
    public class Room
    {
        public Tile[][] tiles;
        public Tile[][] tilesCopy;

        public static Random rnd = new Random();

        public int
            sizeX,
            sizeY;

        public Vector2 pos;
        public Rectangle boundingBox;

        public Room()
        {
        }

        public void Init(int sizeX, int sizeY, Vector2 pos)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            this.pos.X = pos.X;
            this.pos.Y = pos.Y;

            boundingBox = new Rectangle((int)pos.X, (int)pos.Y, sizeX * set.TILE_SIZE, sizeY * set.TILE_SIZE);

            tiles = new Tile[sizeX][];
        }


        public void CreateDoors(World world)
        {
            /*
            // Doors
            */

            /*
             * LEFT AND RIGHT
            */
            for (int x = 1; x < sizeX - 1; x++)
            {
                for (int y = 1; y < sizeY - 1; y++)
                {
                    Room iRoom;

                    if (tiles[x][y + 1].tileType >= Tile.TileType.tile2 && tiles[x][y + 1].tileType < Tile.TileType.tile8)
                    {
                        // Right
                        iRoom = world.GetRoom(new Vector2((pos.X + sizeX) + 1, (pos.Y + sizeY) - 1), this);

                        if (iRoom != null)
                        {
                            if (tiles[x + 1][y].tileType >= Tile.TileType.tile2)
                            {
                                world.SetTile(x + 1, y, Tile.TileType.tile1);
                                world.SetTile(x + 1, y - 1, Tile.TileType.tile1);
                                world.SetTile(x + 1, y - 2, Tile.TileType.tile1);
                                world.SetTile(x + 1, y - 3, Tile.TileType.tile1);
                                world.SetTile(x + 1, y + 1, Tile.TileType.tile3);
                            }
                        }

                        // Left
                        iRoom = world.GetRoom(new Vector2((pos.X) - sizeX / 2, (pos.Y + sizeY) - 2), this);

                        if (iRoom != null)
                        {
                            int i = 0;

                            while (x - i > 0)
                            {
                                i++;

                                if (tiles[x - i][y].tileType >= Tile.TileType.tile2)
                                {
                                    world.SetTile(x + 1, y, Tile.TileType.tile1);
                                    world.SetTile(x + 1, y - 1, Tile.TileType.tile1);
                                    world.SetTile(x + 1, y - 2, Tile.TileType.tile1);
                                    world.SetTile(x + 1, y - 3, Tile.TileType.tile1);
                                    world.SetTile(x + 1, y + 1, Tile.TileType.tile3);
                                }
                            }
                        }
                    }

                }
            }

            /*
             * UP & DOWN
            */
            {
                int rndX = rnd.Next(12, sizeX - 12);

                for (int x = (sizeX / 2) - 5; x < (sizeX / 2) + 5; x++)
                {
                    for (int y = 1; y < sizeY - 1; y++)
                    {
                        // Down
                        if (tiles[x][y + 1].tileType >= Tile.TileType.tile2 && tiles[x][y + 1].tileType <= Tile.TileType.tile8)
                        {
                            Point iPoint = new Point(x, y);

                            Room iRoom = world.GetRoom(new Vector2((pos.X) + 1, (pos.Y + sizeY) + 1), this);

                            if (iRoom != null)
                            {
                                if (tiles[x][y + 1].tileType >= Tile.TileType.tile2)
                                {
                                    world.SetTile(x, y + 1, Tile.TileType.tile1);
                                }
                            }
                        }

                        // Up
                        if (tiles[x][y - 1].tileType >= Tile.TileType.tile2 && tiles[x][y - 1].tileType <= Tile.TileType.tile8)
                        {
                            Point iPoint = new Point(x, y);

                            Room iRoom = world.GetRoom(new Vector2((pos.X) + 1, (pos.Y) - 1), this);

                            if (iRoom != null)
                            {
                                if (tiles[x][y - 1].tileType >= Tile.TileType.tile2)
                                {
                                    world.SetTile(x, y + 1, Tile.TileType.tile1);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    tiles[x] = new Tile[sizeY];
                }
            }

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    tiles[x][y] = new Tile();
                    tiles[x][y].Init(new Vector2((pos.X * set.TILE_SIZE) + x * set.TILE_SIZE, (pos.Y * set.TILE_SIZE) + y * set.TILE_SIZE));
                    tiles[x][y].Update();
                }
            }
        }

        public void FinalInit(Vector2 pos, World world)
        {
            this.pos.X = pos.X;
            this.pos.Y = pos.Y;

            tilesCopy = tiles;

            tiles = new Tile[sizeX][];

            FinalClear(world);
            //Console.WriteLine("Room Initialized");
        }

        public void FinalClear(World world)
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    tiles[x] = new Tile[sizeY];
                }
            }

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    tiles[x][y] = tilesCopy[x][y];
                    tiles[x][y].Init(new Vector2((pos.X * set.TILE_SIZE) + x * set.TILE_SIZE, (pos.Y * set.TILE_SIZE) + y * set.TILE_SIZE));
                    tiles[x][y].Update();
                }
            }
        }

        public bool IsTileSolid(Point point)
        {
            if (tiles[(point.X - (int)pos.X) / set.TILE_SIZE][(point.Y - (int)pos.Y) / set.TILE_SIZE].tileType != Tile.TileType.tile3)
            {
                Console.WriteLine((Mouse.GetState().X - (int)pos.X) + " | " + (Mouse.GetState().Y - (int)pos.Y));
                return true;
            }
            else
            {
                Console.WriteLine((Mouse.GetState().X - (int)pos.X) + " | " + (Mouse.GetState().Y - (int)pos.Y));
                return false; 
            }
        }
        public Tile GetTile(Point point)
        {
            return tiles[(point.X - (int)pos.X) / set.TILE_SIZE][(point.Y - (int)pos.Y) / set.TILE_SIZE];
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    //if (!set.DRAW_ALL)
                    //{
                    //    if (tiles[x][y].ScreenCollide())
                    //    {
                    //        tiles[x][y].Draw(spriteBatch);
                    //    }
                    //}
                    //else
                    //{
                        tiles[x][y].Draw(spriteBatch);
                    //}
                }
            }

            //spriteBatch.Draw(Game1.pixel, new Rectangle(box.X, box.Y, box.Width, box.Height), Color.DarkGreen * 0.6f);
        }

    }
}
