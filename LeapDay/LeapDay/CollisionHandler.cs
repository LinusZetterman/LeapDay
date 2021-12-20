using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LeapDay
{
    class CollisionHandler : IGameObject
    {
        public void Die()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gt)
        {
            for (int i = 0; i < Game1.GameObjects.Count; i++)
            {
                //Block collisions
                if (Game1.GameObjects[i] is Block)
                {
                    Block block = (Block)Game1.GameObjects[i];
                    for (int j = 0; j < Game1.GameObjects.Count; j++)
                    {
                        if (Game1.GameObjects[j] is Player)
                        {
                            Player player = (Player)Game1.GameObjects[j];

                            if ((player.pos.X < block.pos.X + block.size.X && player.pos.X + player.size.X > block.pos.X) && (player.pos.Y < block.pos.Y + block.size.Y && player.pos.Y + player.size.Y > block.pos.Y)) //FEL GREJ
                            {
                                Vector2 deltaPos = new Vector2(player.pos.Y - block.pos.Y, player.pos.X - block.pos.X);
                                double angle = Math.Atan2(deltaPos.Y, deltaPos.X);
                                int roundedAngle = (int)Math.Round(angle / (2 * Math.PI) * 4) + 2;

                                CollisionDir state = CollisionDir.Bottom;
                                float thing = 0;

                                if (roundedAngle == 0)
                                {
                                    player.isOnGround = true;
                                    state = CollisionDir.Top;
                                    thing = block.pos.Y;
                                }
                                else
                                {
                                    player.isOnGround = false;
                                }

                                if (roundedAngle == 1)
                                {
                                    state = CollisionDir.Left;
                                    thing = block.pos.X;
                                }

                                if (roundedAngle == 2)
                                {
                                    state = CollisionDir.Bottom;
                                    thing = block.pos.Y + block.size.Y;
                                }

                                if (roundedAngle == 3)
                                {
                                    state = CollisionDir.Right;
                                    thing = block.pos.X + block.size.X;
                                }
                                

                                player.BlockCollision(state, thing);
                                goto Break;
                            }
                        }
                    }
                }
            }
            Break:;

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            //_spriteBatch.DrawString(Game1.arial, state.ToString(), new Vector2(100, 100), Color.White);

            //SKRIV UT 
        }
    }
}
