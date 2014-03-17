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
using Spelprojekt.Items;
using Spelprojekt.Entities.Creatures.Enemies;

namespace Spelprojekt.States
{
    class StateGame : State
    {
        public World world;
        public Player player;
        public Camera2D camera;
        public Minimap minimap;
        public StateGame(Game1 game, string name)
            : base(game, name)
        {

        }
        public override void EnterState()
        {
            GlobalProperties.CURSOR_TEXTURE = "cursorCrosshair";
            foreach (BasicComponent bc in Game1.guiManager.GetMany(
                "lifeBar", 
                "stamBar",
                "hotbar"
                ))
            {
                bc.visible = true;
            }
        }
        public override void LeaveState(string state)
        {
            foreach (BasicComponent bc in Game1.guiManager.GetMany(
                "lifeBar",
                "stamBar",
                "hotbar"
                ))
            {
                bc.visible = false;
            }
        }
        public override void FirstEnter()
        {
            minimap = new Minimap(new Vector2(400, 400));
            minimap.location = new Vector2(GlobalProperties.SCREEN_WIDTH - minimap.sizeX - 25, 25);
            camera = new Camera2D(game);
            world = new World("world1");
            player = new Player(new Vector2(GlobalProperties.SCREEN_WIDTH / 2 + 20200, GlobalProperties.SCREEN_HEIGHT / 2 + 20400), new Vector2(45, 64), "player");
            world.entities.Add(player);
            camera.Initialize(player);

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
            Game1.guiManager.Add(new ItemSlotCollection("hotbar", ItemSlotCollection.SlotOrder.horizontal, 1,
                new ItemSlot("hotSlot1", "itemframe").SetSize<ItemSlot>(64, 64).SetVisible<ItemSlot>(true).SetItem(new ItemWeapon(new Spritesheet("arm", 64, 64), "test", new Point(0, 0), 10, Bullet.DamageType.physical, 5).SetOffset(new Vector2(33, 29)).SetOrigin(new Vector2(18, 31))),
                new ItemSlot("hotSlot2", "itemframe").SetSize<ItemSlot>(64, 64).SetVisible<ItemSlot>(true).SetItem(new ItemWeapon(new Spritesheet("arm", 64, 64), "test", new Point(0, 1), 50, Bullet.DamageType.physical, 100).SetOffset(new Vector2(33, 29)).SetOrigin(new Vector2(18, 31))),
                new ItemSlot("hotSlot3", "itemframe").SetSize<ItemSlot>(64, 64).SetVisible<ItemSlot>(true),
                new ItemSlot("hotSlot4", "itemframe").SetSize<ItemSlot>(64, 64).SetVisible<ItemSlot>(true),
                new ItemSlot("hotSlot5", "itemframe").SetSize<ItemSlot>(64, 64).SetVisible<ItemSlot>(true).SetItem(new ItemWeapon(new Spritesheet("arm", 64, 64), "test", new Point(0, 2), 20, Bullet.DamageType.physical, 40).SetOffset(new Vector2(33, 29)).SetOrigin(new Vector2(18, 31))),
                new ItemSlot("hotSlot6", "itemframe").SetSize<ItemSlot>(64, 64).SetVisible<ItemSlot>(true),
                new ItemSlot("hotSlot7", "itemframe").SetSize<ItemSlot>(64, 64).SetVisible<ItemSlot>(true),
                new ItemSlot("hotSlot8", "itemframe").SetSize<ItemSlot>(64, 64).SetVisible<ItemSlot>(true).SetItem(new ItemWeapon(new Spritesheet("arm", 64, 64), "test", new Point(0, 3), 30, Bullet.DamageType.physical, 65).SetOffset(new Vector2(33, 29)).SetOrigin(new Vector2(18, 31))),
                new ItemSlot("hotSlot9", "itemframe").SetSize<ItemSlot>(64, 64).SetVisible<ItemSlot>(true)).SetLocation<ItemSlotCollection>(new Vector2(50, 720 - 70)).SetCanMove(false));
            //Ken är bäst
            world.entities.Add(new Zombie(true, true, new Vector2(64), player.location + new Vector2(80, 0), 100, 0, 0, "itemframe"));
            world.entities.Add(new Zombie(true, true, new Vector2(64), player.location + new Vector2(160, 0), 100, 0, 0, "itemframe"));
            world.entities.Add(new Zombie(true, true, new Vector2(64), player.location + new Vector2(-40, 0), 100, 0, 0, "itemframe"));
            world.entities.Add(new Zombie(true, true, new Vector2(64), player.location + new Vector2(-120, 0), 100, 0, 0, "itemframe"));
            world.entities.Add(new Zombie(true, true, new Vector2(64), player.location + new Vector2(-180, 0), 100, 0, 0, "itemframe"));
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
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.Transform);
            Game1.particleManager.Draw(gameTime);
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
            world.Update(gameTime, camera);
            minimap.Update(world, camera);
            Game1.particleManager.Update(gameTime);
        }
        public override void KeyboardUpdate(KeyboardState currentState, KeyboardState previousState)
        {
            if (currentState.IsKeyUp(Keys.Escape) && previousState.IsKeyDown(Keys.Escape))
            {
                Game1.stateManager.EnterState("pauseState");
            }
        }
        public override void MouseUpdate(MouseState currentState, MouseState previousState)
        {
        }
    }
}
