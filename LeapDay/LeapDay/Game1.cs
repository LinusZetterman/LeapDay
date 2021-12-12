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

		public static List<IGameObjects> GameObjects;
		Point tilemapSize = new Point(50, 10);
		List<string> map;

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
			_graphics.PreferredBackBufferWidth = 605;
			_graphics.PreferredBackBufferHeight = 1080;
			_graphics.ApplyChanges();


			map = new List<string>();
			map = CreateMap();
		}

		private List<string> CreateMap()
		{
			List<string> _map = new List<string>();
			_map.Add("00000000000000"); //0
			_map.Add("00000000000000"); //1
			_map.Add("00000000000000"); //2
			_map.Add("00000000000000"); //3
			_map.Add("00000000000000"); //4
			_map.Add("00000000000000"); //5
			_map.Add("00000000000000"); //6
			_map.Add("00000000000000"); //7
			_map.Add("00000000000000"); //8
			_map.Add("00000000000000"); //9
			_map.Add("00000000000000"); //10
			_map.Add("00000000000000"); //11
			_map.Add("00000000000000"); //12
			_map.Add("00000000000000"); //13
			_map.Add("00000000000000"); //14
			_map.Add("00000000000000"); //15
			_map.Add("00000000000000"); //16
			_map.Add("00000000000000"); //17
			_map.Add("00000000000000"); //18
			_map.Add("00000000000000"); //19
			_map.Add("00000000000000"); //20


			return _map;
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}
