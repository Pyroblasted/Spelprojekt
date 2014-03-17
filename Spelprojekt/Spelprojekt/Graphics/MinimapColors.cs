using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spelprojekt.Worlds;
using Microsoft.Xna.Framework;
using SpelProjekt.Worlds;

namespace Spelprojekt.Graphics
{
    class MinimapColors
    {
        public static Dictionary<Tile.TileType, Color> colors = new Dictionary<Tile.TileType, Color>();
        public static void SetColors()
        {
            colors.Add(Tile.TileType.tile1, Color.Transparent);
            colors.Add(Tile.TileType.tile2, Color.Green);
            colors.Add(Tile.TileType.tile3, Color.Green);
            colors.Add(Tile.TileType.tile4, Color.Green);
            colors.Add(Tile.TileType.tile5, Color.Green);
            colors.Add(Tile.TileType.tile6, Color.Green);
            colors.Add(Tile.TileType.tile7, Color.Green);
            colors.Add(Tile.TileType.tile8, Color.Brown);
        }
    }
}
