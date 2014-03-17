using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spelprojekt.Gui.Components;
using Spelprojekt.Gui;
using Spelprojekt.GameProperties;

namespace Spelprojekt.States
{
    class StateMenu : State
    {
        public StateMenu(Game1 game, string name) : base(game, name)
        {

        }
        public override void FirstEnter()
        {
            Game1.guiManager.Add(new Button("buttonStart", "buttonTemp", "testFont", "Start")
                .SetLocation<Button>(new Vector2(100, 100))
                .SetSize<Button>(100, 30)
                .SetVisible<Button>(true)
                .SetTextPadding(new Vector2(30, 5))
                .SetButtonColor(Color.LightGray)
                .SetTextColor(Color.White));
            Game1.guiManager.Get<Button>("buttonStart").OnClickEvent += (Button sender) => 
            {
                Game1.stateManager.EnterState("gameState");
            };
            Game1.guiManager.Get<Button>("buttonStart").OnHoverEvent += (Button sender) =>
            {
                sender.buttonColor = Color.Gray;
            };
            Game1.guiManager.Get<Button>("buttonStart").OnLeaveEvent += (Button sender) =>
            {
                sender.buttonColor = Color.LightGray;
            };

            Game1.guiManager.Add(new Button("buttonExit", "buttonTemp", "testFont", "Exit")
            .SetLocation<Button>(new Vector2(100, 150))
            .SetSize<Button>(100, 30)
            .SetVisible<Button>(true)
            .SetTextPadding(new Vector2(30, 5))
            .SetButtonColor(Color.LightGray)
            .SetTextColor(Color.White));
            Game1.guiManager.Get<Button>("buttonExit").OnClickEvent += (Button sender) =>
            {
                game.Exit();
            };
            Game1.guiManager.Get<Button>("buttonExit").OnHoverEvent += (Button sender) =>
            {
                sender.buttonColor = Color.Gray;
            };
            Game1.guiManager.Get<Button>("buttonExit").OnLeaveEvent += (Button sender) =>
            {
                sender.buttonColor = Color.LightGray;
            };
        }
        public override void EnterState()
        {
            GlobalProperties.CURSOR_TEXTURE = "cursor";
            foreach (BasicComponent bc in Game1.guiManager.GetMany(
                "buttonStart",
                "buttonExit"
                ))
            {
                bc.visible = true;
            }
        }
        public override void LeaveState(string nextState)
        {
            foreach (BasicComponent bc in Game1.guiManager.GetMany(
                "buttonStart",
                "buttonExit"
                ))
            {
                bc.visible = false;
            }
        }
        public override void Update(GameTime gameTime)
        {
            Game1.particleManager.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Game1.particleManager.Draw(gameTime);
        }
    }
}
