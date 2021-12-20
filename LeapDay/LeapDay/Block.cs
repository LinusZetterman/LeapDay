using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LeapDay
{
	class Block : IGameObject
	{
		public Vector2 pos;
		public Vector2 startPos;
		public Vector2 size;
        private Point index;
		Texture2D texture;

        public Block(Texture2D _texture, Vector2 _size, Vector2 _pos)
        {
			texture = _texture;
			size = _size;
			startPos = _pos;
        }

        public void Die()
		{
			throw new NotImplementedException();
		}

		public void Update(GameTime gt)
		{
			pos = Game1.basePos + startPos;
		}
		public void Draw(SpriteBatch _spriteBatch)
		{
			_spriteBatch.Draw(texture, new Rectangle((pos).ToPoint(), size.ToPoint()), Color.Black);
		}

	}
}