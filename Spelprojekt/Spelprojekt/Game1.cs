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
using Spelprojekt.Gui;
using Spelprojekt.Graphics;
using Spelprojekt.Gui.Components;
using Spelprojekt.Worlds;
using Spelprojekt.GameProperties;
using Spelprojekt.Entities;
using Spelprojekt.States;
using SpelProjekt.Worlds;
using Spelprojekt.Particle;

namespace Spelprojekt
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        public static Random rnd = new Random();
        public static StateManager stateManager;
        public static GuiManager guiManager;
        public static ParticleManager particleManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = false;
            this.graphics.PreferredBackBufferWidth = GlobalProperties.SCREEN_WIDTH;
            this.graphics.PreferredBackBufferHeight = GlobalProperties.SCREEN_HEIGHT;
            this.graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            MinimapColors.SetColors();
            stateManager = new StateManager(this);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            particleManager = new ParticleManager(this, spriteBatch);
            guiManager = new GuiManager(this, spriteBatch);
            TextureCreator.device = GraphicsDevice;
            ResourceCollection.LoadResources(Content);
            GlobalProperties.CURSOR_TEXTURE = "cursorCrosshair";
            this.Components.Add(guiManager);
            Tile.texture = ResourceCollection.textures["tileSetForest"];
            StateGame game = new StateGame(this, "gameState");
            stateManager.RegisterState(game);
            stateManager.RegisterState(new StatePause(this, "pauseState", game));
            stateManager.RegisterState(new StateMenu(this, "menuState"));
            stateManager.EnterState("menuState");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // TODO: Add your update logic here
            //if (guiManager.Get<SliderBar>("slider").progress < 100)
            //{
            //    guiManager.Get<SliderBar>("slider").value++;
            //}
            stateManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            stateManager.Draw(spriteBatch, gameTime);
            base.Draw(gameTime);
            spriteBatch.Begin();
            if (ItemSlot.mouseItem != null && stateManager.currentState.GetType() == typeof(StateGame)) ItemSlot.mouseItem.sprite.Draw(spriteBatch, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White * 0.8f);
            spriteBatch.Draw(ResourceCollection.textures[GlobalProperties.CURSOR_TEXTURE],
                new Vector2(
                    Mouse.GetState().X,
                    Mouse.GetState().Y), Color.White);
            spriteBatch.End();
        }
    }
}
