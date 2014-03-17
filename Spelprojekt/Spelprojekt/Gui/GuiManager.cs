using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spelprojekt.Gui.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spelprojekt.Gui
{
    public class GuiManager : DrawableGameComponent
    {
        List<BasicComponent> components = new List<BasicComponent>();
        SpriteBatch spriteBatch;
        public enum ChangeType 
        {
            property, variable
        }
        public GuiManager(Game1 game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
        }
        public T Get<T>(string name) where T : BasicComponent
        {
            T component = null;
            components.ForEach(x =>
            {
                if (x.name == name) component = (T)x;
            });
            return component;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            components.ForEach(x => { if (x.visible) x.Draw(gameTime, spriteBatch); });
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            components.ForEach(x => { if (x.visible) x.Update(gameTime); });
            base.Update(gameTime);
        }
        public void Add(BasicComponent comp)
        {
            components.Add(comp);
        }
        public void Remove(string name)
        {
            if(components.Contains(Get<BasicComponent>(name))) 
                components.Remove(Get<BasicComponent>(name));
        }
        public void Clear()
        {
            components.Clear();
        }
        public void RemoveMany(params string[] names)
        {
            foreach (string s in names)
            {
                Remove(s);
            }
            
        }
        public List<BasicComponent> GetMany(params string[] names)
        {
            List<BasicComponent> comps = new List<BasicComponent>();
            foreach (string s in names)
            {
                comps.Add(Get<BasicComponent>(s));
            }
            return comps;
        }
        public bool AnyHovering()
        {
            foreach (BasicComponent c in components)
            {
                if (c.CurrentHovering() && c.visible) return true;
            }
            return false;
        }
    }
}
