using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Spelprojekt.Util
{
    class VectorUtil
    {
        public static float GetRotation(Vector2 pos1, Vector2 pos2)
        {
            return (float)-Math.Atan2(pos2.Y - pos1.Y, pos2.X - pos1.X);
        }
        public static Vector2 GetStraightPath(Vector2 pos1, Vector2 pos2, float speed)
        {
            Vector2 vec = new Vector2();
            float atan = (float)Math.Atan2(pos2.Y - pos1.Y, pos2.X - pos1.X);
            vec.X = speed * (float)Math.Cos(atan);
            vec.Y = speed * (float)Math.Sin(atan);
            return vec;
        }
    }
}
