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
        public Point size = new Point(20, 20);
        Point gameSize;
        
        public Vector2 pos = new Vector2(100, 400);
        public Vector2 vel;
        float baseSpeed = 3;
        float gravAcc = 0.2f;

        int jumpsLeft = 0;
        public bool isOnGround = false;
        bool wasOnGround = false; //Disables isOnGround if it has not been on ground for the entire round
        bool spaceIsPressed = false;
        bool run = true;
        int direction = 1;

        public Player(Point _gameSize)
        {
            gameSize = _gameSize;
            vel.X = baseSpeed;
        }
        
        public void Die()
        {
            
        }

        public void Update(GameTime gt)
        {
            wasOnGround = false;

            KeyboardState keyboardState = Keyboard.GetState();

            HandleWallAndFloorCollisions(gameSize);

            HandleJumps(keyboardState);

            if (!isOnGround)
			{
                wasOnGround = false;
			}
            if (wasOnGround)
                vel.Y += gravAcc;
            if (run)
                pos.X += vel.X * direction;
            pos.Y += vel.Y;
        }

        private void HandleWallAndFloorCollisions(Point floor)
        {
        
            //if (pos.Y + size.Y > floor.Y)
            //{
            //    pos.Y = floor.Y - size.Y;
            //    isOnGround = true;
            //    if (vel.Y > 0)
            //        vel.Y = 0f;
            //    jumpsLeft = 2;
            //}
            //else
            //{
            //    isOnGround = false;
            //}


            ////Wall collisions
            //if (pos.X < 0 || pos.X + size.X >= gameSize.X)
            //{
            //    jumpsLeft = 2;
            //    if (isOnGround)
            //        run = true;
            //    else
            //        run = false;

            //    if (pos.X < 0)
            //        direction = 1;
            //    if (pos.X + size.X >= gameSize.X)
            //        direction = -1;
            //}
        }

        public void BlockCollision(CollisionDir collisionDir, float thing)
        {
            if (collisionDir == CollisionDir.Upper)
            {
                pos.Y = thing - size.Y;
                vel.Y = Math.Min((float)vel.Y, 0.0f);
                isOnGround = true;
                jumpsLeft = 2;
                run = true;
                wasOnGround = true;
			}

			if (collisionDir == CollisionDir.Right)
			{
				pos.X = thing - size.X;
				direction = -1;
				if (wasOnGround)
					run = true;
				else
					run = false;
			}
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
                _spriteBatch.Draw(Game1.publicPixel, new Rectangle(pos.ToPoint(), size), Color.Red);
            else
                _spriteBatch.Draw(Game1.publicPixel, new Rectangle(pos.ToPoint(), size), Color.Blue);

        }
    }

    public enum CollisionDir
    {
        Upper,
        Lower,
        Left,
        Right
    }
}
