using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spelprojekt.States
{
    public class State
    {
        public string name
        {
            get;
            internal set;
        }
        protected Game1 game;
        public State(Game1 game, string name)
        {
            this.name = name;
            this.game = game;
        }
        /// <summary>
        /// Called every time the game enters the state
        /// </summary>
        public virtual void EnterState()
        {

        }
        /// <summary>
        /// Called when first registering the state
        /// </summary>
        public virtual void Initialize()
        {

        }
        /// <summary>
        /// Called the first time the gmae enters the state
        /// </summary>
        public virtual void FirstEnter()
        {

        }
        /// <summary>
        /// Called when the game leaves this state
        /// </summary>
        public virtual void LeaveState(string nextState)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void KeyboardUpdate(KeyboardState currentState, KeyboardState previousState)
        {

        }
        public virtual void MouseUpdate(MouseState currentState, MouseState previousState)
        {

        }
    }
}
