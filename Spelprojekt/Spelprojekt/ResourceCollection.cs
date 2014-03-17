using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Spelprojekt.Graphics;
using Microsoft.Xna.Framework;
using set = Spelprojekt.GameProperties.GlobalProperties;

namespace Spelprojekt
{
    public class ResourceCollection
    {
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        /// <summary>
        /// Temporary loading of game resources.
        /// </summary>
        /// <param name="content">ContentManager</param>
        public static void LoadResources(ContentManager content)
        {
            fonts.Add("testFont", content.Load<SpriteFont>("testFont"));
            textures.Add("testBar", TextureCreator.CreateSolidTexture(Color.Green, 100, 10));
            textures.Add("testBar1", TextureCreator.CreateSolidTexture(Color.Yellow, 100, 10));


            textures.Add("tileSetForest", content.Load<Texture2D>("World/Tilesets/TilesetForest"));
            textures.Add("dirt", TextureCreator.CreateSolidTexture(Color.Brown, 32, 32));
            textures.Add("redstuff", TextureCreator.CreateSolidTexture(Color.Yellow, 32, 32));
            textures.Add("tile", TextureCreator.CreateSolidTexture(Color.White, set.TILE_SIZE, set.TILE_SIZE));
            textures.Add("player", content.Load<Texture2D>("Entities/Player/Player"));
            textures.Add("arm", content.Load<Texture2D>("Entities/Player/Arm"));
            textures.Add("miniTile", TextureCreator.CreateSolidTexture(Color.White, 4, 4));
            textures.Add("viewport", TextureCreator.CreateSolidTexture(Color.Red, 1000, 700));
            textures.Add("buttonTemp", TextureCreator.CreateSolidTexture(Color.Gray, 100, 30));
            textures.Add("buttonTemp1", TextureCreator.CreateSolidTexture(Color.LightGray, 100, 30));
            textures.Add("frame", TextureCreator.CreateSolidTexture(Color.White, 1, 1));
            textures.Add("itemframe", TextureCreator.CreateSolidTexture(Color.White, 64, 64));
            textures.Add("cursorCrosshair", content.Load<Texture2D>("UI/Cursors/CursorCrosshair"));
            textures.Add("cursor", content.Load<Texture2D>("UI/Cursors/Cursor"));
            textures.Add("bullet", content.Load<Texture2D>("Entities/Bullets/BulletTexture"));
        }
    }
}
