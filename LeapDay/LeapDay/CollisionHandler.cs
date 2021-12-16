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

                            if ((player.pos.X < block.pos.X + block.size.X || player.pos.X + player.size.X > block.size.X) && player.pos.Y < block.pos.Y + block.size.Y || player.pos.Y + player.size.Y > block.pos.Y) //FEL GREJ
                            {
                                //float leftDist = 
                            }
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            
        }
    }
}
