using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spelprojekt.Graphics;
using Spelprojekt.Worlds;
using set = Spelprojekt.GameProperties.GlobalProperties;

namespace Spelprojekt.Entities
{
    public class Entity
    {
        public enum GravityDirection
        {
            down = 60,
            up = -60
        }
        public bool affectedByGravity, doesCollide, isAlive = true, isGrounded;
        public Vector2 velocity, location, collisionOffset, size;
        public Point rightUpPoint, rightDownPoint, leftUpPoint, leftDownPoint,
             leftFallPoint, rightFallPoint;
        public Rectangle hitBox;
        private KeyboardState currentKeyState, previousKeyState;
        private MouseState currentMouseState, previousMouseState;
        public GravityDirection gravityDirection = GravityDirection.down;

        public Entity(bool affectedByGravity, bool doesCollide, Vector2 size, Vector2 location)
        {
            this.affectedByGravity = affectedByGravity;
            this.doesCollide = doesCollide;
            hitBox = new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y);
            this.location = location;
            this.size = size;
        }
        public virtual void Update(GameTime gameTime, World world, Camera2D camera)
        {
            #region Collision Based Movement
            if (doesCollide)
            {
                int pixelsToMoveX = (int)velocity.X;
                int pixelsToMoveY = (int)velocity.Y;

                if (velocity.X > 0) // Right
                {
                    while (pixelsToMoveX > 0)
                    {
                        if (rightUpPoint.X < set.WORLD_SIZE * set.TILE_SIZE - 1)
                        {
                            location.X += 1;
                            pixelsToMoveX--;
                            UpdatePoints();

                            if (world.IsTileSolid(rightUpPoint))
                            {
                                location.X -= 1;
                                pixelsToMoveX = 0;
                            }
                            else if (world.IsTileSolid(rightDownPoint))
                            {
                                location.X -= 1;
                                pixelsToMoveX = 0;
                            }
                        }
                        else
                        {
                            pixelsToMoveX = 0;
                            velocity.X = 0;
                        }
                    }
                }
                else if (velocity.X < 0) // Left
                {
                    while (pixelsToMoveX < 0)
                    {
                        if (leftUpPoint.X > 0)
                        {
                            location.X -= 1;
                            pixelsToMoveX++;
                            UpdatePoints();

                            if (world.IsTileSolid(leftUpPoint))
                            {
                                location.X += 1;
                                pixelsToMoveX = 0;
                            }
                            else if (world.IsTileSolid(leftDownPoint))
                            {
                                location.X += 1;
                                pixelsToMoveX = 0;
                            }
                        }
                        else
                        {
                            pixelsToMoveX = 0;
                            velocity.X = 0;
                        }
                    }
                }

                if (velocity.Y > 0) // Down
                {
                    while (pixelsToMoveY > 0)
                    {
                        if (leftFallPoint.Y < set.WORLD_SIZE * set.TILE_SIZE - 2)
                        {
                            location.Y += 1;
                            pixelsToMoveY--;
                            UpdatePoints();

                            if (world.IsTileSolid(rightDownPoint))
                            {
                                location.Y -= 1;
                                pixelsToMoveY = 0;
                                isGrounded = true;
                                velocity.Y = 0;
                            }
                            else if (world.IsTileSolid(leftDownPoint))
                            {
                                location.Y -= 1;
                                pixelsToMoveY = 0;
                                isGrounded = true;
                                velocity.Y = 0;
                            }
                            else
                            {
                                isGrounded = false;
                            }
                        }
                        else
                        {
                            pixelsToMoveY = 0;
                            isGrounded = true;
                            velocity.Y = 0;
                        }
                    }
                }
                else if (velocity.Y < 0) // Up
                {
                    while (pixelsToMoveY < 0)
                    {
                        if (leftUpPoint.Y > 0)
                        {
                            location.Y -= 1;
                            pixelsToMoveY++;
                            UpdatePoints();

                            if (world.IsTileSolid(leftUpPoint))
                            {
                                location.Y += 1;
                                velocity.Y = 0;
                                pixelsToMoveY = 0;
                            }
                            else if (world.IsTileSolid(rightUpPoint))
                            {
                                location.Y += 1;
                                velocity.Y = 0;
                                pixelsToMoveY = 0;
                            }
                        }
                        else
                        {
                            pixelsToMoveY = 0;
                            velocity.Y = 0;
                        }
                    }
                }
                if (!world.IsTileSolid(leftFallPoint) && !world.IsTileSolid(rightFallPoint))
                {
                    isGrounded = false;
                }

            }
            #endregion
            else
            {
                location += velocity;
            }

            if (affectedByGravity)
            {
                if (!isGrounded)
                {
                    velocity.Y += (float)((int)gravityDirection / 100.0f);
                }
                else
                {
                    velocity.Y = 0;
                }

            }
            hitBox.X = (int)location.X;
            hitBox.Y = (int)location.Y;

                

            currentKeyState = Keyboard.GetState();
            if (previousKeyState != null) KeyboardUpdate(gameTime, currentKeyState, previousKeyState, world, camera);
            previousKeyState = Keyboard.GetState();

            currentMouseState = Mouse.GetState();
            if (previousMouseState != null) MouseUpdate(gameTime, currentMouseState, previousMouseState, world, camera);
            previousMouseState = Mouse.GetState();

            OtherUpdate(gameTime, world, camera);
        }
        public virtual void KeyboardUpdate(GameTime gameTime, KeyboardState currentState, KeyboardState previousState, World world, Camera2D camera)
        {

        }
        public virtual void MouseUpdate(GameTime gameTime, MouseState currentState, MouseState previousState, World world, Camera2D camera)
        {

        }
        public virtual void OtherUpdate(GameTime gameTime, World world, Camera2D camera)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Camera2D camera)
        {

        }
        public void UpdatePoints()
        {
            leftUpPoint = new Point((int)location.X + (int)collisionOffset.X, (int)location.Y + (int)collisionOffset.Y);
            rightUpPoint = new Point((int)location.X + hitBox.Width, (int)location.Y + (int)collisionOffset.Y);
            leftDownPoint = new Point((int)location.X + (int)collisionOffset.X, (int)location.Y + hitBox.Height - 1);
            rightDownPoint = new Point((int)location.X + hitBox.Width, (int)location.Y + hitBox.Height - 1);
            rightFallPoint = new Point(rightDownPoint.X, rightDownPoint.Y + 1 + (int)collisionOffset.Y);
            leftFallPoint = new Point(leftDownPoint.X, leftDownPoint.Y + 1 + (int)collisionOffset.Y);
        }
        public virtual void OnHitEntity(Entity entity)
        {

        }
        public virtual void OnRemoval(World world)
        {

        }
    }
}
