using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Spelprojekt.Worlds;
using Spelprojekt.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Spelprojekt.Gui.Components;

namespace Spelprojekt.Entities.Creatures.Enemies
{
    class Enemy : Creature
    {
        int stuck_Timer = 0,
        Delayed_Timer,
        Mode_Timmer = 0;
        public string texture;
        public Vector2 Delayed_Pos;
        public enum EnemyMode { walk, attack, jump, }
        EnemyMode state = EnemyMode.walk;
        Rectangle Temprec;
        public Boolean Stuck;
        protected ProgressBar progress;
        public Enemy(bool affectedByGravity, bool doesCollide, Vector2 size, Vector2 location, int health, int armor, int resistance, string texture)
            : base(affectedByGravity, doesCollide, size, location, health, armor, resistance)
        {
            this.texture = texture;
            progress = new ProgressBar("hpTemp", "testBar", "").SetLocation<ProgressBar>(location - new Vector2(0, 30)).SetSize<ProgressBar>(100, 10).SetProgress(maxHealth, health);
        }
        public override void OtherUpdate(GameTime gameTime, World world, Camera2D camera)
        {
            switch (state)
            {
                case EnemyMode.walk:
                    Temprec = new Rectangle((int)location.X + 16, (int)location.Y - 16, 70, 40);
                    if (Temprec.Intersects(world.GetPlayer().hitBox))
                    {
                        state = EnemyMode.jump;
                    }

                    //stuck
                    Delayed_Timer++;
                    if (Delayed_Timer >= 100)
                    {
                        Delayed_Pos = location;
                        Delayed_Timer = 0;
                    }
                    if (Delayed_Pos == location)
                    {
                        stuck_Timer++;
                        if (stuck_Timer >= 100)
                        {
                            Stuck = true;
                            stuck_Timer = 0;
                        }
                    }

                    if (Stuck)
                    {
                        velocity.X *= -1;
                        Stuck = false;
                        Console.WriteLine("Stuck");
                    }
                    break;
                case EnemyMode.jump:
                    Mode_Timmer++;
                    if (isGrounded)
                    {

                        velocity.Y -= 12;
                        Console.WriteLine("Hoooopppa");

                    }
                    Console.WriteLine("Timmer");
                    if (Mode_Timmer >= 120)
                    {

                        state = EnemyMode.walk;
                        Console.WriteLine("Den har nu bytt mode");

                    }
                    break;
            }
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Camera2D camera)
        {
            spriteBatch.Draw(ResourceCollection.textures[texture], location, Color.White);
            progress.Draw(gameTime, spriteBatch);
        }
        public void Jump()
        {
            velocity.Y = -14f;
        }
    }
}
