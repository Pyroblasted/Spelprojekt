using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using set = Spelprojekt.GameProperties.GlobalProperties;
using Microsoft.Xna.Framework.Graphics;
using SpelProjekt.Worlds;
using Microsoft.Xna.Framework.Input;
using Spelprojekt.Entities;
using Spelprojekt.Graphics;
using Spelprojekt.States;

namespace Spelprojekt.Worlds
{
    public class World
    {
        public Vector2 lastPos;
        public int lastSizeX;
        public int lastSizeY;
        public static Random rnd = new Random();

        int roomSizeX = 60;
        int roomSizeY = 25;
        private List<Room> rooms = new List<Room>();
        public Tile[,] tiles = new Tile[set.WORLD_SIZE, set.WORLD_SIZE];
        

        public List<Entity> entities = new List<Entity>();
        public string name
        {
            get;
            internal set;
        }
        public World(string name)
        {
            this.name = name;
            GenerateLevel();
        }
        public void SetTile(int x, int y, Tile.TileType tileType)
        {
            if (tiles[x, y] != null)
            {
                tiles[x, y].tileType = tileType;
                tiles[x, y].Update();
            }
        }
        public void SetTile(int x, int y, Tile.TileType tileType, Room room)
        {
            if (tiles[(int)room.pos.X + x, (int)room.pos.Y + y] != null)
            {
                tiles[(int)room.pos.X + x, (int)room.pos.Y + y].tileType = tileType;
                tiles[(int)room.pos.X + x, (int)room.pos.Y + y].Update();
            }
        }
        public Tile.TileType GetTileType(int x, int y)
        {
            if (x > 0 && y > 0 && y < set.WORLD_SIZE && x < set.WORLD_SIZE && tiles[x, y] != null)
            {
                return tiles[x, y].tileType;
            }
            else return Tile.TileType.tile1;
        }
        public Tile GetTile(int x, int y)
        {
            if (x > 0 && y > 0 && y < set.WORLD_SIZE && x < set.WORLD_SIZE && tiles[x, y] != null)
            {
                return tiles[x, y];
            }
            else return null;
        }
        public Tile.TileType GetTileType(int x, int y, Room room)
        {
            int x1 = (int)room.pos.X;
            int y1 = (int)room.pos.Y;
            if (x1 + x > 0 && y1 + y > 0 && y1 + y < set.WORLD_SIZE && x1 + x < set.WORLD_SIZE && tiles[x1 + x, y1 + y] != null)
            {
                return tiles[x1 + x, y1 + y].tileType;
            }
            else return Tile.TileType.tile1;
        }
        public Tile GetTile(int x, int y, Room room)
        {
            int x1 = (int)room.pos.X;
            int y1 = (int)room.pos.Y;
            if (x1 + x > 0 && y1 + y > 0 && y1 + y < set.WORLD_SIZE && x1 + x < set.WORLD_SIZE && tiles[x1 + x, y1 + y] != null)
            {
                return tiles[x1 + x, y1 + y];
            }
            else return null;
        }
        public void SpawnEntity(Vector2 velocity, Vector2 position, Entity entity)
        {
            entity.location = position;
            entity.velocity = velocity;
            entities.Add(entity);
        }
        public void SpawnEntity(Entity entity)
        {
            entities.Add(entity);
        }
        public void SpawnEntity(Func<Entity> function, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                entities.Add(function.Invoke());
            }
        }
        public void Update(GameTime gameTime, Camera2D camera)
        {
            //for (int x = 0; x < set.WORLD_SIZE; x++)
            //{
            //    for (int y = 0; y < set.WORLD_SIZE; y++)
            //    {
            //        if (tiles[x, y] != null) tiles[x, y].Update(gameTime);
            //    }
            //}
            //if (GetCurrentRoomOnMouse() != null) Console.WriteLine(GetCurrentRoomOnMouse().GetTile(new Point(Mouse.GetState().X, Mouse.GetState().Y)).ToString());
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity c = entities[i];
                foreach (Entity e in FindEntitiesClose(c, 256))
                {
                    if (e != c && e.hitBox.Intersects(c.hitBox))
                    {
                        e.OnHitEntity(c);
                    }
                }
                if (c.isAlive)
                {
                    c.Update(gameTime, this, camera);
                }
                else
                {
                    entities[i].OnRemoval(this);
                    entities.RemoveAt(i);
                }

            }
            //if (tiles[Mouse.GetState().X / set.TILE_SIZE, Mouse.GetState().Y / set.TILE_SIZE] != null) Console.WriteLine(tiles[Mouse.GetState().X / set.TILE_SIZE, Mouse.GetState().Y / set.TILE_SIZE].ToString());
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Camera2D camera)
        {
            int cameraPosX = (int)(camera.Position.X - camera.Origin.X) / set.TILE_SIZE;
            int cameraPosY = (int)(camera.Position.Y - camera.Origin.Y) / set.TILE_SIZE;
            int cameraWidth = (set.SCREEN_WIDTH / set.TILE_SIZE) * 2;
            int cameraHeight = (set.SCREEN_HEIGHT / set.TILE_SIZE) * 2;
            for (int x = cameraPosX; x < cameraPosX + cameraWidth / camera.Scale; x++)
            {
                for (int y = cameraPosY; y < cameraPosY + cameraHeight / camera.Scale; y++)
                {
                    if(x >= 0 && y >= 0 && y < set.WORLD_SIZE && x < set.WORLD_SIZE && tiles[x, y] != null) tiles[x, y].Draw(spriteBatch);
                }
            }
            foreach (Room r in rooms)
            {
                r.Draw(spriteBatch);
            }
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity c = entities[i];
                c.Draw(spriteBatch, gameTime, camera);
            }

        }
        //public bool IsTileSolid(Point point)
        //{
        //    if (tiles[point.X / set.TILE_SIZE][point.Y / set.TILE_SIZE] == null)
        //    {
        //        return false;
        //    }
        //    else if(tiles[point.X / set.TILE_SIZE][point.Y / set.TILE_SIZE].isSolid)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        public bool IsTileSolid(Point point)
        {
            int x = point.X / set.TILE_SIZE;
            int y = point.Y / set.TILE_SIZE;
            return (bool)(GetTile(x, y) != null && GetTile(x, y).isSolid);
        }
        public Player GetPlayer()
        {
            foreach (Entity c in entities)
            {
                if (c.GetType() == typeof(Player))
                {
                    return (Player)c;
                }
            }
            return null;
        }
        public void AddRoom(int sizeX, int sizeY, Vector2 pos)
        {
            Random rnd = new Random();
            int random = 0;
            Room room = new Room();

            random = rnd.Next(0, 5);

            if (random == 0)
            {
                room = SerializationHelper.Load<Room>("room1");
            }
            else if (random == 1)
            {
                room = SerializationHelper.Load<Room>("room2");
            }
            else if (random == 2)
            {
                room = SerializationHelper.Load<Room>("room3");
            }
            else if (random == 3)
            {
                room = SerializationHelper.Load<Room>("room4");
            }
            else if (random == 4)
            {
                room = SerializationHelper.Load<Room>("room4");
            }

            room.FinalInit(pos, this);
            rooms.Add(room);
            int posX = (int)room.pos.X;
            int posY = (int)room.pos.Y;
            if ((posX + room.sizeX) > set.WORLD_SIZE || posY + room.sizeY > set.WORLD_SIZE || posX < 0 || posY < 0) return;
            int x1 = 0;
            int y1 = 0;
            for (int x = posX; x < posX + room.sizeX; x++)
            {
                for (int y = posY; y < posY + room.sizeY; y++)
                {
                    tiles[x, y] = room.tiles[x1][y1];
                    y1++;
                }
                y1 = 0;
                x1++;
            }
            
        }
        public Room GetRoom(Vector2 roomPos, Room currentRoom)
        {
            Room iRoom = new Room();

            for (int i = 0; i < rooms.Count; i++)
            {
                //Console.WriteLine(rPos);

                if ((roomPos.X * set.TILE_SIZE) > (rooms[i].pos.X * set.TILE_SIZE) && (roomPos.X * set.TILE_SIZE) < ((rooms[i].pos.X * set.TILE_SIZE) + (rooms[i].sizeX * set.TILE_SIZE)))
                {
                    if ((roomPos.Y * set.TILE_SIZE) > (rooms[i].pos.Y * set.TILE_SIZE) && (roomPos.Y * set.TILE_SIZE) < ((rooms[i].pos.Y * set.TILE_SIZE) + (rooms[i].sizeY * set.TILE_SIZE)))
                    {
                        iRoom = rooms[i];
                        return iRoom;
                    }
                }
            }

            return null;
        }
        public void GenerateLevel()
        {
            Console.WriteLine("Generation started.");
            for (int i = 0; i < 20; i++)
            {
                if (rooms.Count == 0)
                {
                    AddRoom(roomSizeX, roomSizeY, new Vector2(20000 / set.TILE_SIZE, 20000 / set.TILE_SIZE));
                    lastSizeX = rooms[rooms.Count - 1].sizeX;
                    lastSizeY = rooms[rooms.Count - 1].sizeY;
                    lastPos = rooms[rooms.Count - 1].pos + new Vector2(lastSizeX, 0);
                }
                else
                {

                    int r = rnd.Next(0, 4);

                    lastSizeX = rooms[rooms.Count - 1].sizeX;
                    lastSizeY = rooms[rooms.Count - 1].sizeY;
                    Room iRoom;

                    bool b = false;
                    int j = 0;

                    while (b == false)
                    {
                        if (r == 0)
                        {
                            iRoom = GetRoom(rooms[rooms.Count - 1].pos + new Vector2(lastSizeX + 8, 8), rooms[rooms.Count - 1]);

                            if (iRoom == null)
                            {
                                lastPos = rooms[rooms.Count - 1].pos + new Vector2(lastSizeX, 0);
                                b = true;
                            }
                            else
                            {
                                j++;
                                r = rnd.Next(0, 4);
                            }
                        }
                        else if (r == 1)
                        {
                            iRoom = GetRoom(rooms[rooms.Count - 1].pos - new Vector2(lastSizeX - 8, -8), rooms[rooms.Count - 1]);

                            if (iRoom == null)
                            {
                                lastPos = rooms[rooms.Count - 1].pos - new Vector2(lastSizeX, 0);
                                b = true;
                            }
                            else
                            {
                                j++;
                                r = rnd.Next(0, 4);
                            }
                        }
                        else if (r == 2)
                        {
                            iRoom = GetRoom(rooms[rooms.Count - 1].pos + new Vector2(8, lastSizeY + 8), rooms[rooms.Count - 1]);

                            if (iRoom == null)
                            {
                                lastPos = rooms[rooms.Count - 1].pos + new Vector2(0, lastSizeY);
                                b = true;
                            }
                            else
                            {
                                j++;
                                r = rnd.Next(0, 4);
                            }
                        }
                        else if (r == 3)
                        {
                            iRoom = GetRoom(rooms[rooms.Count - 1].pos - new Vector2(-8, lastSizeY - 8), rooms[rooms.Count - 1]);

                            if (iRoom == null)
                            {
                                lastPos = rooms[rooms.Count - 1].pos - new Vector2(0, lastSizeY);
                                b = true;
                            }
                            else
                            {
                                j++;
                                r = rnd.Next(0, 4);
                            }
                        }

                        if (j >= 4)
                        {
                            b = true;
                        }
                    }

                    AddRoom(roomSizeX, roomSizeY, lastPos);
                }
            }
            foreach (Room r in rooms)
            {
                r.CreateDoors(this);
            }
        }
        public List<Entity> FindEntitiesClose(Entity other, float maxDistance)
        {
            List<Entity> entityList = new List<Entity>();
            foreach(Entity e in entities)
            {
                float distance = 0;
                Vector2.Distance(ref e.location, ref other.location, out distance);
                if (distance <= maxDistance)
                {
                    entityList.Add(e);
                }
            }
            return entityList;
        }
        public List<Tile> FindTilesClose(Entity e, int distance)
        {
            int tx = (int)e.location.X / set.TILE_SIZE;
            int ty = (int)e.location.Y / set.TILE_SIZE;
            List<Tile> tileList = new List<Tile>();
            for (int x = tx - distance / 2; x < tx + distance / 2; x++)
            {
                for (int y = ty - distance / 2; y < ty + distance / 2; y++)
                {
                    if (x >= 0 && y >= 0 && y < set.WORLD_SIZE && x < set.WORLD_SIZE && tiles[x, y] != null) tileList.Add(tiles[x, y]);
                }
            }
            return tileList;
        }
    }
}