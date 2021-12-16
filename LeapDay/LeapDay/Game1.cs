using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LeapDay
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		Texture2D pixel;

		public static Vector2 basePos = new Vector2(0, 0);

		Point tileNumber;
		public static List<IGameObject> GameObjects = new List<IGameObject>();
		string[] map;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_graphics.PreferredBackBufferWidth = 562;
			_graphics.PreferredBackBufferHeight = 950;
			_graphics.ApplyChanges();

			pixel = new Texture2D(GraphicsDevice, 1, 1);
			pixel.SetData(new[] {Color.White});
			
			map = new string[21];
			map = CreateMap();

			float sideLength = _graphics.PreferredBackBufferWidth / 14;
			Vector2 blockSize = new Vector2(sideLength, sideLength);

			GameObjects.Add(new Player(pixel, new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight)));

			for (int i = 0; i < map.Length; i ++)
            {
				for (int j = 0; j < map[i].Length; j++)
                {
					if (map[i][j] == 'G')
                    {
						Vector2 pos = new Vector2(j * sideLength, i * sideLength + (_graphics.PreferredBackBufferHeight - tileNumber.Y * sideLength));
						GameObjects.Add(new Ground(pixel, blockSize, pos));
                    }
                }
            }
		}

		private string[] CreateMap()
		{
			string[] _stringMap = new string[] {
				"00000G00000000", //0
				"00000000000000", //1
				"00000000000000", //2
				"000000000G0000", //3
				"00000000G00000", //4
				"00000000000000", //5
				"00000000000000", //6
				"00000000000000", //7
				"00000000000000", //8
				"00000000000000", //9
				"00000000000000", //10
				"00000000000000", //11
				"00000000000000", //12
				"00000000000000", //13
				"00000GGGG00000", //14
				"0000GGGGGG0000", //15
				"0000GGGGGG0000", //16
				"00000GGGG00000", //17
				"00000000000000", //18
				"00000000000000", //19
				"00000000000000", //20
				"00000000000000", //21
				"00000000000000", //22
				"00000000000000", //23
				"00000000000000", //24
				"00000000000000", //25
				"00000000000000", //26
				"00000000000000", //27
				"00000000000000", //28
				"00000000000000", //29
			};

			tileNumber.X = _stringMap[0].Length;
			tileNumber.Y = _stringMap.Length;

			return _stringMap;
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			for (int i = 0; i < GameObjects.Count; i++)
            {
				GameObjects[i].Update(gameTime);
            }

			basePos.Y++;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();
			for (int i = 0; i < GameObjects.Count; i ++)
            {
				GameObjects[i].Draw(_spriteBatch);
            }
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
