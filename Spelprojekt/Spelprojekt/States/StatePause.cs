using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Spelprojekt.GameProperties;
using Microsoft.Xna.Framework.Input;
using Spelprojekt.Gui.Components;
using Spelprojekt.Gui;

namespace Spelprojekt.States
{
    class StatePause : State
    {
        private StateGame stateGame;
        public StatePause(Game1 game, string name, StateGame stateGame)
            : base(game, name)
        {
            this.stateGame = stateGame;
        }
        public override void FirstEnter()
        {
            Game1.guiManager.Add(new Button("buttonResume", "buttonTemp", "testFont", "Resume")
            .SetLocation<Button>(new Vector2(100, 100))
            .SetSize<Button>(100, 30)
            .SetVisible<Button>(true)
            .SetTextPadding(new Vector2(15, 5))
            .SetButtonColor(Color.LightGray)
            .SetTextColor(Color.White));
            Game1.guiManager.Get<Button>("buttonResume").OnClickEvent += (Button sender) =>
            {
                Game1.stateManager.EnterState("gameState");
            };
            Game1.guiManager.Get<Button>("buttonResume").OnHoverEvent += (Button sender) =>
            {
                sender.buttonColor = Color.Gray;
            };
            Game1.guiManager.Get<Button>("buttonResume").OnLeaveEvent += (Button sender) =>
            {
                sender.buttonColor = Color.LightGray;
            };

            Game1.guiManager.Add(new Button("buttonExit2", "buttonTemp", "testFont", "Quit")
            .SetLocation<Button>(new Vector2(100, 150))
            .SetSize<Button>(100, 30)
            .SetVisible<Button>(true)
            .SetTextPadding(new Vector2(30, 5))
            .SetButtonColor(Color.LightGray)
            .SetTextColor(Color.White));
            Game1.guiManager.Get<Button>("buttonExit2").OnClickEvent += (Button sender) =>
            {
                game.Exit();
            };
            Game1.guiManager.Get<Button>("buttonExit2").OnHoverEvent += (Button sender) =>
            {
                sender.buttonColor = Color.Gray;
            };
            Game1.guiManager.Get<Button>("buttonExit2").OnLeaveEvent += (Button sender) =>
            {
                sender.buttonColor = Color.LightGray;
            };
        }
        public override void EnterState()
        {
            GlobalProperties.CURSOR_TEXTURE = "cursor";
            foreach (BasicComponent bc in Game1.guiManager.GetMany(
            "buttonResume",
            "buttonExit2"
            ))
            {
                bc.visible = true;
            }
        }
        public override void LeaveState(string nextState)
        {
            foreach (BasicComponent bc in Game1.guiManager.GetMany(
                "buttonResume",
                "buttonExit2"
                ))
            {
                bc.visible = false;
            }
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            stateGame.Draw(spriteBatch, gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(ResourceCollection.textures["frame"], new Rectangle(0, 0, GlobalProperties.SCREEN_WIDTH, GlobalProperties.SCREEN_HEIGHT), Color.Black * 0.8f);
            spriteBatch.End();
        }
        public override void KeyboardUpdate(KeyboardState currentState, KeyboardState previousState)
        {
            if (currentState.IsKeyUp(Keys.Escape) && previousState.IsKeyDown(Keys.Escape))
            {
                Game1.stateManager.EnterState("gameState");
            }
        }
    }
}
