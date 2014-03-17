using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spelprojekt.Graphics
{
    public class TextureCreator
    {
        public static GraphicsDevice device;
        public static Texture2D CreateSolidTexture(Color color, int width, int height)
        {
            Color[] buffer = new Color[width * height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    buffer[width * y + x] = color;
                }
            }
            Texture2D texture = new Texture2D(device, width, height);
            texture.SetData(buffer);
            return texture;
        }
    }
}
