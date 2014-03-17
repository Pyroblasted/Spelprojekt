using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt.Graphics
{
    public class TextureInfo
    {
        public static Color GetPixelData(Texture2D texture, int x, int y)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(buffer);
            int accessPoint = texture.Width * (x + y);
            if (accessPoint < buffer.Length && accessPoint > 0)
            {
                return buffer[accessPoint];
            }
            else
            {
                return Color.Transparent;
            }
        }
    }
}
