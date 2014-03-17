using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt.GameProperties
{
    class GlobalProperties
    {
        public static readonly int TILE_SIZE = 64;
        public static readonly Vector2 TILE_V = new Vector2(TILE_SIZE, TILE_SIZE);
        public static readonly int WORLD_SIZE = 5000;
        public static int SCREEN_WIDTH = 1280;
        public static int SCREEN_HEIGHT = 720;
        public static bool DRAW_ALL = false;
        public static string CURSOR_TEXTURE;
    }
}
