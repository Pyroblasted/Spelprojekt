using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spelprojekt.Gui.Components;
using set = Spelprojekt.GameProperties.GlobalProperties;
using Spelprojekt.Worlds;
using Spelprojekt.Util;
using Spelprojekt.Items;
using Spelprojekt.Graphics;

namespace Spelprojekt.Entities
{
    public class Player : Creature
    {
        public enum Direction
        {
            left, right
        }
        public enum AnimationNumbers
        {
            walkingRight = 0,
            walkingLeft = 1,
            runningRight = 2,
            runningLeft = 3,
            runJumpRight = 4,
            runJumpLeft = 5,
            walkJumpRight = 6,
            walkJumpLeft = 7,
            standingLeft = 9,
            standingRight = 8

        }
        public int
            stamina = 350,
            maxStamina = 350,
            shootTimer = 0;
        public string texture;
        public AnimationSingleLine animationSheet;
        public Direction direction;
        public AnimationNumbers currentAnimation;
        public bool running;
        public float rotation;

        public Player(Vector2 location, Vector2 size, string texture)
            : base(true, true, size, location, 1000, 10, 5)
        {
            collisionOffset = new Vector2(20, 0);
            this.texture = texture;
            animationSheet = new AnimationSingleLine(64, 64, 0, texture, 10);
            animationSheet.Start();


            animationSheet.OnFinishedEvent += new AnimationSingleLine.OnAnimationFinished(animationSheet_OnFinishedEvent);
        }

        void animationSheet_OnFinishedEvent(AnimationSingleLine sender, int frame, int line)
        {
            if (currentAnimation == AnimationNumbers.standingLeft
                || currentAnimation == AnimationNumbers.standingRight
                 || currentAnimation == AnimationNumbers.runJumpLeft
                 || currentAnimation == AnimationNumbers.runJumpRight
                 || currentAnimation == AnimationNumbers.walkJumpLeft
                 || currentAnimation == AnimationNumbers.walkJumpRight)
            {
                sender.Pause();
            }
            else
            {
                sender.Frame = 0;
            }

        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Camera2D camera)
        {
            animationSheet.Draw(spriteBatch, location, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
            ItemSlotCollection c = Game1.guiManager.Get<ItemSlotCollection>("hotbar");
            if (c != null && c.slotCollection[c.selectedSlot].item != null && (c.slotCollection[c.selectedSlot].item.GetType() == typeof(ItemHoldable) || c.slotCollection[c.selectedSlot].item.GetType() == typeof(ItemWeapon)))
            {
                ItemHoldable h = (ItemHoldable)c.slotCollection[c.selectedSlot].item;
                h.Draw(spriteBatch, (int)location.X, (int)location.Y, rotation);
            }
        }
        public override void MouseUpdate(GameTime gameTime, MouseState currentState, MouseState previousState, World world, Camera2D camera)
        {
            //if (ItemSlot.mouseItem != null)
            //{
            //    if (currentState.LeftButton == ButtonState.Pressed)
            //    {
            //        ItemSlot.mouseItem.LeftClick(currentState, previousState, currentState.X, currentState.Y);
            //    }
            //    if (currentState.RightButton == ButtonState.Pressed)
            //    {
            //        ItemSlot.mouseItem.UseItem(currentState, previousState, currentState.X, currentState.Y);
            //    }
            //}
            //else if (ItemSlot.mouseItem == null)
            //{
            //    //use current item
            //}
            ItemSlotCollection c = Game1.guiManager.Get<ItemSlotCollection>("hotbar");
            if (currentState.LeftButton == ButtonState.Pressed && !Game1.guiManager.AnyHovering() && c.slotCollection[c.selectedSlot].item != null && c.slotCollection[c.selectedSlot].item.GetType() == typeof(ItemWeapon))
            {
                ItemWeapon weapon = (ItemWeapon)c.slotCollection[c.selectedSlot].item;
                weapon.Fire(world, this, camera);
            }


        }
        public override void KeyboardUpdate(GameTime gameTime, KeyboardState currentState, KeyboardState previousState, World world, Camera2D camera)
        {
            running = false;

            #region NoCollide
            if (currentState.IsKeyDown(Keys.E))
            {
                location.X += 10;
            }
            if (currentState.IsKeyDown(Keys.Q))
            {
                location.X -= 10;
            }
            #endregion

            #region Left
            if (currentState.IsKeyDown(Keys.A))
            {
                velocity.X = -3;
                direction = Direction.left;
                if (isGrounded)
                {
                    currentAnimation = AnimationNumbers.walkingLeft;
                    animationSheet.delay = 6;
                    animationSheet.Start();
                }
            }
            #endregion
            #region Right
            else if (currentState.IsKeyDown(Keys.D))
            {
                velocity.X = 3;
                direction = Direction.right;
                if (isGrounded)
                {
                    currentAnimation = AnimationNumbers.walkingRight;
                    animationSheet.delay = 6;
                    animationSheet.Start();
                }
            }
            #endregion
            #region Stop
            else
            {
                velocity.X = 0;
            }
            #endregion
            #region Sprint
            if (currentState.IsKeyDown(Keys.LeftShift) && stamina > 0)
            {
                velocity.X *= 2;
                running = true;
                //if (Game1.camera.Zoom >= 0.95f)
                //{
                //    Game1.camera.Zoom -= 0.005f;
                //}
                if (isGrounded)
                {
                    if (direction == Direction.left) currentAnimation = AnimationNumbers.runningLeft;
                    if (direction == Direction.right) currentAnimation = AnimationNumbers.runningRight;
                    animationSheet.delay = 2;
                }
            }
            else if (currentState.IsKeyUp(Keys.LeftShift) && isGrounded)
            {
                running = false;
                if (stamina < maxStamina) stamina++;
                //if (Game1.camera.Zoom <= 1.0f)
                //{
                //    Game1.camera.Zoom += 0.005f;
                //}
            }
            if (running && velocity.X != 0) stamina--;
            #endregion
            #region Jump
            if (currentState.IsKeyDown(Keys.Space))
            {
                Point left = new Point(leftUpPoint.X + 1, leftUpPoint.Y - set.TILE_SIZE / 2);
                Point right = new Point(rightUpPoint.X - 1, rightUpPoint.Y - set.TILE_SIZE / 2);
                if (isGrounded && !world.IsTileSolid(left) && !world.IsTileSolid(right))
                {
                    animationSheet.Frame = 0;
                    isGrounded = false;
                    velocity.Y = -14f;
                }
            }
            #region Jumping Animation
            if (!isGrounded)
            {
                if (direction == Direction.left)
                {
                    if (running)
                    {
                        currentAnimation = AnimationNumbers.runJumpLeft;
                        animationSheet.delay = 6;
                    }
                    else
                    {
                        currentAnimation = AnimationNumbers.walkJumpLeft;
                        animationSheet.delay = 8;
                    }
                }
                else if (direction == Direction.right)
                {
                    if (running)
                    {
                        currentAnimation = AnimationNumbers.runJumpRight;
                        animationSheet.delay = 6;
                    }
                    else
                    {
                        currentAnimation = AnimationNumbers.walkJumpRight;
                        animationSheet.delay = 8;
                    }
                }
            }
            #endregion
            #endregion
            #region Start/Stop Animation
            if ((int)velocity.X == 0 && (int)velocity.Y == 0 && isGrounded)
            {
                if (direction == Direction.left)
                {
                    currentAnimation = AnimationNumbers.standingLeft;
                }
                else if (direction == Direction.right)
                {
                    currentAnimation = AnimationNumbers.standingRight;
                }
                animationSheet.Stop();
            }
            else animationSheet.Start();
            animationSheet.Line = (int)currentAnimation;
            #endregion

            if (currentState.IsKeyDown(Keys.G))
            {
                health--;
            }
            //Console.WriteLine("animation number: " + currentAnimation + " | direction: " + direction + " | frame: " + animationSheet.frame + " | target.X: " + animationSheet.target.X + " | target.Y: " + animationSheet.target.Y);
        }
        public override void OtherUpdate(GameTime gameTime, World world, Camera2D camera)
        {
            animationSheet.Update();
            hitBox.X = (int)location.X + 20;
            hitBox.Y = (int)location.Y;
            hitBox.Width = (int)this.size.X;
            hitBox.Height = (int)this.size.Y;
            Game1.guiManager.Get<ProgressBar>("lifeBar").SetProgress(maxHealth, health);
            Game1.guiManager.Get<ProgressBar>("stamBar").SetProgress(maxStamina, stamina);
            rotation = -VectorUtil.GetRotation(location, camera.GetPosition() + new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            //Vector2 cameraPosOffset = location - Game1.camera.Pos;
            //if (((int)cameraPosOffset.X < 100 && (int)cameraPosOffset.X > -100)
            //    || ((int)cameraPosOffset.Y < 100 && (int)cameraPosOffset.Y > -100)) Game1.camera.Pos += velocity;
        }
    }
}
