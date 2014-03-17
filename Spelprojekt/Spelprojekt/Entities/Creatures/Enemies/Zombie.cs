using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Spelprojekt.Graphics;
using Spelprojekt.Worlds;
using Microsoft.Xna.Framework.Graphics;
using Spelprojekt.Gui.Components;
using Spelprojekt.Particle;

namespace Spelprojekt.Entities.Creatures.Enemies
{
    class Zombie : Enemy
    {
        int Timer;
        enum ZombieState { walk, attack, die }
        ZombieState State = ZombieState.walk;
        Rectangle
            VisionBox,
            Real_HitBox;
        Boolean
            FacingRight,
            Intersect,
            Vision;
        SpriteEffects effect;
        public Zombie(bool affectedByGravity, bool doesCollide, Vector2 size, Vector2 location, int health, int armor, int resistance, string texture)
            : base(affectedByGravity, doesCollide, size, location, health, armor, resistance, texture)
        {

        }
        public override void OtherUpdate(GameTime gameTime, World world, Camera2D camera)
        {
            progress.SetProgress(maxHealth, health);
            progress.SetLocation<ProgressBar>(location - new Vector2(0, 30));
            progress.Update(gameTime);
            switch (State)
            {
                case ZombieState.walk:
                    //SrcRec = new Rectangle(0, 0, 64, 64);
                    Point TempPoint;
                    Timer += gameTime.ElapsedGameTime.Milliseconds;
                    hitBox = new Rectangle((int)location.X, (int)location.Y, 64, 64);
                    if (FacingRight)
                    {
                        VisionBox = new Rectangle((int)location.X, (int)location.Y - 200, 400, 400);
                    }
                    else
                    {
                        VisionBox = new Rectangle((int)location.X - 260, (int)location.Y - 200, 400, 400);
                    }
                    if (world.IsTileSolid(rightDownPoint) || world.IsTileSolid(rightUpPoint))
                    {
                        Jump();
                    }
                    if (world.IsTileSolid(leftDownPoint) || world.IsTileSolid(leftUpPoint))
                    {
                        Jump();
                    }
                    if (world.GetPlayer().hitBox.Intersects(VisionBox))
                    {

                        Vision = true;


                    }
                    else
                    {
                        Vision = true;
                    }
                    if (Timer >= 2000)
                    {
                        Delayed_Pos = location;
                        if (Timer >= 4000)
                        {
                            if (location == Delayed_Pos)
                            {
                                Stuck = true;
                                Timer = 0;
                            }
                        }
                    }
                    if (Stuck)
                    {
                        velocity.X *= -1;
                        Stuck = false;
                    }

                    if (Vision)
                    {
                        State = ZombieState.attack;
                    }
                    break;
                case ZombieState.die:

                    break;
                case ZombieState.attack:
                    hitBox = new Rectangle((int)location.X, (int)location.Y, 64, 64);
                    if (FacingRight)
                    {
                        VisionBox = new Rectangle((int)location.X, (int)location.Y - 200, 400, 400);
                    }
                    else
                    {
                        VisionBox = new Rectangle((int)location.X - 260, (int)location.Y - 200, 400, 400);
                    }
                    if (world.GetPlayer().hitBox.Intersects(VisionBox))
                    {
                        Vision = true;

                    }
                    else
                    {
                        Vision = false;
                    }
                    if (Vision)
                    {
                        if (velocity.X >= world.GetPlayer().location.X)
                        {
                            velocity.X = 1;
                            FacingRight = false;
                            effect = SpriteEffects.None;
                        }
                        else
                        {
                            velocity.X = 1;
                            effect = SpriteEffects.FlipHorizontally;
                            FacingRight = true;
                        }
                        if (world.GetPlayer().hitBox.Intersects(hitBox))
                        {
                            Intersect = true;
                        }
                        else
                        {
                            Intersect = false;
                        }
                    }
                    if (Intersect)
                    {
                        Vision = false;
                    }

                    if (!Vision)
                    {
                        State = ZombieState.walk;

                    }

                    if (Intersect)
                    {
                        /*Förlora hp
                         ta skada,
                         Döda folk,
                         Ta health från människor*/

                    }

                    break;
            }
        }
        public override void OnRemoval(World world)
        {
            Game1.particleManager.AddParticle(2000, () =>
            {
                float f1 = (float)(Game1.rnd.NextDouble() - Game1.rnd.NextDouble()) * 2;
                float f2 = (float)(Game1.rnd.NextDouble() - Game1.rnd.NextDouble()) * 2;
                return new ParticleResizing(new Vector2(f1, f2), location + new Vector2((Game1.rnd.Next(64) - Game1.rnd.Next(64)),
                    (Game1.rnd.Next(64) - Game1.rnd.Next(64))),
                    Color.White,
                    "frame",
                    (float)Game1.rnd.NextDouble() * 5,
                    false,
                    0.1f,
                    false);
            });
        }
    }
}
