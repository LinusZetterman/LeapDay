using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeapDay
{
    class Player : IGameObjects
    {
        Texture2D texture;
        Point size = new Point(20, 20);
        Vector2 pos = new Vector2(100, 100);
        Vector2 vel;
        float gravAcc = 0.2f;

        int jumpsLeft = 2;
        bool isOnGround = false;
        Point gameSize;
        bool spaceIsPressed = false;

        public Player(Texture2D _texture, Point _gameSize)
        {
            texture = _texture;
            gameSize = _gameSize;
        }
        
        public void Die()
        {
            
        }

        public void Update(GameTime gt)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (jumpsLeft > 0)
                HandleJumps(keyboardState);

            HandleWallAndFloorCollisions();
            
            if (!isOnGround)
                vel.Y += gravAcc;
            pos += vel;
        }

        private void HandleWallAndFloorCollisions()
        {
            if (pos.Y + size.Y > gameSize.Y)
            {
                pos.Y = gameSize.Y - size.Y - 1;
                isOnGround = true;
                vel.Y = 0f;
                jumpsLeft = 2;
            }
            else
            {
                isOnGround = false;
            }
        }

        private void HandleJumps(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (!spaceIsPressed) {
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

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, new Rectangle(pos.ToPoint(), size), Color.Red);
        }
    }
}
