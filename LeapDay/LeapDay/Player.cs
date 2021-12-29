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
        public Vector2 vel;
        float baseSpeed = 3;
        float gravAcc = 0.2f;

        int jumpsLeft = 0;
        public bool wasOnGround = false;
        bool isOnGround = false; //Disables isOnGround if it has not been on ground for the entire round
        bool spaceIsPressed = false;
        bool run = true;

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

            
            //wasOnGround = isOnGround;

			if (!wasOnGround)
                vel.Y += gravAcc;
            if (run)
                pos.X += vel.X;
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
            isOnGround = false; //Makes sure isOnGround is false as a base case, which is the whole reason for wasOnGround
            
            if (collisionDir == CollisionDir.Upper)
            {
                pos.Y = thing - size.Y;
				vel.Y = Math.Min((float)vel.Y, 0.0f);
				jumpsLeft = 2;
				run = true;
				isOnGround = true;
			}

            if (collisionDir == CollisionDir.Lower)
			{
                pos.Y = thing;
				vel.Y = 0;
			}
            if (collisionDir == CollisionDir.Right)
			{
				pos.X = thing - size.X;
                vel.X = -baseSpeed;
                
                if (wasOnGround)
                    run = true;
                else
                    run = false;

                vel.Y = Math.Max(vel.Y, 0); //Restricts player from sliding upwards

                jumpsLeft = 2;
            }

            if (collisionDir == CollisionDir.Left)
			{
                pos.X = thing;
                vel.X = baseSpeed;

                if (wasOnGround)
                    run = true;
                else
                    run = false;

                vel.Y = Math.Max(vel.Y, 0); //Restricts player from sliding upwards

                jumpsLeft = 2;
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
            Color color = Color.Blue;
            if (jumpsLeft >= 2)
                color = Color.Red;
            else if (jumpsLeft >= 1) 
                color = Color.Purple;

            _spriteBatch.Draw(Game1.publicPixel, new Rectangle(pos.ToPoint(), size), color);


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
