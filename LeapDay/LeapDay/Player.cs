using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeapDay
{
    class Player : IGameObject
    {
        Texture2D texture;
        public Point size = new Point(20, 20);
        Point gameSize;
        
        public Vector2 pos = new Vector2(100, 100);
        Vector2 vel;
        float baseSpeed = 3;
        float gravAcc = 0.2f;

        int jumpsLeft = 2;
        public bool isOnGround = false;
        bool spaceIsPressed = false;
        bool run = true;
        int direction = 1;

        public Player(Texture2D _texture, Point _gameSize)
        {
            texture = _texture;
            gameSize = _gameSize;
            vel.X = baseSpeed;
        }
        
        public void Die()
        {
            
        }

        public void Update(GameTime gt)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            HandleWallAndFloorCollisions(gameSize);

            HandleJumps(keyboardState);

            
            if (!isOnGround)
                vel.Y += gravAcc;
            if (run)
                pos.X += vel.X * direction;
            pos.Y += vel.Y;
        }

        private void HandleWallAndFloorCollisions(Point floor)
        {
        
            if (pos.Y + size.Y > floor.Y)
            {
                pos.Y = floor.Y - size.Y;
                isOnGround = true;
                if (vel.Y > 0)
                    vel.Y = 0f;
                jumpsLeft = 2;
            }
            else
            {
                isOnGround = false;
            }


            //Wall collisions
            if (pos.X < 0 || pos.X + size.X >= gameSize.X)
            {
                jumpsLeft = 2;
                if (isOnGround)
                    run = true;
                else
                    run = false;

                if (pos.X < 0)
                    direction = 1;
                if (pos.X + size.X >= gameSize.X)
                    direction = -1;
            }
        }

        public void BlockCollision(CollisionDir collisionDir, float thing)
        {
            //    if (collisionDir == CollisionDir.Top)
            //    {
            //        pos.Y = thing - size.Y;
            //        isOnGround = true;
            //        if (vel.Y > 0)
            //            vel.Y = 0f;
            //        jumpsLeft = 2;
            //    }
            //    if (collisionDir == CollisionDir.Left)
            //    {
            //        if (isOnGround)
            //            run = true;
            //        else
            //            run = false;

            //        direction = 1;
            //    }

            //    if (collisionDir == CollisionDir.Bottom)
            //    {
            //    }

            //    if (collisionDir == CollisionDir.Right)
            //    {
            //        if (isOnGround)
            //            run = true;
            //        else
            //            run = false;

            //        direction = -1;
            //    }
        }

        private void HandleJumps(KeyboardState keyboardState)
        {
            if (jumpsLeft > 0)
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    run = true;
                    if (!spaceIsPressed)
                    {
                        vel.Y = Math.Min(-6, vel.Y - 6);
                        jumpsLeft--;
                        spaceIsPressed = true;
                    }
                }
                else
                {
                    spaceIsPressed = false;
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            if (jumpsLeft > 0)
                _spriteBatch.Draw(texture, new Rectangle(pos.ToPoint(), size), Color.Red);
            else
                _spriteBatch.Draw(texture, new Rectangle(pos.ToPoint(), size), Color.Blue);

        }
    }

    public enum CollisionDir
    {
        Top,
        Bottom,
        Left,
        Right
    }
}
