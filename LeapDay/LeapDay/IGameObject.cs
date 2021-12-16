using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LeapDay
{
	public interface IGameObject
	{
		public void Die();
		public void Update(GameTime gt);
		public void Draw(SpriteBatch _spriteBatch);
	}
}
