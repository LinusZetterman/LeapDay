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
        //Shoud be moved back into loop
        bool[] collisions = {false, false, false, false}; //Upper, left, lower, right
        Vector2 upperPos;
        Vector2 upperSize;

        Vector2 leftPos;
        Vector2 leftSize;

        Vector2 lowerPos;
        Vector2 lowerSize;

        Vector2 rightPos;
        Vector2 rightSize;

        public void Die()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gt)
        {
            bool wallCollision = false;
            bool roofCollision = false;
            bool floorCollision = false;

            //If collision causes future errors, maybe make a copy of the player pos so that the loop can continue from the original state. No idea how though but you'll figure something out <3

            for (int i = 0; i < Game1.GameObjects.Count; i++)
            {
                //Block collisions
                if (Game1.GameObjects[i] is Player)
                {
                    Player player = (Player)Game1.GameObjects[i];

                    Vector2 pPos = player.pos;
                    Vector2 pVel = player.vel;
                    Vector2 pSize = player.size.ToVector2();
                    
                    for (int j = 0; j < Game1.GameObjects.Count; j++)
                    {
                        if (Game1.GameObjects[j] is Block)
                        {
                            Block block = (Block)Game1.GameObjects[j];
                            bool isBlockCollision = DetectCollision(pPos, pSize, block.pos, block.size); //Checks if player and block collision är 

                            if (isBlockCollision) 
                            {
                                collisions = new bool[] {false, false, false, false};

                                pPos -= pVel; //Checks player state before collision

                                //Saves all positions and sizes of the collisionrectangles surrounding the player. Avoided rectangles since I wanted to preserve the decimals
                                upperPos = pPos + new Vector2(0, - Math.Abs(pVel.Y));
                                upperSize = new Vector2(pSize.X, Math.Abs(pVel.Y));

                                leftPos = pPos + new Vector2(- Math.Abs(pVel.X), 0);
                                leftSize = new Vector2(Math.Abs(pVel.X), pSize.Y);

                                lowerPos = pPos + new Vector2(0, pSize.Y);
                                lowerSize = new Vector2(pSize.X, Math.Abs(pVel.Y));

                                rightPos = pPos + new Vector2(pSize.X, 0);
                                rightSize = new Vector2(Math.Abs(pVel.X), pSize.Y);

                                pPos += pVel;

								collisions[0] = DetectCollision(upperPos, upperSize, block.pos, block.size);    //Upper
                                collisions[1] = DetectCollision(leftPos, leftSize, block.pos, block.size);      //Left
                                collisions[2] = DetectCollision(lowerPos, lowerSize, block.pos, block.size);    //Lower
                                collisions[3] = DetectCollision(rightPos, rightSize, block.pos, block.size);    //Right

                                if (collisions[0])
								{
                                    player.BlockCollision(CollisionDir.Upper, block.pos.Y);
								}

                                if(collisions[1])
                                {
                                    player.BlockCollision(CollisionDir.Left, block.pos.X);
                                }

                                if (collisions[2])
                                {
                                    player.BlockCollision(CollisionDir.Lower, block.pos.Y + block.size.Y);
                                }

                                if (collisions[3])
                                {
                                    player.BlockCollision(CollisionDir.Right, block.pos.X + block.size.X);
                                }

                                //goto help;
                            }
                        }
                    }
                }
            }

        //help:;
        }

        public bool DetectCollision(Vector2 aPos, Vector2 aSize, Vector2 bPos, Vector2 bSize)
		{
            bool isCollision = false;

            if ((aPos.X < bPos.X + bSize.X && aPos.X + aSize.X > bPos.X) && (aPos.Y < bPos.Y + bSize.Y && aPos.Y + aSize.Y > bPos.Y))
				isCollision = true;
			return isCollision;
		}

        public void Draw(SpriteBatch _spriteBatch)
        {
            //_spriteBatch.DrawString(Game1.arial, state.ToString(), new Vector2(100, 100), Color.White);

            //_spriteBatch.Draw(Game1.publicPixel, new Rectangle(upperPos.ToPoint(), upperSize.ToPoint()), Color.Yellow);
			//_spriteBatch.Draw(Game1.publicPixel, new Rectangle(leftPos.ToPoint(), leftSize.ToPoint()), Color.Yellow);
			//_spriteBatch.Draw(Game1.publicPixel, new Rectangle(lowerPos.ToPoint(), lowerSize.ToPoint()), Color.Yellow);
			//_spriteBatch.Draw(Game1.publicPixel, new Rectangle(rightPos.ToPoint(), rightSize.ToPoint()), Color.Yellow);

        }
    }
}
