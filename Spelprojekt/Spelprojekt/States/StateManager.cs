using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spelprojekt.States
{
    public class StateManager
    {
        private Game1 game;
        public Dictionary<String, State> states = new Dictionary<String, State>();
        public Dictionary<String, bool> status = new Dictionary<String, bool>();
        public KeyboardState currentKeyState, previousKeyState;
        public MouseState currentMouseState, previousMouseState;
        public State currentState
        {
            get;
            internal set;
        }
        public StateManager(Game1 game)
        {
            this.game = game;
        }
        public void RegisterState(State state)
        {
            states.Add(state.name, state);
            state.Initialize();
        }
        public void EnterState(string name)
        {
            if(currentState != null) currentState.LeaveState(name);
            currentState = states[name];
            if (!status.ContainsKey(name))
            {
                status.Add(name, true);
                currentState.FirstEnter();
            }
            else if (!status[name])
            {
                status.Remove(name);
                status.Add(name, true);
                currentState.FirstEnter();
            }
            currentState.EnterState();
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentState != null)
            {
                currentState.Draw(spriteBatch, gameTime);
            }
        }
        public void Update(GameTime gameTime)
        {
            currentKeyState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            if (currentState != null)
            {
                currentState.Update(gameTime);
                currentState.KeyboardUpdate(currentKeyState, previousKeyState);
                currentState.MouseUpdate(currentMouseState, previousMouseState);
            }
            previousKeyState = Keyboard.GetState();
            previousMouseState = Mouse.GetState();
        }
    }
}
