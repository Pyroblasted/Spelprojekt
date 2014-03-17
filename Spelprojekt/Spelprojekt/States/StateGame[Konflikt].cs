using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spelprojekt.Gui.Components;
using Microsoft.Xna.Framework;
using Spelprojekt.Entities;
using Spelprojekt.Worlds;
using Spelprojekt.GameProperties;
using Microsoft.Xna.Framework.Graphics;
using Spelprojekt.Graphics;
using Microsoft.Xna.Framework.Input;
using Spelprojekt.Gui;

namespace Spelprojekt.States
{
    class StateGame : State
    {
        public World world;
        public EntityPlayer player;
        public Camera2D camera;
        public Minimap minimap;
        public StateGame(Game1 game, string name)
            : base(game, name)
        {

        }
        public override void EnterState()
        {
            foreach (BasicComponent bc in Game1.guiManager.GetMany(
                "lifeBar", 
                "stamBar"
                ))
            {
                bc.visible = true;
            }
        }
        public override void LeaveState(string state)
        {
            if (state != "pauseState")
            {
                foreach (BasicComponent bc in Game1.guiManager.GetMany(
                    "lifeBar",
                    "stamBar"
                    ))
                {
                    bc.visible = false;
                }
            }
        }
        public override void FirstEnter()
        {
            minimap = new Minimap(new Vector2(400, 400));
            minimap.location = new Vector2(GlobalProperties.SCREEN_WIDTH - minimap.sizeX - 25, 25);
            camera = new Camera2D(game);
            camera.Initialize();
            world = new World("world1");
            player = new EntityPlayer(new Vector2(GlobalProperties.SCREEN_WIDTH / 2 + 200, GlobalProperties.SCREEN_HEIGHT / 2 + 400), new Vector2(45, 64), "player");
            world.entities.Add(player);
            camera.Focus = player;

            Game1.guiManager.Add(new ProgressBar("lifeBar", "testBar", "")
                .SetLocation<ProgressBar>(new Vector2(10, 10))
                .SetSize<ProgressBar>(100, 10)
                .SetVisible<ProgressBar>(true)
                .SetProgress(player.maxHealth, player.health));
            Game1.guiManager.Add(new ProgressBar("stamBar", "testBar1", "")
                .SetLocation<ProgressBar>(new Vector2(10, 25))
                .SetSize<ProgressBar>(100, 10)
                .SetVisible<ProgressBar>(true)
                .SetProgress(player.maxStamina, player.stamina));
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.Transform);
            world.Draw(spriteBatch, gameTime, camera);
            spriteBatch.End();
            // TODO: Draw objects with a fixed location on screen here.
            spriteBatch.Begin();
            spriteBatch.DrawString(ResourceCollection.fonts["testFont"], gameTime.ElapsedGameTime.Milliseconds.ToString(), new Vector2(200, 200), Color.White);
            minimap.Draw(spriteBatch);
            if(ItemSlot.mouseItem != null) spriteBatch.Draw(ResourceCollection.textures[ItemSlot.mouseItem.icon], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White * 0.8f);
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
            world.Update(gameTime);
            minimap.Update(world, camera);
        }
    }
}
